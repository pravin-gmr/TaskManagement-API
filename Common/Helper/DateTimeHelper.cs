using System.Globalization;

namespace API.Common.Helper
{
    public class DateTimeHelper
    {
        public static DateTime GetDate()
        {
            return DateTime.UtcNow;
        }

        public static DateOnly GetDateOnly()
        {
            var dateTime = GetDate();
            return DateOnly.FromDateTime(dateTime);
        }

        public static DateOnly ToDateOnly(string? date)
        {
            if (string.IsNullOrWhiteSpace(date)) return DateOnly.MinValue;
            var datetime = DateTime.Parse(date).Date;
            return DateOnly.FromDateTime(datetime);
        }

        public static DateOnly ToDateOnly(DateTime datetime)
        {
            return DateOnly.FromDateTime(datetime);
        }

        public static DateTime ToDateTime(DateOnly date)
        {
            return date.ToDateTime(new TimeOnly() { }, DateTimeKind.Utc);
        }

        public static DateTime ToDateTime(DateOnly? date)
        {
            if (date.HasValue) return date.Value.ToDateTime(new TimeOnly() { });
            else return DateTime.MinValue;
        }

        public static DateTime ToDateTimeInIST(DateTime localTime)
        {
            TimeZoneInfo istZone = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
            DateTime istTime = TimeZoneInfo.ConvertTime(localTime, istZone);

            return istTime;
        }

        public static DateTime ToDateTimeInISO(DateOnly date)
        {
            var dateTime = date.ToDateTime(new TimeOnly() { });
            string isoFormat = dateTime.ToString("o");
            return DateTime.Parse(isoFormat);
        }

        public static string ToDateTimeInISOString(DateOnly? date)
        {
            if (date == null) return DateTime.MinValue.ToString("o");

            var dateTime = date.Value.ToDateTime(new TimeOnly() { });
            return dateTime.ToString("o");
        }

        public static TimeOnly ToTimeOnly(string? date)
        {
            if (date == null) return TimeOnly.MinValue;

            TimeOnly timeOnly = TimeOnly.ParseExact(date, "hh:mm tt", CultureInfo.InvariantCulture);
            return timeOnly;

        }

        public static DateTime RemoveTime(DateTime dateTime)
        {
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, DateTimeKind.Utc);
            return date;
        }

        public static DateTime RemoveSeconds(DateTime dateTime)
        {
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, DateTimeKind.Utc);
            return date;
        }

        public static DateTime UnixMillisecondsToDateTime(long timestamp, bool local = false)
        {
            var offset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
            return local ? offset.LocalDateTime : offset.UtcDateTime;
        }
    }
}
