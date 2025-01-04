# Guestline Booking Service

## Prerequisites
- **.NET 8.0**

## Setup Instructions (Linux)

To set up and run the application, follow these steps:

1. Clone the repository to your local machine.
2. Navigate to the project directory.
3. Run `dotnet build`
4. Go to `/src/Guestline.Console/bin/Debug/net8.0/`
5. Run `./Guestline.Console`

## Usage
After successfully building the application the program lets you
to check a new booking availability against a base of all bookings stored in
`bookings.json` file</br>
To see the availability run this command: `Availability(hotel_id, date_range, room_type)`</br>
### Input
1. hotel_id
- `H` followed by a number (e.g. H1, H2, H100)
2. date_range
- A date range represented as `YYYYMMDD-YYYYMMDD` (`-YYYMMDD` part is optional)
3. room_type
- A three-letter uppercase abbreviation (e.g. `SGL`, `DBL`)
- The `RoomType` validator currently only accepts `SGL` and `DBL`

### Output
```
Availability(H1, 20240901-20240903, SGL)
Room availability: 1
```
