using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressBarDialogue : Dialogue
{
    [field: SerializeField] private float fillTime = 2;
    [field: SerializeField] private int barCharLength = 60;

    [field: SerializeField] private string progressBarPrefix = "|";
    [field: SerializeField] private string progressBarAffix = "|";
    [field: SerializeField] private char progressBarFillChar = '█';
    [field: SerializeField] private char progressBarEmptyChar = ' ';

    [field: SerializeField] private string completionAffix = " Done";

    private float time;

    public override void Progress(float deltaTime)
    {
        time += deltaTime;
        int barFillLength = Mathf.FloorToInt(time/fillTime * barCharLength);

        if(time >= fillTime)
        {
            text = progressBarPrefix + new string(progressBarFillChar, barCharLength) + progressBarAffix + completionAffix;
            IsComplete = true;
            return;
        }

        string bar = new string(progressBarFillChar, barFillLength) + new string(progressBarEmptyChar, barCharLength - barFillLength);

        text = progressBarPrefix + bar + progressBarAffix;
    }

    public override void ResetProgress()
    {
        throw new System.NotImplementedException();
    }
}
