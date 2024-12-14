using System.Text.Json;
using Guestline.Console.Models;

IList<Hotel>? hotels = new List<Hotel>();

string path = Path.Combine(Directory.GetCurrentDirectory(), "hotels.json");
StreamReader reader = new StreamReader(path);
string json = reader.ReadToEnd();
hotels = JsonSerializer.Deserialize<List<Hotel>>(json, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
reader.Close();

var hotel = hotels?.FirstOrDefault();
Console.WriteLine(hotel?.Rooms?.FirstOrDefault()?.RoomId);

