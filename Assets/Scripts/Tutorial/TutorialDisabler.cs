using UnityEngine;

public class TutorialDisabler : MonoBehaviour
{
    private void Awake()
    {
        if (!GameManager.VariableManager.GetVariable<bool>(SaveManager.VariableToSave.tutorialFinish))
        {
            gameObject.SetActive(false);
        }
    }
}
