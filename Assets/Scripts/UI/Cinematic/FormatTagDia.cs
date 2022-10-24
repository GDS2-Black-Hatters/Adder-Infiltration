using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormatTagDia : Dialogue
{
    [field: SerializeField] private string tag;

    public override void Progress(float deltaTime)
    {
        text = tag;
        IsComplete = true;
        return;
    }

    public override void ResetProgress()
    {
        return;
    }
}
