namespace ShinkoAPI.Utils;

/// <summary>
/// Class that regroup all constants of the application
/// </summary>
public static class AppConstants
{
    public static class Urls
    {
        public static string GetAvailabilitiesSummary(string beginDate, string endDate)
        {
            return "https://bookings-middleware.zenchef.com/getAvailabilitiesSummary?restaurantId=356608&date_begin="
                   + beginDate
                   + "&date_end="
                   + endDate;
        }

        public static string GetAvailabilities(string beginDate, string endDate)
        {
            return "https://bookings-middleware.zenchef.com/getAvailabilities?restaurantId=356608&date_begin="
                   + beginDate
                   + "&date_end="
                   + endDate;
        }
    }
}