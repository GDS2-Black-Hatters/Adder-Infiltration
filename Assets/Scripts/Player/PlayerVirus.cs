using UnityEngine;

public class PlayerVirus : MonoBehaviour
{
    [field: SerializeField] public PlayerVirusController virusController { get; private set; }
    [field: SerializeField] public GameObject playerVisual { get; private set; }
    private PlayerVirusMoveControl movement;

    private void Awake()
    {
        GameManager.LevelManager.ActiveSceneController.SetPlayer(this);
        movement = GetComponent<PlayerVirusMoveControl>();
    }

    public void Dash(float strength)
    {
        movement.Dash(strength);
    }
}
