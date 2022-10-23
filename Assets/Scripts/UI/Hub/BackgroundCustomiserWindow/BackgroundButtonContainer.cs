using UnityEngine;

public class BackgroundButtonContainer : BaseButtonContainer
{
    [SerializeField] private BackgroundSwapper swapper;

    private void Awake()
    {
        foreach (Background background in GameManager.VariableManager.BackgroundList.Backgrounds)
        {
            GameObject go = Instantiate(BaseItem, transform);
            go.AddComponent<BackgroundChangeButton>().StartUp(this, background, swapper);
            go.AddComponent<UnlockableObject>().StartUp(background.Unlockable);
        }
    }
}