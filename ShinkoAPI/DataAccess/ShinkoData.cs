using Newtonsoft.Json;
using ShinkoAPI.Models;
using ShinkoAPI.Utils;

namespace ShinkoAPI.DataAccess;

/// <inheritdoc />
public class ShinkoData: IShinkoData
{
    private readonly ILogger<ShinkoData> _logger;
    private HttpClient _client;

    public ShinkoData(IHttpClientFactory factory, ILogger<ShinkoData> logger)
    {
        _logger = logger;
        _client = factory.CreateClient();
    }
    
    /// <inheritdoc />
    public async Task<IList<DateTime>> GetAvailableReservations(DateTime beginDate, DateTime endDate, int nbGuests, bool isLunch, bool isDinner)
    {
        var datesInfos = await GetDatesInfos(beginDate, endDate);
        return GetAvailableDates(datesInfos, nbGuests, isLunch, isDinner);
    }

    private async Task<string> GetData(string url)
    {
        var response = await _client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
    
    private async Task<IList<DateAvailabilitiesModel>> GetDatesInfos(DateTime beginDate, DateTime endDate)
    {
        var beginDateString = beginDate.ToString("yyyy-MM-dd");
        var endDateStringString = endDate.ToString("yyyy-MM-dd");

        var url = AppConstants.Urls.GetAvailabilitiesSummary(beginDateString, endDateStringString);
        var json = await GetData(url);
        
        // _logger.LogInformation(json);
        
        return JsonConvert.DeserializeObject<List<DateAvailabilitiesModel>>(json)!;
    }

    private IList<DateTime> GetAvailableDates(IList<DateAvailabilitiesModel> data, int nbGuests, bool isLunch, bool isDinner)
    {
        IList<Shift> shifts = new List<Shift>();

        var availableReservations = data.Where(dateAvailabilities =>
        {
            if (!dateAvailabilities.IsOpen || dateAvailabilities.Shifts.Count == 0)
                return false;

            if (isLunch && dateAvailabilities.Shifts[0].PossibleGuests.Contains(nbGuests))
                return true;

            return isDinner && dateAvailabilities.Shifts[1].PossibleGuests.Contains(nbGuests);
        });

        return availableReservations
            .Select(reservation => reservation.Date.DateTime)
            .ToList();
    }
}