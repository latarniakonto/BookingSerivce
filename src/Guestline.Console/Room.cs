
namespace Guestline.Console.Models;

public class RoomType
{
    public string? Code { get; set; }
    public string? Description { get; set; }
}

public class Room
{
    public string? RoomType { get; set; }
    public string? RoomId { get; set; }
}

