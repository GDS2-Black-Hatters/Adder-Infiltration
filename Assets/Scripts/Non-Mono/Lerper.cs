#pragma warning disable IDE1006 // Naming Styles
using UnityEngine;

[System.Serializable]
public class Lerper
{
    [SerializeField] private float start; //The start value
    public float currentValue { get; protected set; } //The current value depending on the current time between start and end value.
    [SerializeField] private float end; //The destination value
    private TimeTracker timer = new(1, 1); //The timer

    public bool isLerping { get; protected set; } = false; //A flag used to check if this class is still lerping.

    public void SetValues(float startValue, float endValue, float time, bool startLerping = true)
    {
        start = startValue;
        currentValue = start;
        end = endValue;

        timer.SetTimer(1);
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
            float percentage = timer.tick / timer.timer;
            currentValue = Mathf.Lerp(start, end, percentage);
            if (percentage == 1)
            {
                Reset();
            }
        }
        return currentValue;
    }

    private void Reset()
    {
        timer.Reset();
        isLerping = false;
    }
}