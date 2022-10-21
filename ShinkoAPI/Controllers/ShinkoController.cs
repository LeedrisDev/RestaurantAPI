using System.ComponentModel.DataAnnotations;
using System.Globalization;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using ShinkoAPI.Models;
using ShinkoAPI.Utils;

namespace ShinkoAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ShinkoController: ControllerBase
{
    private readonly ILogger<ShinkoController> _logger;
    
    public ShinkoController(ILogger<ShinkoController> logger)
    {
        _logger = logger;
    }
    
    /// <summary>
    /// Get available reservations for Shinko
    /// </summary>
    /// <param name="beginDate">Wanted begin date, in format dd/mm/yyyy</param>
    /// <param name="endDate">Wanted end date, in format dd/mm/yyyy</param>
    /// <returns>Available reservations</returns>
    /// <response code="200">Returns the available reservations</response>
    /// <response code="400">If the beginDate or endDate are incorrect</response>
    [HttpGet("reservations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationsResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAvailableReservations([Required] string beginDate, [Required] string endDate)
    {
        var frFr = new CultureInfo("fr-FR");
        DateTime beginDateDatetime;
        DateTime endDateDatetime;

        if (!DateTime.TryParseExact(beginDate, "dd-MM-yyyy", frFr, DateTimeStyles.None, out beginDateDatetime))
            return BadRequest(new ErrorResponse("Wrong beginDate format, should be 'dd-mm-yyyy'"));
        if (!DateTime.TryParseExact(endDate, "dd-MM-yyyy", frFr, DateTimeStyles.None, out endDateDatetime))
            return BadRequest(new ErrorResponse("Wrong endDate format, should be 'dd-mm-yyyy'"));
        if (DateTime.Compare(beginDateDatetime, endDateDatetime) > 0)
            return BadRequest(new ErrorResponse("beginDate should be before endDate"));
        
        return Ok("Hello World");
    }

    [HttpGet("TestEndpoint")]
    public async Task<IActionResult> TestEndpoint()
    {
        HttpClient client = new HttpClient();
        var htlmDoc = new HtmlDocument();
        
        var response = await client.GetAsync(AppConstants.Urls.ShinkoParis);
        var content = await response.Content.ReadAsStringAsync();
        htlmDoc.LoadHtml(content);
    
        var reservationUrl = AppConstants.Urls.ShinkoParis + htlmDoc.DocumentNode
            .SelectSingleNode("/html/body/div[2]/div/div[2]/div/div[2]/a")
            .Attributes["href"].Value;
        
        _logger.LogInformation(reservationUrl);
        
        return Ok("Hello World");
    }
}