using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using ShinkoAPI.Business;
using ShinkoAPI.Models;

namespace ShinkoAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class ShinkoController: ControllerBase
{
    private readonly ILogger<ShinkoController> _logger;
    private readonly IShinkoBusiness _shinkoBusiness;
    
    public ShinkoController(ILogger<ShinkoController> logger, IShinkoBusiness shinkoBusiness)
    {
        _logger = logger;
        _shinkoBusiness = shinkoBusiness;
    }
    
    /// <summary>
    /// Get available reservations for Shinko
    /// </summary>
    /// <param name="beginDate">Wanted begin date, in format 'dd/mm/yyyy'</param>
    /// <param name="endDate">Wanted end date, in format 'dd/mm/yyyy'</param>
    /// <param name="nbGuests">Wanted number of guests for thw reservation</param>
    /// <param name="isLunch">Is reservation for a lunch</param>
    /// <param name="isDinner">Is reservation for a dinner</param>
    /// <returns>Available reservations</returns>
    /// <response code="200">Returns the available reservations</response>
    /// <response code="400">If the beginDate or endDate are incorrect</response>
    [HttpGet("reservations")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ReservationsResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> GetAvailableReservations(
        [Required] string beginDate, 
        [Required] string endDate,
        [Required] int nbGuests,
        [Required] bool isLunch = true,
        [Required] bool isDinner = true
        )
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
        if (nbGuests is < 2 or > 6)
            return BadRequest(new ErrorResponse("nbGuests should be between 2 and 6"));
        if (!isDinner && !isLunch)
            return BadRequest(new ErrorResponse("isLunch and isDinner cannot be both false"));
        
        var reservations = await _shinkoBusiness.GetAvailableReservations(beginDateDatetime, endDateDatetime, nbGuests, isLunch, isDinner);
        
        return Ok(new ReservationsResponse(reservations));
    }
}