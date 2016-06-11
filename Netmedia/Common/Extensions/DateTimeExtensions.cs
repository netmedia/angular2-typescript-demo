using System;

namespace Netmedia.Common.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime AddWeeks(this DateTime date, int weeksCount) 
        { 
            return date.AddDays(7 * weeksCount); 
        }
        public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
        {
            var start = (int)from.DayOfWeek;
            var target = (int)dayOfWeek - 1;
            if (target <= start)
                target += 7;

            return from.AddDays(target - start);
        }

        /// <summary>
        /// Prints year, month, day, hour, minutes and seconds like 20150208164025
        /// </summary>
        public static string Timestamp(this DateTime datetime)
        {
            var year = datetime.Year;
            var month = datetime.Month < 10 ? ("0" + datetime.Month) : datetime.Month.ToString();
            var day = datetime.Day < 10 ? ("0" + datetime.Day) : datetime.Day.ToString();
            var hour = datetime.Hour < 10 ? ("0" + datetime.Hour) : datetime.Hour.ToString();
            var minutes = datetime.Minute < 10 ? ("0" + datetime.Minute) : datetime.Minute.ToString();
            var seconds = datetime.Second < 10 ? ("0" + datetime.Second) : datetime.Second.ToString();
            return string.Format("{0}{1}{2}{3}{4}{5}", year, month, day, hour, minutes, seconds);
        }

        /// <summary>
        /// Prints year, month and day like 20150208
        /// </summary>
        public static string ShortTimestamp(this DateTime datetime)
        {
            var year = datetime.Year;
            var month = datetime.Month < 10 ? ("0" + datetime.Month) : datetime.Month.ToString();
            var day = datetime.Day < 10 ? ("0" + datetime.Day) : datetime.Day.ToString();
            return string.Format("{0}{1}{2}", year, month, day);
        }

        public static DateTime TruncateToHoursOnly(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, datetime.Hour, 0, 0);
        }
    }
}