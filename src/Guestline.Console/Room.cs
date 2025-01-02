using System.Text.RegularExpressions;

namespace Guestline.Console.Models;

public class RoomType
{
    public string? Code { get; set; }
    public string? Description { get; set; }

    public void Validate()
    {
        switch (Code)
        {
            case "SGL":
            case "DBL":
                break;

            case null:
            case "":
                throw new ArgumentNullException("Booking service: data initialization for RoomType_Code failed");

            default:
                throw new NotImplementedException("Booking service: case not supported");
        }
    }
}

public class Room
{
    public string? RoomType { get; set; }
    public string? RoomId { get; set; }

    public void Validate()
    {
        switch (RoomType)
        {
            case "SGL":
            case "DBL":
                break;

            case null:
            case "":
                throw new ArgumentNullException("Booking service: data initialization for Room_RoomType failed");

            default:
                throw new NotImplementedException("Booking service: case not supported");
        }
    }
}

