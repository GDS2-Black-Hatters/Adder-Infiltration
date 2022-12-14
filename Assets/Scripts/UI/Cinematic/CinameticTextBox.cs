using TMPro;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CinameticTextBox : MonoBehaviour
{
    [System.Serializable]
    private class TextContent
    {
        public TextContent()
        {
            dialogueType = Dialogue.DialogueType.SimpleText;
            dialogue = new SimpleTextDialogue();
            progressRate = 1;
            startDelay = 0.3f;
        }

        [field: SerializeField] public Dialogue.DialogueType dialogueType { get; private set; }
        [field: SerializeReference] public Dialogue dialogue { get; private set; }
        [field: SerializeField] public float progressRate { get; private set; }
        [field: SerializeField] public float startDelay { get; private set; }


        public void UpdateDialogueType()
        {
            dialogue = (Dialogue)System.Activator.CreateInstance(Dialogue.GetDialogueClassType(dialogueType));
        }
    }

    private TextMeshProUGUI textBox;

    [SerializeReference] private TextContent[] textBoxContents;

    private string completedText;
    private int activeDialogueIndex = 0;
    private float delayProgress = 0;
    private bool started = false;
    [SerializeField] private UnityEvent OnFinish;
    [SerializeField] protected AK.Wwise.Event typeSFXEvent;
    [SerializeField] protected AK.Wwise.Event progressBarSFXEvent;

    private void Awake()
    {
        textBox = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (textBoxContents[activeDialogueIndex].startDelay > delayProgress)
        {
            delayProgress += Time.deltaTime;
            return;
        }

        if(!started)
        {
            started = true;
            if(textBoxContents[activeDialogueIndex].dialogueType == Dialogue.DialogueType.ProgressBar)
            {
                OnProgressBarStart();
            }
        }

        textBoxContents[activeDialogueIndex].dialogue.Progress(Time.deltaTime);
        textBox.text = completedText + textBoxContents[activeDialogueIndex].dialogue.text;

        if (textBoxContents[activeDialogueIndex].dialogue.IsComplete)
        {
            started = false;
            if(textBoxContents[activeDialogueIndex].dialogueType == Dialogue.DialogueType.ProgressBar)
            {
                OnProgressBarEnd();
            }

            delayProgress = 0;
            completedText += textBoxContents[activeDialogueIndex].dialogue.text;
            activeDialogueIndex++;
            if (activeDialogueIndex >= textBoxContents.Length)
            {
                textBox.text = completedText;
                OnFinish.Invoke();
                enabled = false;
            }
            else
            {
                textBoxContents[activeDialogueIndex].dialogue.onTypeAction = OnTypeAction;
            }
        }
    }

    private void OnTypeAction()
    {
        typeSFXEvent.Post(gameObject);
    }

    private void OnProgressBarStart()
    {
        progressBarSFXEvent.Post(gameObject);
    }

    private void OnProgressBarEnd()
    {
        progressBarSFXEvent.Stop(gameObject);
    }

    private void OnValidate()
    {
        //Check Dialogue Type and change type if needed
        for (int i = 0; i < textBoxContents.Length; i++)
        {
            if (textBoxContents[i] == null)
            {
                textBoxContents[i] = new();
            }

            if (textBoxContents[i].dialogue.GetType() != Dialogue.GetDialogueClassType(textBoxContents[i].dialogueType))
            {
                textBoxContents[i].UpdateDialogueType();
            }
        }
    }
}
