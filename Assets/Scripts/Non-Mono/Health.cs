#pragma warning disable IDE1006 // Naming Styles
using System;

[Serializable]
public class Health
{
    public OriginalValue<float> health;
    public event Action onHurt;
    public event Action onDeath;
    public bool isDead = false;

    public Health(float maxHealth = 100)
    {
        health = new(maxHealth);
    }

    public void Reset()
    {
        health.Reset();
    }

    public void ReduceHealth(float amount)
    {
        health.value -= amount;
        onHurt?.Invoke();
        if (!isDead && health.value <= 0)
        {
            isDead = true;
            health.value = 0;
            onDeath?.Invoke();
        }
    }

    public float healthPercentage
    {
        get
        {
            return health.value / health.originalValue;
        }
    }
}
