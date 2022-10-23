namespace ShinkoAPI.Models;

using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class DateAvailabilitiesModel
{
    [JsonProperty("date")] 
    public string Date { get; set; } = null!;

    [JsonProperty("shifts")] 
    public List<DateAvailabilitiesShift> Shifts { get; set; } = null!;
}

public class DateAvailabilitiesShift
{
    [JsonProperty("id")] 
    public long Id { get; set; }

    [JsonProperty("waitlist_total")] 
    public long WaitlistTotal { get; set; }

    [JsonProperty("open")] 
    public string Open { get; set; }

    [JsonProperty("close")] 
    public string Close { get; set; }

    [JsonProperty("name")] 
    public string Name { get; set; }

    [JsonProperty("marked_as_full")] 
    public bool MarkedAsFull { get; set; }

    [JsonProperty("shift_slots")] 
    public List<ShiftSlot> ShiftSlots { get; set; } = null!;
}

public class ShiftSlot
{
    [JsonProperty("name")] 
    public string Name { get; set; }

    [JsonProperty("closed")] 
    public bool Closed { get; set; }

    [JsonProperty("marked_as_full")] 
    public bool MarkedAsFull { get; set; }

    [JsonProperty("possible_guests")] 
    public List<int> PossibleGuests { get; set; } = null!;
}