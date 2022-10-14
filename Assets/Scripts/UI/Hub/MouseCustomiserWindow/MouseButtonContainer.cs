using UnityEngine;

public class MouseButtonContainer : BaseButtonContainer
{
    [SerializeField] private MouseList mouseList;

    private void Awake()
    {
        foreach (Mouse mouse in mouseList.Mouses)
        {
            GameObject go = Instantiate(BaseItem, transform);
            go.AddComponent<MouseChangeButton>().StartUp(this, mouse);
            //go.AddComponent<UnlockableObject>().StartUp(mouse.Unlockable);
        }
    }
}
