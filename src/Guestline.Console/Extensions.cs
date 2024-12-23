using System.Text.RegularExpressions;
using System.Globalization;

namespace Guestline.Console.Extensions;

public struct DateRange
{
    public DateTime StartDate;
    public DateTime? EndDate;

    public DateRange(string p_startDate, string p_endDate)
    {
        string format = "yyyyMMdd";
        CultureInfo provider = CultureInfo.InvariantCulture;
        StartDate = DateTime.ParseExact(p_startDate, format, provider);
        EndDate = string.IsNullOrEmpty(p_endDate) ? null : DateTime.ParseExact(p_endDate, format, provider);
    }
}

public static class Extensions
{
    public static DateRange GetDateRangeFromYYYYMMDDString(this string p_date)
    {
        string pattern = @"(?<arrival>(20\d{2})(\d{2})(\d{2}))(\-(?<departure>(20\d{2})(\d{2})(\d{2})))?";
        Regex regex = new Regex(pattern);
        if (!regex.IsMatch(p_date))
            throw new ArgumentException("Wrong string date format");

        Match dateMatch = regex.Match(p_date);
        string arrival = dateMatch.Groups["arrival"].ToString();
        string departure = dateMatch.Groups["departure"].ToString();
        return new DateRange(arrival, departure);
    }
}

