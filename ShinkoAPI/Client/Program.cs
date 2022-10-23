using Client.Business;
using Client.Utils;

var programDone = false;

while (!programDone)
{
    var reservations = await ClientBusiness.GetShinkoReservations();
    programDone = reservations.ReservationsAvailable;

    if (programDone)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Reservations available !");
        Console.ResetColor();

        foreach (var date in reservations.ReservationDates)
        {
            Console.WriteLine("{0} {1} {2} at {3}", date.ToString("dddd").Capitalize(), date.Day, date.ToString("MMMM"),
                date.ToString("HH:mm"));
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("Go to ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(AppConstants.URLs.ShinkoReservation);
        Console.ResetColor();
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("No reservations available.");
        Console.ResetColor();
        await Task.Delay(AppConstants.TimeToWait);
    }
}