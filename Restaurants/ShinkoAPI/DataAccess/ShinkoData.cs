using Newtonsoft.Json;
using ShinkoAPI.Models;
using ShinkoAPI.Utils;

namespace ShinkoAPI.DataAccess;

/// <inheritdoc />
public class ShinkoData: IShinkoData
{
    private readonly ILogger<ShinkoData> _logger;
    private HttpClient _client;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="factory">IHttpClienFactory instance</param>
    /// <param name="logger">ILogger instance</param>
    public ShinkoData(IHttpClientFactory factory, ILogger<ShinkoData> logger)
    {
        _logger = logger;
        _client = factory.CreateClient();
    }
    
    /// <inheritdoc />
    public async Task<IList<DateTime>> GetAvailableReservationsSummary(DateTime beginDate, DateTime endDate, int nbGuests, bool isLunch, bool isDinner)
    {
        var datesInfos = await GetDatesInfos(beginDate, endDate);
        return GetAvailableDates(datesInfos, nbGuests, isLunch, isDinner);
    }
    
    /// <inheritdoc />
    public async Task<IList<DateTime>> GetAvailableReservations(DateTime beginDate, DateTime endDate, int nbGuests,
        bool isLunch, bool isDinner)
    {
        var url = AppConstants.Urls.GetAvailabilities(beginDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"));
        var data = JsonConvert.DeserializeObject<List<DateAvailabilitiesModel>>(await GetData(url))!;

        return GetAvailableDatesWithHours(data, nbGuests, isLunch, isDinner);
    }

    private async Task<string> GetData(string url)
    {
        var response = await _client.GetAsync(url);
        return await response.Content.ReadAsStringAsync();
    }
    
    private async Task<IList<DateAvailabilitiesSummaryModel>> GetDatesInfos(DateTime beginDate, DateTime endDate)
    {
        var beginDateString = beginDate.ToString("yyyy-MM-dd");
        var endDateStringString = endDate.ToString("yyyy-MM-dd");

        var url = AppConstants.Urls.GetAvailabilitiesSummary(beginDateString, endDateStringString);
        var json = await GetData(url);

        return JsonConvert.DeserializeObject<List<DateAvailabilitiesSummaryModel>>(json)!;
    }

    private IList<DateTime> GetAvailableDates(IEnumerable<DateAvailabilitiesSummaryModel> data, int nbGuests, bool isLunch,
        bool isDinner)
    {
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

    private IList<DateTime> GetAvailableDatesWithHours(IEnumerable<DateAvailabilitiesModel> data, int nbGuests, bool isLunch,
        bool isDinner)
    {
        var availableDates = new List<DateTime>();

        foreach (var dateAvailabilty in data)
        {
            if (dateAvailabilty.Shifts.Count == 0)
                continue;
            
            DateTime dateTime;
            try
            {
                dateTime = DateTime.ParseExact(dateAvailabilty.Date, "yyyy-MM-dd", null);
            }
            catch (FormatException)
            {
                continue;
            }

            switch (isLunch)
            {
                case true when !isDinner:
                    dateAvailabilty.Shifts.Remove(dateAvailabilty.Shifts[1]);
                    break;
                case false when isDinner:
                    dateAvailabilty.Shifts.Remove(dateAvailabilty.Shifts[0]);
                    break;
            }
            
            foreach (var shift in dateAvailabilty.Shifts)
            {
                if (shift.MarkedAsFull)
                    continue;

                foreach (var shiftSlot in shift.ShiftSlots)
                {
                    if (shiftSlot.Closed || !shiftSlot.PossibleGuests.Contains(nbGuests))
                        continue;

                    var hours = int.Parse(shiftSlot.Name.Split(':')[0]);
                    var minutes = int.Parse(shiftSlot.Name.Split(':')[1]);
                    availableDates.Add(dateTime.AddHours(hours).AddMinutes(minutes));
                }
            }
        }

        return availableDates;
    }
}