using Guestline.Console.Models;
using Guestline.Console.Extensions;

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

        if (hotel.RoomTypes.FirstOrDefault(rt => rt.Code == p_roomType) == null)
            return 0;

        if (bookings.Count == 0)
            return hotel.Rooms.Where(r => r.RoomType == p_roomType).Count();

        int candidateRooms = hotel.Rooms.Where(r => r.RoomType == p_roomType).Count();
        int overlappingBookings = GetOverlappingBookings(p_hotel, p_dateRange, p_roomType);

        return candidateRooms - overlappingBookings;
    }

    private int GetOverlappingBookings(string p_hotel, string p_dateRange, string p_roomType)
    {
        DateRange newBooking = p_dateRange.GetDateRangeFromYYYYMMDDString();
        return bookings.Where(b => b.HotelId == p_hotel && b.RoomType == p_roomType)
            .Where(b => b.Arrival != null && IsOverlapping(new DateRange(b.Arrival, b.Departure), newBooking)).Count();
    }

    private bool IsOverlapping(DateRange p_existingBooking, DateRange p_newBooking)
    {
        DateTime maxLowerBound = new[] {p_existingBooking.StartDate, p_newBooking.StartDate}.Max();
        DateTime minUpperBound = new[] {p_existingBooking.EndDate, p_newBooking.EndDate}.Min() ??
             new[] {p_existingBooking.StartDate, p_newBooking.StartDate}.Min();

        int result = DateTime.Compare(maxLowerBound, minUpperBound);
        return result <= 0;
    }
}

