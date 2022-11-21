namespace ECommerce.API.Utilities;

public static class RangeUtility
{
    public static bool Add<T>(this List<Range<T>> list, T minValue, T maxValue) where T : IComparable<T>
    {
        if (!list.HasOverlap(minValue, maxValue))
        {
            list.Add(new Range<T>(minValue, maxValue));
            return true;
        }

        return false;
    }

    public static bool Remove<T>(this List<Range<T>> list, T inputMin, T inputMax) where T : IComparable<T>
    {
        var item = list.FirstOrDefault(p => p.Min.CompareTo(inputMin) == 0 && p.Max.CompareTo(inputMax) == 0);
        if (item != null)
            return list.Remove(item);
        return false;
    }

    public static bool Remove<T>(this List<Range<T>> list, Range<T> input) where T : IComparable<T>
    {
        return list.Remove(input.Min, input.Max);
    }

    public static bool HasOverlap<T>(T inputMin1, T inputMax1, T inputMin2, T inputMax2) where T : IComparable<T>
    {
        if (inputMin2.CompareTo(inputMin1) >= 0 && inputMin2.CompareTo(inputMax1) <= 0 ||
            inputMax2.CompareTo(inputMin1) >= 0 && inputMax2.CompareTo(inputMax1) <= 0 ||
            inputMin2.CompareTo(inputMin1) <= 0 && inputMax2.CompareTo(inputMax1) >= 0)
            return true;

        return false;
    }

    public static bool HasOverlap<T>(this List<Range<T>> list, T inputMin, T inputMax) where T : IComparable<T>
    {
        foreach (var item in list)
            if (HasOverlap(item.Min, item.Max, inputMin, inputMax))
                return true;

        return false;
    }

    public static bool HasOverlap<T>(this List<Range<T>> list, Range<T> input) where T : IComparable<T>
    {
        return list.HasOverlap(input.Min, input.Max);
    }
}

public class Range<T>
{
    public Range(T min, T max)
    {
        Min = min;
        Max = max;
    }

    public T Min { get; set; }
    public T Max { get; set; }
}