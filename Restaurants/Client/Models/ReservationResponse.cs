namespace Client.Models;

public class ReservationResponse
{
    /// <summary>
    /// Boolean indicating if there is available reservations
    /// </summary>
    public bool ReservationsAvailable { get; set; }
    
    /// <summary>
    /// List containing all the available reservations
    /// </summary>
    public List<DateTimeOffset> ReservationDates { get; set; } = null!;

    public override string ToString()
    {
        return "ReservationsAvailable: " + ReservationsAvailable + '\n'
               + "reservationDates: " + ReservationDates;
    }
}