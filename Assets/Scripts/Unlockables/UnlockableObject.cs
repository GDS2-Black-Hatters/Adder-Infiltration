using UnityEngine;

public class UnlockableObject : MonoBehaviour
{
    [SerializeField] private VariableManager.AllUnlockables unlockable;
    private Unlockable unlock;
    [SerializeField] private bool autoStart = false;

    void Start()
    {
        if (autoStart)
        {
            StartUp(unlockable);
        }
    }

    public void StartUp(VariableManager.AllUnlockables unlockable)
    {
        unlock = GameManager.VariableManager.GetUnlockable(unlockable);
        if (!unlock.IsUnlocked)
        {
            GameManager.VariableManager.purchaseCallback += CheckUnlock;
            gameObject.SetActive(false);
        }
    }

    private void CheckUnlock()
    {
        if (unlock.IsUnlocked)
        {
            GameManager.VariableManager.purchaseCallback -= CheckUnlock;
            gameObject.SetActive(true);
            //TODO: Sprint 5 - Do a fade in or transition in for the install.
        }
    }
}
