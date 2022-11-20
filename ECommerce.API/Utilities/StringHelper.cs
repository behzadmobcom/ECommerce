using System.Text.RegularExpressions;

namespace ECommerce.API.Utilities;

public static class StringHelper
{
    public static bool HasValue(this string value)
    {
        return !(string.IsNullOrWhiteSpace(value) || string.IsNullOrEmpty(value));
    }

    public static string GetDigits(this string input)
    {
        if (input.HasValue()) return Regex.Match(input, @"[\d-]").Value;

        return null;
    }

    public static string RemoveDigits(this string input)
    {
        if (input.HasValue()) return Regex.Replace(input, @"[\d-]", string.Empty);

        return null;
    }

    public static bool IsInFormat(this string input, string format)
    {
        if (input.HasValue() && format.HasValue())
            if (input.Length == format.Length)
            {
                for (var i = 0; i < input.Length; i++)
                    if (input[i].Equals('x') && format[i].Equals('-') || input[i].Equals('-') && format[i].Equals('x'))
                        return false;

                return true;
            }

        return false;
    }

    public static string RemoveMasks(this string input)
    {
        if (input.HasValue()) return input.Replace("-", "").Replace("(", "").Replace(")", "");

        return null;
    }

    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source.IndexOf(toCheck, comp) >= 0;
    }

    public static string NullIfEmpty(this string str)
    {
        return str == "" ? null : str;
    }

    public static string EmptyIfNull(this string str)
    {
        return str ?? "";
    }

    public static string Summary(this string str, int lenght)
    {
        if (str.Length < lenght)
            return str;
        return str.Substring(0, lenght - 4) + " ...";
    }

    public static string Summary(this string str, int lenght, string searchText)
    {
        var j = 0;

        try
        {
            str = str.Replace("●", " ");
            for (var i = 1; i <= str.Length; i++)
            {
                if (i > str.Length) break;
                j += 1;
                if (str.Substring(j - 1, 1) == "<")
                {
                    var Temp = str.Substring(j - 1, 1);
                    j += 1;
                    while (str.Substring(j - 1, 1) != ">")
                    {
                        Temp += str.Substring(j - 1, 1);
                        j += 1;
                    }

                    Temp += str.Substring(j - 1, 1);
                    str = str.Replace(Temp, "");
                    j = 0;
                    i = 0;
                }
            }
        }
        catch
        {
        }

        if (searchText.HasValue())
            str = str.Replace(searchText, "<b><span style='color:red'>" + searchText + "</span></b>");
        if (str.Length < lenght)
            return str;
        return str.Substring(0, lenght);
    }

    public static bool In(this string str, params string[] stringValues)
    {
        foreach (var item in stringValues)
            if (string.Compare(str, item) == 0)
                return true;

        return false;
    }

    public static string Right(this string str, int length)
    {
        return str != null && str.Length > length ? str.Substring(str.Length - length) : str;
    }

    public static string Left(this string str, int length)
    {
        return str != null && str.Length > length ? str.Substring(0, length) : str;
    }

    public static string RemoveLeft(this string str, int length)
    {
        return str.Remove(0, length);
    }

    public static string RemoveRight(this string str, int length)
    {
        return str.Remove(str.Length - length);
    }

    public static string CleanString(this string str)
    {
        return str.Trim().RemoveWhiteSpaces().NullIfEmpty();
    }

    public static string RemoveLetters(this string text)
    {
        if (text.HasValue()) return Regex.Replace(text, "[^0-9.]", "");

        return null;
    }

    public static string RemoveWhiteSpaces(this string input)
    {
        while (input.Contains("  ")) input = input.Replace("  ", " ");

        return input;
    }

    public static string Take(this string input, int count)
    {
        if (input.Length > count) return input.Substring(0, count);

        return input;
    }

    public static string ShowDurationinHourFormat(int duration)
    {
        var returnString = "";
        try
        {
            var hours = 0;
            hours = duration / 60;
            var minutes = 0;
            minutes = duration % 60;

            if (hours > 0)
                returnString += $"{hours}h ";

            if (minutes > 0)
                returnString += $"{minutes}min";
        }
        catch
        {
        }

        return returnString;
    }
}