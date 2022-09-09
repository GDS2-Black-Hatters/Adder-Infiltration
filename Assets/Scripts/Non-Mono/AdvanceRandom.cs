using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdvanceRandom
{
    /// <summary>
    /// Get a random number with an exponential distribution
    /// </summary>
    /// <param name="baseValue">The base number for adding to the exponential result, treat like minimum return value.</param>
    /// <param name="expectation">The expected range where most results should lie within, baseValue will act like a MaxValue if expectation is negative.</param>
    /// <returns>A random number following baseValue + exponential distribution.</returns>
    public static float ExponentialRandom(float baseValue, float expectation)
    {
        return baseValue + (Mathf.Log(1 - Random.value) / -(1/expectation));
    }

    /// <summary>
    /// Generates normally distributed numbers. Each operation makes two Gaussians for the price of one, and apparently they can be cached or something for better performance, but who cares.
    /// </summary>
    /// <param name = "mu">Mean of the distribution</param>
    /// <param name = "sigma">Standard deviation</param>
    /// <returns></returns>
    public static float GaussianRandom(float mu = 0, float sigma = 1)
    {
        float u1 = Random.value;
        float u2 = Random.value;

        float randStdNormal = Mathf.Sqrt(-2 * Mathf.Log(u1)) * Mathf.Sin(2 * Mathf.PI * u2);
        return mu + sigma * randStdNormal;
    }
}
