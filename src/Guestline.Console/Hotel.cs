
namespace Guestline.Console.Models;

public class Hotel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public IList<RoomType>? RoomTypes { get; set; }
    public IList<Room>? Rooms { get; set; }
}

