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
       return 0;
    }
}

