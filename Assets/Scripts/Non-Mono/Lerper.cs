#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

public class Lerper
{
    [SerializeField] private float start; //The start value
    public float currentValue { get; protected set; } //The current value depending on the current time between start and end value.
    [SerializeField] private float end; //The destination value
    private TimeTracker timer; //The timer

    public bool isLerping { get; protected set; } = false; //A flag used to check if this class is still lerping.

    public void SetValues(float startValue, float endValue, float time, bool startLerping = true)
    {
        start = startValue;
        currentValue = start;
        end = endValue;

        timer = new(time, 1);
        isLerping = startLerping;
    }

    /// <summary>
    /// Updates the lerp.
    /// </summary>
    /// <param name="deltaTime">The amount of time that has passed.</param>
    /// <returns>The new current value.</returns>
    public float Update(float deltaTime)
    {
        if (isLerping)
        {
            timer.Update(deltaTime);
            float clamp = Mathf.Clamp(timer.tick / timer.timer, 0, 1);
            currentValue = ValueAtPercentage(clamp);
            if (clamp == 1)
            {
                Reset();
            }
        }
        return currentValue;
    }

    public float ValueAtPercentage(float percentage)
    {
        return Mathf.Clamp(percentage, 0, 1) * (end - start) + start;
    }

    private void Reset()
    {
        timer.Reset();
        isLerping = false;
    }
}