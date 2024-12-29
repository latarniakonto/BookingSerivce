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
}

