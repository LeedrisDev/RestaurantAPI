using ShinkoAPI.DataAccess;

namespace ShinkoAPI.Business;

/// <inheritdoc />
public class ShinkoBusiness: IShinkoBusiness
{
    private readonly IShinkoData _shinkoData;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="shinkoData">Data Access layer for Shinko API</param>
    public ShinkoBusiness(IShinkoData shinkoData)
    {
        _shinkoData = shinkoData;
    }
    
    /// <inheritdoc />
    public async Task<IList<DateTime>> GetAvailableReservations(DateTime beginDate, DateTime endDate, int nbGuests, bool isLunch, bool isDinner)
    {
        return await _shinkoData.GetAvailableReservations(beginDate, endDate, nbGuests, isLunch, isDinner);
    }
}