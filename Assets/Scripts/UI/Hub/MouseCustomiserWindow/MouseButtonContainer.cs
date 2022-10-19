using UnityEngine;

public class MouseButtonContainer : BaseButtonContainer
{
    private void Awake()
    {
        foreach (Mouse mouse in GameManager.VariableManager.MouseList.Mouses)
        {
            GameObject go = Instantiate(BaseItem, transform);
            go.AddComponent<MouseChangeButton>().StartUp(this, mouse);
            go.AddComponent<UnlockableObject>().StartUp(mouse.Unlockable);
        }
    }
}
