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

    public override void Progress(float deltaTime)
    {
        time += deltaTime;
        int endIndex = Mathf.FloorToInt(time * charactersPerSecond);
        
        if(endIndex >= fullText.Length)
        {
            text = fullText;
            IsComplete = true;
            return;
        }
        
        text = fullText.Remove(endIndex);
    }

    public override void ResetProgress()
    {
        time = 0;
        IsComplete = false;
    }
}
