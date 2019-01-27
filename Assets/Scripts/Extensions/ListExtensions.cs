using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static T Random<T>(this List<T> list)
    {
        var index = UnityEngine.Random.Range(0, list.Count);
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
            var random = UnityEngine.Random.Range(i, count);
            var temp = list[i];
            list[i] = list[random];
            list[random] = temp;
        }
    }
}