using System.Text.Json;
using Guestline.Console.Models;
using System.Text.RegularExpressions;

IList<Hotel>? hotels = new List<Hotel>();

string path = Path.Combine(Directory.GetCurrentDirectory(), "hotels.json");
StreamReader reader = new StreamReader(path);
string json = reader.ReadToEnd();
hotels = JsonSerializer.Deserialize<List<Hotel>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
reader.Close();

var hotel = hotels?.FirstOrDefault();
Console.WriteLine(hotel?.Rooms?.FirstOrDefault()?.RoomId);

string? input = Console.ReadLine();
string pattern = @"Availability\(H(\d+), (20\d{2})(\d{2})(\d{2})(\-(20\d{2})(\d{2})(\d{2}))?, [A-Z]{3}\)";
Regex regex = new Regex(pattern);

if (!string.IsNullOrEmpty(input) && regex.IsMatch(input))
{
    Console.WriteLine("Match");
}

