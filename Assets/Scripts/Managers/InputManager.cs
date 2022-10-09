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

        return this;
    }

    public void SetControlScheme(ControlScheme scheme)
    {
        playerInput.SwitchCurrentActionMap(DoStatic.EnumToString(scheme));
    }
}
