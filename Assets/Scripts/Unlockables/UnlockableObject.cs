using UnityEngine;

public class UnlockableObject : MonoBehaviour
{
    [SerializeField] private VariableManager.AllUnlockables unlockable;
    private Unlockable unlock;

    void Start()
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
            //TODO: Do a fade in or transition in for the install.
        }
    }
}
