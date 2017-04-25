using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dashboard
{
    public static class Dates
    {
        public const int UNF_FISCAL_START_DATE_MONTH = 7;   // July
        public const int UNF_FISCAL_START_DATE_DAY = 1;     // July 1st
        public const int UNF_FISCAL_END_DATE_MONTH = 6;     // June
        public const int UNF_FISCAL_END_DATE_DAY = 30;      // June 30th
		public const string DATE_FORMAT = "MM-dd-yyyy";

        public static DateTime GetFiscalYearStart(int year, int month, int day)
        {
            // if month is July or later, the FY started 7/1 of this year
            // else it started 7-1 of last year
            return (month > UNF_FISCAL_START_DATE_MONTH)
                ? new DateTime(year, UNF_FISCAL_START_DATE_MONTH, UNF_FISCAL_START_DATE_DAY)
                : new DateTime(year - 1, UNF_FISCAL_START_DATE_MONTH, UNF_FISCAL_START_DATE_DAY);
        }

        public static string GetFiscalYearStartString(int year)
        {
            return "";
        }

        public static DateTime Morning(DateTime dt)
        {
            return dt.AddHours(-23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime Tonight(DateTime dt)
        {
            return dt.AddHours(23).AddMinutes(59).AddSeconds(59);
        }

        public static DateTime GetFiscalYearEnd(int year, int month, int day)
        {
            // if month is July or later, the FY ends 6/30 next year
            // else it ends 6/30 of this year
            return month > UNF_FISCAL_START_DATE_MONTH
                ? new DateTime(year + 1, UNF_FISCAL_END_DATE_MONTH, UNF_FISCAL_END_DATE_DAY)
                : new DateTime(year, UNF_FISCAL_END_DATE_MONTH, UNF_FISCAL_END_DATE_DAY);
        }

        /// <summary>
        /// Gets the first day of the current year.
        /// </summary>
        public static DateTime GetFirstDayOfCalendarYear()
        {
            return GetFirstDayOfCalendarYear(DateTime.Today);
        }

        /// <summary>
        /// Finds the first day of year of the specified day.
        /// </summary>
        public static DateTime GetFirstDayOfCalendarYear(DateTime year)
        {
            return new DateTime(year.Year, 1, 1);
        }

        /// <summary>
        /// Gets the next day, tomorrow.
        /// </summary>
        public static DateTime GetTomorrow()
        {
            return DateTime.Today.AddDays(1);
        }

        /// <summary>
        /// Gets the previous day to the current day.
        /// </summary>
        public static DateTime GetYesterday()
        {
            // Add -1 to now
            return DateTime.Today.AddDays(-1);
        }

        /// <summary>
        /// Finds the last day of the year for today.
        /// </summary>
        public static DateTime GetLastDayOfCalendarYear()
        {
            return GetLastDayOfCalendarYear(DateTime.Today);
        }

        /// <summary>
        /// Finds the last day of the year for the selected day's year.
        /// </summary>
        public static DateTime GetLastDayOfCalendarYear(DateTime date)
        {
            // 1) Get first of next year
            DateTime n = new DateTime(date.Year + 1, 1, 1);

            // 2) Subtract 1 from it
            return n.AddDays(-1);
        }

        /// <summary>
        /// Gets the first day of the last week (7 days ago)
        /// </summary>
        /// <returns></returns>
        public static DateTime GetBeginningOfLastWeek()
        {
            return DateTime.Today.AddDays(-7);
        }

        public static DateTime GetMondayOfCurrentWeek()
        {
            DateTime today = DateTime.Today;
            int delta = DayOfWeek.Monday - today.DayOfWeek;
            DateTime monday = today.AddDays(delta);

            return monday;
        }

        public static DateTime PreviousMonday(this DateTime dt)
        {
            var dateDayOfWeek = (int)dt.DayOfWeek;
            if (dateDayOfWeek == 0)
            {
                dateDayOfWeek = dateDayOfWeek + 7;
            }
            var alterNumber = dateDayOfWeek - ((dateDayOfWeek * 2) - 1);

            return dt.AddDays(alterNumber);
        }

        public static int GetTaxWeek(this DateTime dt)
        {
            var startTaxYear = GetActualWeekNumber(new DateTime(DateTime.Now.Year, 7, 1));
            var thisWeekNumber = GetActualWeekNumber(dt);
            var difference = thisWeekNumber - startTaxYear;

            return difference < 0 ? 53 + difference : difference + 1;
        }

        public static int PeriodWeek(this DateTime dt)
        {
            var rawPeriodWeek = GetTaxWeek(dt) % 4;

            return rawPeriodWeek == 3 ? 1 : rawPeriodWeek + 2;
        }

        private static int GetActualWeekNumber(DateTime dt)
        {
            var ci = System.Threading.Thread.CurrentThread.CurrentCulture;
            var cal = ci.Calendar;
            var calWeekRule = ci.DateTimeFormat.CalendarWeekRule;
            var fDoW = ci.DateTimeFormat.FirstDayOfWeek;

            return cal.GetWeekOfYear(dt, calWeekRule, fDoW);
        }

        /// <summary>
        /// Get the beginning day of the current week
        /// </summary>
        /// <returns></returns>
        public static DateTime GetBeginningOfWeek()
        {
            return DateTime.Today.AddDays(-6);
        }

        /// <summary>
        /// Get's the same day one year ago
        /// </summary>
        /// <returns></returns>
        public static DateTime GetLastCalendarYear()
        {
            return DateTime.Today.AddYears(-1);
        }

        /// <summary>
        /// Get's the start of the week
        /// 
        /// Ex. DateTime dt = DateTime.Now.StartOfWeek(DayOfWeek.Monday);
        ///     DateTime dt = DateTime.Now.StartOfWeek(DayOfWeek.Sunday);
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startOfWeek"></param>
        /// <returns></returns>
        public static DateTime GetStartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }

        public static List<SelectListItem> GetTenYearDropdown()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            bool bSelected = false;

            for (int i = -5; i < 1; i++)
            {
                string year = DateTime.Today.AddYears(i).Year.ToString();

                bSelected = (year.CompareTo(DateTime.Today.Year.ToString()) == 0) ? true : false;

                items.Add(new SelectListItem { Text = year, Value = year, Selected = bSelected });
            }

            return items;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SelectListItem> GetMonthDropdown()
        {
            List<SelectListItem> items = new List<SelectListItem>();

            int month = DateTime.Today.Month;
            bool bSelected = false;
            string text = "", value = "";

            for (int i = 1; i < 13; i++)
            {
                value = i.ToString();

                bSelected = (i == DateTime.Today.Month) ? true : false;

                switch (i)
                {
                    case 1: text = "01 - January";  break;
                    case 2: text = "02 - February"; break;
                    case 3: text = "03 - March"; break;
                    case 4: text = "04 - April"; break;
                    case 5: text = "05 - May"; break;
                    case 6: text = "06 - June"; break;
                    case 7: text = "07 - July"; break;
                    case 8: text = "08 - August"; break;
                    case 9: text = "09 - September"; break;
                    case 10: text = "10 - October"; break;
                    case 11: text = "11 - November"; break;
                    case 12: text = "12 - December"; break;
                }

                // Add the item to the list
                items.Add(new SelectListItem { Text = text, Value = value, Selected = bSelected });
            }

            return items;
        }
    }
}