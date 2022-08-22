using System.Collections.Generic;
using UnityEngine;

public class DoStatic
{
    public delegate void SimpleDelegate();

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
        return Random.value > 1 - successRate;
    }

    /// <summary>
    /// Gets all the children in the given gameobject.
    /// This is a recursive function with utilising the Depth First Search algorithm.
    /// </summary>
    /// <param name="transform">The transform of the gameobject.</param>
    /// <param name="generationDepth">The depth of the search.</param>
    /// <param name="childrenRef">Starting children, if any, mainly used for generation depth.</param>
    /// <returns>An array of all the children.</returns>
    public static Transform[] GetChildren(Transform transform, int generationDepth = 1, List<Transform> childrenRef = null)
    {
        generationDepth--;
        List<Transform> children = childrenRef ?? new List<Transform>();
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            children.Add(child);
            if (generationDepth > 0 && child.childCount > 0)
            {
                GetChildren(child, generationDepth, children);
            }
        }
        return children.ToArray();
    }

    /// <summary>
    /// Get a child with tag. Utilises GetChildren() meaning, it is a recursive function.
    /// </summary>
    /// <param name="tag">The first tag to find</param>
    /// <param name="parent">The parent to search through.</param>
    /// <param name="generationDepth">The depth of the search.</param>
    /// <returns>The child with the corresponding tag, may return a null.</returns>
    public static GameObject GetChildWithTag(string tag, Transform parent, int generationDepth = 1)
    {
        foreach (Transform child in GetChildren(parent, generationDepth))
        {
            if (child.CompareTag(tag))
            {
                return child.gameObject;
            }
        }
        return null;
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
}