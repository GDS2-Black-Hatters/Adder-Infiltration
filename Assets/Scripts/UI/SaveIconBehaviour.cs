#pragma warning disable IDE0051 // Remove unused private members
using UnityEngine;
using UnityEngine.UI;

public class SaveIconBehaviour : MonoBehaviour
{
    private Image image;
    private Animator animator;

    // Start is called before the first frame update
    private void Awake()
    {
        image = GetComponent<Image>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("FinishedSaving", true); //Should set according to SaveManager.
    }

    private void OnEnable()
    {
        image.color = DoStatic.RandomColor();
        animator.Play("SaveIn");
        animator.SetBool("FinishedSaving", false);
    }

    /// <summary>
    /// Used in the animator.
    /// </summary>
    private void Finished()
    {
        gameObject.SetActive(false);
    }
}
