using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Dialogue
{
    public enum DialogueType
    {
        SimpleText,
        FormatTag,
        ProgressBar,

    }

    public static System.Type GetDialogueClassType(DialogueType dialogueTypeEnum)
    {
        switch (dialogueTypeEnum)
        {
            case DialogueType.FormatTag:
            return typeof(FormatTagDia);

            case DialogueType.ProgressBar:
            return typeof(ProgressBarDialogue);

            default:
            case DialogueType.SimpleText:
            return typeof(SimpleTextDialogue);
        }
    }

    public string text { get; protected set; }
    public bool IsComplete { get; protected set; }

    public abstract void ResetProgress();
    public abstract void Progress(float deltaTime);
}
