using System.Text.Json;
using Guestline.Console.Models;
using Guestline.Console.Extensions;
using System.Text.RegularExpressions;

IList<Hotel>? hotels = new List<Hotel>();
IList<Booking>? bookings = new List<Booking>();

string pathToHotels = Path.Combine(Directory.GetCurrentDirectory(), "hotels.json");
string pathToBookings = Path.Combine(Directory.GetCurrentDirectory(), "bookings.json");

using (StreamReader readHotels = new StreamReader(pathToHotels))
using (StreamReader readBookings = new StreamReader(pathToBookings))
{
    string json = readHotels.ReadToEnd();
    hotels = JsonSerializer.Deserialize<List<Hotel>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
    json = readBookings.ReadToEnd();
    bookings = JsonSerializer.Deserialize<List<Booking>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
}

var hotel = hotels?.FirstOrDefault();
var booking = bookings?.FirstOrDefault();
Console.WriteLine(hotel?.Rooms?.FirstOrDefault()?.RoomId);

string? input = Console.ReadLine();
string pattern = @"Availability\((?<hotel>(H(\d+))), (?<timeline>(20\d{2})(\d{2})(\d{2})(\-(20\d{2})(\d{2})(\d{2}))?), (?<roomType>[A-Z]{3})\)";
Regex regex = new Regex(pattern);

if (string.IsNullOrEmpty(input) || !regex.IsMatch(input))
{
    Console.WriteLine("Not recognized command");
    return;
}

Match match = regex.Match(input);
DateRange timeline = match.Groups["timeline"].ToString().GetDateRangeFromYYYYMMDDString();
Console.WriteLine(timeline.StartDate);
Console.WriteLine(timeline.EndDate);

