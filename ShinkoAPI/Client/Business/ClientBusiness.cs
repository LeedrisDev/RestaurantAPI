using Client.DataAccess;
using Client.Models;

namespace Client.Business;

public class ClientBusiness
{
    public static async Task<ReservationResponse> GetShinkoReservations()
    {
        return await ClientData.GetShinkoReservations();
    }
}