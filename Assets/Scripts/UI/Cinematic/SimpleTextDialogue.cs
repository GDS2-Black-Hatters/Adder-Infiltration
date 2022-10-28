using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SimpleTextDialogue : Dialogue
{
    [TextArea(1, 3)]
    public string fullText;
    public float charactersPerSecond = 5;

    private float time = 0;
    private int prevIndex = 0;

    public override void Progress(float deltaTime)
    {
        time += deltaTime;
        
        int newEndIndex = Mathf.FloorToInt(time * charactersPerSecond);
        if(prevIndex != newEndIndex)
        {
            onTypeAction?.Invoke();
            prevIndex = newEndIndex;
        }

        if(newEndIndex >= fullText.Length)
        {
            text = fullText;
            IsComplete = true;
            return;
        }
        
        text = fullText.Remove(newEndIndex);
    }

    public override void ResetProgress()
    {
        time = 0;
        IsComplete = false;
    }
}
