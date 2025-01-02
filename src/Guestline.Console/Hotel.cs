
namespace Guestline.Console.Models;

public class Hotel
{
    public string? Id { get; set; }
    public string? Name { get; set; }
    public IList<RoomType>? RoomTypes { get; set; }
    public IList<Room>? Rooms { get; set; }

    public void Validate()
    {
        if (Id == null)
            throw new ArgumentNullException("Booking Service: data initalization for Hotel_Id failed");

        if (Name == null)
            throw new ArgumentNullException("Booking Service: data initalization for Hotel_Name failed");

        if (RoomTypes == null)
            throw new ArgumentNullException("Booking Service: data initalization for Hotel_RoomTypes failed");

        if (Rooms == null)
            throw new ArgumentNullException("Booking Service: data initalization for Hotel_Rooms failed");

        foreach (Room room in Rooms)
            room.Validate();

        foreach (RoomType roomType in RoomTypes)
            roomType.Validate();
    }
}

