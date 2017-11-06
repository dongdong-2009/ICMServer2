using System;

namespace ICMServer
{
    public static class Time
    {
        public static int DateTimeToUnixTimestamp(DateTime dateTime)
        {
            return (int)(TimeZoneInfo.ConvertTimeToUtc(dateTime) -
                   new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc)).TotalSeconds;
        }

        public static string GetSevenCharsHexTimestamp()
        {
            string timeStamp = DateTimeToUnixTimestamp(DateTime.Now).ToString("X7");
            return timeStamp.Substring(Math.Max(timeStamp.Length - 7, 0), Math.Min(7, timeStamp.Length));
        }
    }
}
