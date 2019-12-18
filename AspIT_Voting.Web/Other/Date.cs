using System;
using System.Globalization;

namespace AspIT_Voting.Web.Other
{
    public class Date
    {
        public int GetWeekOfYear()
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date = DateTime.Now;
            Calendar cal = dfi.Calendar;

            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        private int GetWeekOfYear(DateTime dateTime)
        {
            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
            DateTime date = dateTime;
            Calendar cal = dfi.Calendar;

            return cal.GetWeekOfYear(date, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
        }

        public bool DateRange(DayOfWeek start, DayOfWeek end)
        {
            DayOfWeek today = DateTime.Today.DayOfWeek;

            return GetWeekOfYear() % 2 == 0 && DateTime.Now.Hour >= 8 && start <= today && today <= end && DateTime.Now.Hour <= 15;
        }

        public bool DateRange(DateTime date, DayOfWeek start, DayOfWeek end)
        {           
            DayOfWeek dayOfWeek = date.DayOfWeek;

            return GetWeekOfYear(date) % 2 == 0 && DateTime.Now.Hour >= 8 && start <= dayOfWeek && dayOfWeek <= end && DateTime.Now.Hour <= 15;
        }
    }
}
