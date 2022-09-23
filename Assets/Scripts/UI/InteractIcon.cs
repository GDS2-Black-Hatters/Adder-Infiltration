using UnityEngine;

public class InteractIcon : MonoBehaviour
{
    [SerializeField] private SpriteRenderer backgroundRend;
    [SerializeField] private SpriteRenderer progressRingRend;

    private void Start()
    {
        transform.rotation = Quaternion.identity;
    }

    public void UpdateProgress(float progress)
    {
        progressRingRend.material.SetFloat("_FillAmount", progress);
    }
}
