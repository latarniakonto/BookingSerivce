using Guestline.Console.Models;

namespace Guestline.Console.Services;

public class BookingService
{
    public IList<Booking> bookings = new List<Booking>();
    public IList<Hotel> hotels = new List<Hotel>();

    public BookingService(IList<Booking> p_bookings, IList<Hotel> p_hotels)
    {
        bookings = p_bookings;
        hotels = p_hotels;
    }

    public int GetRoomAvailability(string p_hotel, string p_dateRange, string p_roomType)
    {
        if (hotels.Count == 0)
            throw new InvalidOperationException("Booking service has no valid hotels to use");

        Hotel? hotel = hotels.FirstOrDefault(h => h.Id == p_hotel);
        if (hotel == null || hotel.RoomTypes == null || hotel.Rooms == null)
            throw new InvalidOperationException($"Booking service cannot operate on hotel_{p_hotel}");

        return 0;
    }
}

