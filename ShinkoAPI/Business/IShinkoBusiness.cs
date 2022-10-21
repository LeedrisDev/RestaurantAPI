namespace ShinkoAPI.Business;

/// <summary>
/// Shinko API Business Layer
/// </summary>
public interface IShinkoBusiness
{
    /// <summary>
    /// Return a list of all available reservations
    /// </summary>
    /// <param name="beginDate">Datetime object.</param>
    /// <param name="endDate">Datetime object.</param>
    /// <param name="nbGuests">Number of guests wanted for the reservations.</param>
    /// <param name="isLunch">Is reservations are to lunch.</param>
    /// <param name="isDinner">Is reservations are to dinner.</param>
    /// <returns>A list of all available reservations</returns>
    public Task<IList<DateTime>> GetAvailableReservations(DateTime beginDate, DateTime endDate, int nbGuests, bool isLunch, bool isDinner);
}