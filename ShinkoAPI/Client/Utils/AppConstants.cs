namespace Client.Utils;

public class AppConstants
{
    public static int TimeToWait = 30000; // 30 seconds
    public class URLs
    {
        public static string ShinkoReservation = "http://shinkoparis.fr/reserver-une-table/";
        public static string ShinkoApi(string beginDate, string endDate, int nbGuests, bool isLunch, bool isDinner)
        {
            return "http://localhost:5292/Shinko/reservations?beginDate="
                + beginDate
                + "&endDate="
                + endDate
                + "&nbGuests="
                + nbGuests
                + "&isLunch="
                + isLunch
                + "&isDinner="
                + isDinner;
        }
    }
}