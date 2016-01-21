using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TFSTeamForge.DataProcessing
{
    public static class DateTimeExtensions
    {
        private static string EST = "Eastern Standard Time";

        public static DateTimeOffset ConvertToESTDateTimeOffset(this DateTime dateTime)
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById(EST);
            return TimeZoneInfo.ConvertTime(dateTime, est);
        }
        public static DateTimeOffset ConvertToESTDateTimeOffset(this DateTimeOffset dateTimeOffset)
        {
            TimeZoneInfo est = TimeZoneInfo.FindSystemTimeZoneById(EST);
            return TimeZoneInfo.ConvertTime(dateTimeOffset, est);
        }
    }
}