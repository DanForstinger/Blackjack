using System.Collections.Generic;
using System;

public static class ListExtensions
{
    private static Random random = new Random();

    public static T Random<T>(this List<T> list)
    {
        var index = random.Next(0, list.Count);
        return list[index];
    }

    public static T Draw<T>(this List<T> list)
    {
        var result = list.Random();
        list.Remove(result);
        return result;
    }

    public static void Shuffle<T>(this List<T> list)
    {
        var count = list.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var rand = random.Next(i, count);
            var temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }
}