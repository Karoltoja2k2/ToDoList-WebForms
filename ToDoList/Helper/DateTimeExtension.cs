using System;

namespace ToDoList.Helper
{
    public static class DateTimeExtension
    {
        public static DateTime ToMonthStart(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.Month, 1);

        public static DateTime ToMonthEnd(this DateTime dateTime) =>
           dateTime.AddMonths(1).ToMonthStart().AddDays(-1).Date;
    }
}