using System.Globalization;

namespace Untitled.Helper;

public static class DateHelper
{
    public static DateTime ToDateTime(string date)
    {
        try
        {
            return DateTime.ParseExact(date, "yyyyMMdd", CultureInfo.CurrentCulture);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return DateTime.Now;
        }
    }
}