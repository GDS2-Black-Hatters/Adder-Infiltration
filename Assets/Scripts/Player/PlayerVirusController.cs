using UnityEngine;
using static InputManager;
using static ActionInputSubscriber.CallbackContext;
using UnityEngine.InputSystem;

public class PlayerVirusController : MonoBehaviour
{
    private ActionInputSubscriber ais;

    public System.Action<Vector2> onMovementInputUpdate;
    public System.Action<Vector2> onLookInputUpdate;
    public System.Action onInteractStart;
    public System.Action onInteractEnd;
    public System.Action onAbilityTrigger;
    //public System.Action onAbilityTriggerEnd;
    public System.Action onAbilityPrimeStart;
    public System.Action onAbilityPrimeEnd;

    private bool abilityPrimeHeld = false;

    private void Awake()
    {
        ais = gameObject.AddComponent<ActionInputSubscriber>();
        ais.AddActions( new() {
            new(MainGameMove, Performed, moveInputChange),
            new(MainGameMove, Canceled, moveInputStop),
            new(MainGameLook, Performed, lookInputChange),
            new(MainGameInteract, Performed, primaryTrigger),
            new(MainGameInteract, Canceled, interactHalt),
            new(MainGameAbility, Performed, primeAbility),
            new(MainGameAbility, Canceled, primeAbilityEnd),
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

    private void primaryTrigger(InputAction.CallbackContext ctx)
    {
        if(abilityPrimeHeld)
        {
            triggerAbility();
        }
        else
        {
            interactStart();
        }
    }

    private void interactStart()
    {
        onInteractStart?.Invoke();
    }

    private void interactHalt(InputAction.CallbackContext ctx)
    {
        onInteractEnd?.Invoke();
    }

    private void triggerAbility()
    {
        onAbilityTrigger?.Invoke();
    }

    private void primeAbility(InputAction.CallbackContext ctx)
    {
        abilityPrimeHeld = true;
        onAbilityPrimeStart?.Invoke();
    }

    private void primeAbilityEnd(InputAction.CallbackContext ctx)
    {
        abilityPrimeHeld = false;
        onAbilityPrimeEnd?.Invoke();
    }
}
