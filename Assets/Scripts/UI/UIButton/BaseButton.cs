using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Button))]
public abstract class BaseButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected Button button;

    protected virtual void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    protected abstract void OnClick();

    public virtual void OnPointerEnter(PointerEventData eventData) { }
    public virtual void OnPointerExit(PointerEventData eventData) { }
}
