using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DoStatic
{
    /// <summary>
    /// Get a random colour.
    /// </summary>
    /// <returns>A random colour.</returns>
    public static Color RandomColor()
    {
        return new Color(Random.value, Random.value, Random.value);
    }

    /// <summary>
    /// Get a random bool.
    /// </summary>
    /// <param name="successRate">The successRate. Defaults to 50%. Should be between 0 to 1</param>
    /// <returns>A random bool.</returns>
    public static bool RandomBool(float successRate = 0.5f)
    {
        return Random.value < successRate;
    }

    /// <summary>
    /// Swaps the values between two variables.
    /// </summary>
    /// <param name="a">The first variable</param>
    /// <param name="b">The second variable</param>
    public static void Swap<T>(ref T a, ref T b)
    {
        (b, a) = (a, b);
    }

    /// <summary>
    /// Get the rotation to look at. (Works for 2D, maybe not 3D)
    /// </summary>
    /// <param name="from">The starting position to look</param>
    /// <param name="target">The target position to look</param>
    public static void LookAt(Transform from, Vector3 target)
    {
        from.right = target - from.position;
    }

    /// <summary>
    /// Shuffles the given array.
    /// </summary>
    /// <typeparam name="T">Any dayatype</typeparam>
    /// <param name="arr">An array.</param>
    public static void ShuffleArray<T>(T[] arr)
    {
        for (int element = 0; element < arr.Length; element++)
        {
            Swap(ref arr[element], ref arr[Random.Range(0, arr.Length)]);
        }
    }

    /// <summary>
    /// Convert a given enum value into string.
    /// </summary>
    /// <typeparam name="T">Any enumerator type.</typeparam>
    /// <param name="enumerator">The enum to convert to string.</param>
    /// <returns>The name of the enum value.</returns>
    public static string EnumAsString<T>(T enumerator) where T : System.Enum
    {
        return System.Enum.GetName(typeof(T), enumerator);
    }

    /// <summary>
    /// Convert a given string into the enum value.
    /// </summary>
    /// <typeparam name="T">Any enumerator type</typeparam>
    /// <param name="enumerator">The string to convert into a enumerator.</param>
    /// <returns>The enum value.</returns>
    public static T StringToEnum<T>(string enumerator) where T : System.Enum
    {
        return (T)System.Enum.Parse(typeof(T), enumerator);
    }

    /// <summary>
    /// Get a list of all the enum values.
    /// </summary>
    /// <typeparam name="T">Any enumerator type</typeparam>
    /// <returns>An array of all the enums values.</returns>
    public static T[] EnumList<T>() where T : System.Enum
    {
        return System.Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }

    /// <summary>
    /// Get the nearest given number from given value.
    /// </summary>
    /// <param name="value">The value to round up or down</param>
    /// <param name="nearest">The value where the value is rounded to become divisible</param>
    /// <returns>A rounded value</returns>
    public static float RoundToNearestFloat(float value, float nearest = 1)
    {
        float fractional = 1 / nearest;
        return Mathf.Round(value * fractional) / fractional;
    }
}