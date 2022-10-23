namespace ShinkoAPI.Models;

#pragma warning disable CS1591
[Serializable]
public class ReservationsResponse
{
    /// <summary>
    /// Boolean indicating if there is available reservations
    /// </summary>
    public bool ReservationsAvailable { get; set; }
    
    /// <summary>
    /// List containing all the available reservations
    /// </summary>
    public IList<DateTime> ReservationDates { get; set; }

    /// <summary>
    /// Default constructor
    /// </summary>
    public ReservationsResponse()
    {
        ReservationsAvailable = false;
        ReservationDates = new List<DateTime>();
    }
    
    /// <summary>
    /// Constructor with parameters
    /// </summary>
    /// <param name="reservationDates">List of Datetime objects</param>
    public ReservationsResponse(IList<DateTime> reservationDates)
    {
        ReservationsAvailable = reservationDates.Count > 0;
        ReservationDates = reservationDates;
    }

    public override string ToString()
    {
        return "ReservationsAvailable: " + ReservationsAvailable + '\n'
               + "reservationDates: " + ReservationDates;
    }
}
#pragma warning restore CS1591