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
    [SerializeField] private UnityEvent OnFinish;
    [SerializeField] protected AK.Wwise.Event typeSFXEvent;

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

        textBoxContents[activeDialogueIndex].dialogue.Progress(Time.deltaTime);
        textBox.text = completedText + textBoxContents[activeDialogueIndex].dialogue.text;

        if (textBoxContents[activeDialogueIndex].dialogue.IsComplete)
        {
            delayProgress = 0;
            completedText += textBoxContents[activeDialogueIndex].dialogue.text;
            typeSFXEvent.Post(gameObject);
            activeDialogueIndex++;
            if (activeDialogueIndex >= textBoxContents.Length)
            {
                textBox.text = completedText;
                OnFinish.Invoke();
                enabled = false;
            }
        }
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
