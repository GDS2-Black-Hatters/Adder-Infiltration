using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public sealed class InputManager : BaseManager
{
    public enum ControlScheme
    {
        Hub,
        MainGame,
        Phishing,
    }

    #region Hub Controls
    public static InputAction HubMove { get; private set; }
    public static InputAction HubClick { get; private set; }
    #endregion
    #region Main Game Controls
    public static InputAction MainGameMove { get; private set; }
    public static InputAction MainGameLook { get; private set; }
    public static InputAction MainGamePause { get; private set; }
    public static InputAction MainGameInteract { get; private set; }
    public static InputAction MainGameAbility { get; private set; }
    public static InputAction MainGameScroll { get; private set; }
    public static InputAction MainGameTab { get; private set; }
    #endregion
    #region Minigame Controls
    public static InputAction Phish { get; private set; }
    #endregion

    private PlayerInput playerInput;

    public override BaseManager StartUp()
    {
        playerInput = GetComponent<PlayerInput>();

        SetControlScheme(ControlScheme.Hub);
        HubMove = playerInput.actions["Move"];
        HubClick = playerInput.actions["Click"];

        SetControlScheme(ControlScheme.MainGame);
        MainGameMove = playerInput.actions["Move"];
        MainGameLook = playerInput.actions["Look"];
        MainGamePause = playerInput.actions["Pause"];
        MainGameInteract = playerInput.actions["Interact"];
        MainGameAbility = playerInput.actions["Ability"];
        MainGameScroll = playerInput.actions["Scroll"];
        MainGameTab = playerInput.actions["Tab"];
        
        SetControlScheme(ControlScheme.Phishing);
        Phish = playerInput.actions["Phish"];

        VariableManager var = GameManager.VariableManager;
        UpdateMouse(var.MouseList.MouseDictionary[var.GetVariable<VariableManager.AllUnlockables>(SaveManager.VariableToSave.mouseSprite)]);
        return this;
    }

    public void SetControlScheme(ControlScheme scheme)
    {
        playerInput.SwitchCurrentActionMap(DoStatic.EnumToString(scheme));
    }

    public void UpdateMouse(Mouse mouse)
    {
        StopAllCoroutines();
        Cursor.SetCursor(mouse.Frames[0], Vector2.zero, CursorMode.Auto);
        if (mouse.Frames.Length > 1)
        {
            StartCoroutine(Animate(mouse));
        }
    }

    private IEnumerator Animate(Mouse mouse)
    {
        TimeTracker tracker = new(1 / mouse.FPS);
        int index = 0;
        tracker.onFinish += () =>
        {
            index = (index + 1) % mouse.Frames.Length;
            Cursor.SetCursor(mouse.Frames[index], Vector2.zero, CursorMode.Auto);
            tracker.Reset();
        };
        while (true)
        {
            yield return null;
            tracker.Update(Time.unscaledDeltaTime);
        }
    }
}
