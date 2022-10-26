using System.Collections.Generic;
using UnityEngine;
using static VariableManager;
using static SaveManager.VariableToSave;

public class BackgroundSwapper : MonoBehaviour
{
    [SerializeField] private FakeDictionary<AllUnlockables, GameObject> backgrounds;
    private Dictionary<AllUnlockables, GameObject> backgroundDict;
    private GameObject currentBackground;

    void Start()
    {
        backgroundDict = backgrounds.ToDictionary();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        UpdateBackground(GameManager.VariableManager.GetVariable<AllUnlockables>(currentDesktopBackground), false);
    }

    public void UpdateBackground(AllUnlockables unlock, bool autosave = true)
    {
        if (currentBackground)
        {
            currentBackground.SetActive(false);
        }
        currentBackground = backgroundDict[unlock];
        currentBackground.SetActive(true);
        if (autosave)
        {
            GameManager.SaveManager.SaveToFile(false);
        }
    }
}
