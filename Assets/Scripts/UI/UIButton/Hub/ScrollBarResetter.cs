using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Scrollbar))]
public class ScrollBarResetter : MonoBehaviour
{
    private Scrollbar scrollbar;

    private void Awake()
    {
        scrollbar = GetComponent<Scrollbar>();
    }

    private void OnEnable()
    {
        scrollbar.value = 1;
    }
}
