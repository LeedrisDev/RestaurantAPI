namespace ShinkoAPI.Business;

/// <inheritdoc />
public class ShinkoBusiness: IShinkoBusiness
{
    /// <inheritdoc />
    public async Task<IList<DateTime>> GetAvailableReservations(DateTime beginDate, DateTime endDate, int nbGuests, bool isLunch, bool isDinner)
    {
        throw new NotImplementedException();
    }
}