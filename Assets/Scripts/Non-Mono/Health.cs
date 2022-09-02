using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health
{
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public Health(float maxHealth = 100)
    {
        this.maxHealth = maxHealth;
        currentHealth = maxHealth;
    }

    public void Reset()
    {
        currentHealth = maxHealth;
    }

    public void ReduceHealth(float amount)
    {
        currentHealth -= amount;
    }

    public float healthPercentage{
        get{
            return currentHealth/maxHealth;
        }
    }

    /// <summary>
    /// Updates the timer.
    /// </summary>
    /// <param name="deltaTime">Pass through the UnityEngine.Time.deltaTime or another controlled time variable</param>
    public void Update(float deltaTime)
    {

    }
}
