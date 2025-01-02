using System.Text.Json;
using Guestline.Console.Models;
using Guestline.Console.Extensions;
using Guestline.Console.Services;
using System.Text.RegularExpressions;

IList<Hotel>? hotels;
IList<Booking>? bookings;

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

if (hotels == null)
    throw new ArgumentNullException($"Booking service: data initialization for hotels failed");

if (bookings == null)
    throw new ArgumentNullException($"Booking service: data initialization for bookings failed");

foreach (Hotel hotel in hotels)
{
    hotel.Validate();
}

foreach (Booking booking in bookings)
{
    booking.Validate();
}

string? input = Console.ReadLine();
string pattern = @"Availability\(\s*(?<hotel>(H(\d+))),\s*(?<timeline>(20\d{2})(\d{2})(\d{2})(\-(20\d{2})(\d{2})(\d{2}))?),\s*(?<roomType>[A-Z]{3})\s*\)";
Regex regex = new Regex(pattern);

if (string.IsNullOrEmpty(input) || !regex.IsMatch(input))
    throw new IOException($"Booking Service: '{input}' is not a supported command");

Match match = regex.Match(input);
DateRange timeline = match.Groups["timeline"].ToString().GetDateRangeFromYYYYMMDDString();
string hotelId = match.Groups["hotel"].ToString();
string roomType = match.Groups["roomType"].ToString();

BookingService service = new BookingService(bookings, hotels);

