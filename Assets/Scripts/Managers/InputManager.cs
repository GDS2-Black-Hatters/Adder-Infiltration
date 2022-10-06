using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public sealed class InputManager : BaseManager
{
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
    #endregion

    private PlayerInput playerInput;

    public override BaseManager StartUp()
    {
        playerInput = GetComponent<PlayerInput>();

        playerInput.SwitchCurrentActionMap("Hub");
        HubMove = playerInput.actions["Move"];
        HubClick = playerInput.actions["Click"];

        playerInput.SwitchCurrentActionMap("MainGame");
        MainGameMove = playerInput.actions["Move"];
        MainGameLook = playerInput.actions["Look"];
        MainGamePause = playerInput.actions["Pause"];
        MainGameInteract = playerInput.actions["Interact"];
        MainGameAbility = playerInput.actions["Ability"];
        MainGameScroll = playerInput.actions["Scroll"];

        return this;
    }
}
