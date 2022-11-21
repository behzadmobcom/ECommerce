namespace ECommerce.API.Utilities;

public static class DateTimeHelper
{
    public static string DefaultDatetimeFormat = "yyyy-mm-dd hh:mm tt";

    public static DateTime FirstTimeOfHour(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0);
    }

    public static DateTime LastTimeOfHour(this DateTime dt)
    {
        return dt.FirstTimeOfHour().AddHours(1).AddTicks(-1);
    }

    public static DateTime FirstTimeOfDay(this DateTime dt)
    {
        return new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0);
    }

    public static DateTime LastTimeOfDay(this DateTime dt)
    {
        return dt.FirstTimeOfDay().AddDays(1).AddTicks(-1);
    }

    public static DateTime FirstTimeOfWeek(this DateTime dt)
    {
        switch (dt.DayOfWeek)
        {
            case DayOfWeek.Monday:
                return FirstTimeOfDay(dt);
            case DayOfWeek.Tuesday:
                return dt.AddDays(-1).FirstTimeOfDay();
            case DayOfWeek.Wednesday:
                return dt.AddDays(-2).FirstTimeOfDay();
            case DayOfWeek.Thursday:
                return dt.AddDays(-3).FirstTimeOfDay();
            case DayOfWeek.Friday:
                return dt.AddDays(-4).FirstTimeOfDay();
            case DayOfWeek.Saturday:
                return dt.AddDays(-5).FirstTimeOfDay();
            case DayOfWeek.Sunday:
                return dt.AddDays(-6).FirstTimeOfDay();
            default:
                return dt;
        }
    }

    public static DateTime LastTimeOfWeek(this DateTime dt)
    {
        switch (dt.DayOfWeek)
        {
            case DayOfWeek.Monday:
                return dt.AddDays(6).LastTimeOfDay();
            case DayOfWeek.Tuesday:
                return dt.AddDays(5).LastTimeOfDay();
            case DayOfWeek.Wednesday:
                return dt.AddDays(4).LastTimeOfDay();
            case DayOfWeek.Thursday:
                return dt.AddDays(3).LastTimeOfDay();
            case DayOfWeek.Friday:
                return dt.AddDays(2).LastTimeOfDay();
            case DayOfWeek.Saturday:
                return dt.AddDays(1).LastTimeOfDay();
            case DayOfWeek.Sunday:
                return dt.LastTimeOfDay();
            default:
                return dt;
        }
    }

    public static DateTime FirstTimeOfMonth(this DateTime dt)
    {
        var ret = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0);
        return ret;
    }

    public static DateTime LastTimeOfMonth(this DateTime dt)
    {
        var days = DateTime.DaysInMonth(dt.Year, dt.Month);

        var ret = new DateTime(dt.Year, dt.Month, days);
        var dd = ret.AddDays(1);
        var ds = dd.AddTicks(-1);
        //return dt.FirstTimeOfMonth().AddMonths(1).AddTicks(-1);
        return ds;
    }

    public static DateTime FirstTimeOfYear(this DateTime dt)
    {
        return new DateTime(dt.Year, 1, 1, 0, 0, 0);
    }

    public static DateTime LastTimeOfYear(this DateTime dt)
    {
        return dt.FirstTimeOfYear().AddYears(1).AddTicks(-1);
    }
}