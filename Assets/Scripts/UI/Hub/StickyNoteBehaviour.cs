using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class StickyNoteBehaviour : MonoBehaviour
{
    [SerializeField] private Color[] colorPool;
    [SerializeField] private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        GetComponent<Image>().color = colorPool[Random.Range(0, colorPool.Length)];
    }

    public void UpdateText(string text)
    {
        textMesh.text = text;
    }
}
