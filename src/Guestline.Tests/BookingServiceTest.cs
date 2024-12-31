using AutoFixture;
using Guestline.Console.Models;
using Guestline.Console.Services;

namespace Guestline.Tests;

public class BookServiceTest
{
    [Fact]
    public void Check_Hotel_Occupancy_No_Bookings()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Customize<Room>(r => r
                .With(r1 => r1.RoomId, fixture.Create<string>())
                .With(r1 => r1.RoomType, "SGL"));

        fixture.Customize<RoomType>(rt => rt
                .With(rt1 => rt1.Code, "SGL")
                .With(rt1 => rt1.Description, fixture.Create<string>()));

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .With(h1 => h1.RoomTypes, fixture.CreateMany<RoomType>(1).ToList())
            .With(h1 => h1.Rooms, fixture.CreateMany<Room>(3).ToList()));

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, new List<Booking>())
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(1).ToList())
            .Create();
        // Act
        int availability = bookingService.GetRoomAvailability("H1", "20240901-20240903", "SGL");
        // Assert
        Assert.Equal(3, availability);
    }

    [Fact]
    public void Check_Hotel_Occupancy_No_Hotels()
    {
        // Arrange
        var fixture = new Fixture();

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, fixture.CreateMany<Booking>(1).ToList())
            .With(bs => bs.hotels, new List<Hotel>())
            .Create();
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => bookingService.GetRoomAvailability("H1", "20240901-20240903", "SGL"));
    }

    [Fact]
    public void Check_Hotel_Occupancy_Invalid_Hotel()
    {
        // Arrange
        var fixture = new Fixture();

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .With(h1 => h1.RoomTypes, fixture.CreateMany<RoomType>(1).ToList())
            .With(h1 => h1.Rooms, fixture.CreateMany<Room>(3).ToList()));

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, fixture.CreateMany<Booking>(1).ToList())
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(1).ToList())
            .Create();
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => bookingService.GetRoomAvailability("H2", "20240901-20240903", "SGL"));
    }

    [Fact]
    public void Check_Hotel_Occupancy_Invalid_Hotel_Null_Rooms()
    {
        // Arrange
        var fixture = new Fixture();

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .With(h1 => h1.RoomTypes, fixture.CreateMany<RoomType>(1).ToList())
            .Without(h1 => h1.Rooms));

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, fixture.CreateMany<Booking>(1).ToList())
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(1).ToList())
            .Create();
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => bookingService.GetRoomAvailability("H1", "20240901-20240903", "SGL"));
    }

    [Fact]
    public void Check_Hotel_Occupancy_Invalid_Hotel_Null_Room_Types()
    {
        // Arrange
        var fixture = new Fixture();

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .Without(h1 => h1.RoomTypes)
            .With(h1 => h1.Rooms, fixture.CreateMany<Room>(3).ToList()));

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, fixture.CreateMany<Booking>(1).ToList())
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(1).ToList())
            .Create();
        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => bookingService.GetRoomAvailability("H1", "20240901-20240903", "SGL"));
    }

    [Fact]
    public void Check_Hotel_Occupancy_Unrecognized_Room_Type()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Customize<Room>(r => r
                .With(r1 => r1.RoomId, fixture.Create<string>())
                .With(r1 => r1.RoomType, "SGL"));

        fixture.Customize<RoomType>(rt => rt
                .With(rt1 => rt1.Code, "SGL")
                .With(rt1 => rt1.Description, fixture.Create<string>()));

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .With(h1 => h1.RoomTypes, fixture.CreateMany<RoomType>(1).ToList())
            .With(h1 => h1.Rooms, fixture.CreateMany<Room>(3).ToList()));

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, new List<Booking>())
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(1).ToList())
            .Create();
        // Act
        int availability = bookingService.GetRoomAvailability("H1", "20240901-20240903", "DBL");
        // Assert
        Assert.Equal(0, availability);
    }

    [Fact]
    public void Check_Hotel_Occupancy_With_Overlapping_Bookings()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Customize<Room>(r => r
                .With(r1 => r1.RoomId, fixture.Create<string>())
                .With(r1 => r1.RoomType, "SGL"));

        fixture.Customize<RoomType>(rt => rt
                .With(rt1 => rt1.Code, "SGL")
                .With(rt1 => rt1.Description, fixture.Create<string>()));

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .With(h1 => h1.RoomTypes, fixture.CreateMany<RoomType>(1).ToList())
            .With(h1 => h1.Rooms, fixture.CreateMany<Room>(3).ToList()));

        fixture.Customize<Booking>(b => b
                .With(b1 => b1.HotelId, "H1").With(b1 => b1.RoomType, "SGL")
                .With(b1 => b1.Arrival, "20240902").With(b1 => b1.Departure, "20240903")
                .With(b1 => b1.RoomRate, "Prepaid"));

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, fixture.CreateMany<Booking>(1).ToList())
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(3).ToList())
            .Create();
        // Act
        int availability = bookingService.GetRoomAvailability("H1", "20240901-20240903", "SGL");
        // Assert
        Assert.Equal(2, availability);
    }

    [Fact]
    public void Check_Hotel_Occupancy_With_Overlapping_Bookings_Overbooking_Scenario()
    {
        // Arrange
        var fixture = new Fixture();
        fixture.Customize<Room>(r => r
                .With(r1 => r1.RoomId, fixture.Create<string>())
                .With(r1 => r1.RoomType, "SGL"));

        fixture.Customize<RoomType>(rt => rt
                .With(rt1 => rt1.Code, "SGL")
                .With(rt1 => rt1.Description, fixture.Create<string>()));

        fixture.Customize<Hotel>(h => h
            .With(h1 => h1.Id, "H1").With(h1 => h1.Name, "Test1")
            .With(h1 => h1.RoomTypes, fixture.CreateMany<RoomType>(1).ToList())
            .With(h1 => h1.Rooms, fixture.CreateMany<Room>(1).ToList()));

        var booking1 = fixture.Build<Booking>()
                .With(b => b.HotelId, "H1").With(b => b.RoomType, "SGL")
                .With(b => b.Arrival, "20240902").With(b => b.Departure, "20240903")
                .With(b => b.RoomRate, "Prepaid").Create();

        var booking2 = fixture.Build<Booking>()
                .With(b => b.HotelId, "H1").With(b => b.RoomType, "SGL")
                .With(b => b.Arrival, "20240901").With(b => b.Departure, "20240902")
                .With(b => b.RoomRate, "Prepaid").Create();

        var booking3 = fixture.Build<Booking>()
                .With(b => b.HotelId, "H1").With(b => b.RoomType, "SGL")
                .With(b => b.Arrival, "20240903").Without(b => b.Departure)
                .With(b => b.RoomRate, "Prepaid").Create();
        var bookings = new List<Booking> {booking1, booking2, booking3};

        var bookingService = fixture.Build<BookingService>()
            .With(bs => bs.bookings, bookings)
            .With(bs => bs.hotels, fixture.CreateMany<Hotel>(1).ToList())
            .Create();
        // Act
        int availability = bookingService.GetRoomAvailability("H1", "20240901-20240903", "SGL");
        // Assert
        Assert.Equal(-2, availability);
    }
}

