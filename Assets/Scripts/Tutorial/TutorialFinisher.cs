using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class TutorialFinisher : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Interactable>().AddInteraction(GameManager.VariableManager.FinishTutorial);
    }
}
