using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager.Controls;
using static InputManager.ControlScheme;

public class PlayerVirusController : MonoBehaviour
{
    private ActionInputSubscriber ais;

    public System.Action<Vector2> onMovementInputUpdate;
    public System.Action<Vector2> onLookInputUpdate;
    public System.Action onInteractStart;
    public System.Action onInteractEnd;
    public System.Action onAbilityStart;
    public System.Action onAbilityEnd;

    private void Awake()
    {
        InputManager inputManager = GameManager.InputManager;
        inputManager.ChangeControlMap(MainGame);
        
        ais = gameObject.AddComponent<ActionInputSubscriber>();
        ais.AddActions( new System.Collections.Generic.List<ActionInputSubscriber.ActionDelegate>{
            new(MainGame, GameManager.InputManager.GetAction(Move), ActionInputSubscriber.CallBackContext.Performed, moveInputChange),
            new(MainGame, GameManager.InputManager.GetAction(Move), ActionInputSubscriber.CallBackContext.Canceled, moveInputStop),
            new(MainGame, GameManager.InputManager.GetAction(Look), ActionInputSubscriber.CallBackContext.Performed, lookInputChange),
            new(MainGame, GameManager.InputManager.GetAction(Interact), ActionInputSubscriber.CallBackContext.Performed, interactStart),
            new(MainGame, GameManager.InputManager.GetAction(Interact), ActionInputSubscriber.CallBackContext.Canceled, interactHalt),
        });
    }

    public void SetInputEnable(bool disabled)
    {
        ais.enabled = !disabled;
    }

    private void OnEnable()
    {
        GameManager.LevelManager.onGamePauseStateChange += SetInputEnable;
    }

    private void OnDisable()
    {
        GameManager.LevelManager.onGamePauseStateChange -= SetInputEnable;
    }

    private void moveInputChange(InputAction.CallbackContext ctx)
    {
        onMovementInputUpdate?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void moveInputStop(InputAction.CallbackContext ctx)
    {
        onMovementInputUpdate?.Invoke(Vector2.zero);
    }

    private void lookInputChange(InputAction.CallbackContext ctx)
    {
        onLookInputUpdate?.Invoke(ctx.ReadValue<Vector2>());
    }

    private void interactStart(InputAction.CallbackContext ctx)
    {
        onInteractStart?.Invoke();
    }

    private void interactHalt(InputAction.CallbackContext ctx)
    {
        onInteractEnd?.Invoke();
    }

    private void triggerAbility(InputAction.CallbackContext ctx)
    {
        onAbilityStart?.Invoke();
    }
}
