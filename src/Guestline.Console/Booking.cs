namespace Guestline.Console.Models;
public class Booking
{
    public string? HotelId { get; set; }
    public string? Arrival { get; set; }
    public string? Departure { get; set; }
    public string? RoomType { get; set; }
    public string? RoomRate { get; set; }

    public void Validate()
    {
        if (HotelId == null)
            throw new ArgumentNullException("Booking Service: data initalization for Booking_Id failed");

        if (Arrival == null)
            throw new ArgumentNullException("Booking Service: data initalization for Booking_Arrival failed");

        if (Departure == null)
            throw new ArgumentNullException("Booking Service: data initalization for Booking_Departure failed");

        if (RoomType == null)
            throw new ArgumentNullException("Booking Service: data initalization for Booking_RoomType failed");

        if (RoomRate == null)
            throw new ArgumentNullException("Booking Service: data initalization for Booking_RoomRate failed");
    }
}

