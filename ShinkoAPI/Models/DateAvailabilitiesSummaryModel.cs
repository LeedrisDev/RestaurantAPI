namespace ShinkoAPI.Models;

using System;
using System.Collections.Generic;

using Newtonsoft.Json;

#pragma warning disable CS1591
public class DateAvailabilitiesModel
{
    [JsonProperty("date")] 
    public DateTimeOffset Date { get; set; }

    [JsonProperty("isOpen")] 
    public bool IsOpen { get; set; }

    [JsonProperty("shifts")] 
    public List<Shift> Shifts { get; set; } = null!;
}

public class Shift
{
    [JsonProperty("id")] 
    public long Id { get; set; }

    [JsonProperty("possible_guests")] 
    public List<int> PossibleGuests { get; set; } = null!;

    [JsonProperty("waitlist_possible_guests")]
    public List<object> WaitlistPossibleGuests { get; set; } = null!;

    [JsonProperty("closedBookingsAfter")] 
    public long ClosedBookingsAfter { get; set; }

    [JsonProperty("closedBookingsBefore")] 
    public long ClosedBookingsBefore { get; set; }
}
#pragma warning restore CS1591