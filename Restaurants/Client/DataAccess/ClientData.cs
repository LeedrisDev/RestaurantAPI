using System.Runtime.CompilerServices;
using Client.Models;
using Client.Utils;
using Newtonsoft.Json;

namespace Client.DataAccess;

public class ClientData
{
    private static string BeginDate { get; set; } = null!;
    private static string EndDate { get; set; } = null!;
    private static int NbGuests { get; set; }
    private static bool IsLunch { get; set; }
    private static bool IsDinner { get; set; }
    
    public static async Task<ReservationResponse> GetShinkoReservations()
    {
        var json = await GetData();
        return JsonConvert.DeserializeObject<ReservationResponse>(json)!;
    }

    private static void InitializeProperties()
    {
        try
        {
            BeginDate = Environment.GetEnvironmentVariable("BEGIN_DATE")!;
            EndDate = Environment.GetEnvironmentVariable("END_DATE")!;
            NbGuests = int.Parse(Environment.GetEnvironmentVariable("NB_GUESTS")!);
            IsLunch = bool.Parse(Environment.GetEnvironmentVariable("IS_LUNCH")!);
            IsDinner = bool.Parse(Environment.GetEnvironmentVariable("IS_DINNER")!);
        }
        catch (ArgumentNullException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Some environment variables are missing");
            Console.ResetColor();
            Environment.Exit(1);
        }
    }

    private static async Task<string> GetData()
    {
        InitializeProperties();
        var httpClient = new HttpClient();
        var response = await httpClient.GetAsync(AppConstants.URLs.ShinkoApi(BeginDate, EndDate, NbGuests, IsLunch, IsDinner));

        if (response.IsSuccessStatusCode) return await response.Content.ReadAsStringAsync();
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Error while fetching data, check your environment variables");
        Console.ResetColor();
        Environment.Exit(1);
        
        return string.Empty;
    }
}