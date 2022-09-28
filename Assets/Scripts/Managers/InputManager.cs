using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public sealed class InputManager : MonoBehaviour, IManager
{
    #region Enums
    public enum ControlScheme
    {
        Hub,
        MainGame
    }

    public enum Controls
    {
        //Hub
        Move, //Hub (Mouse movement) || MainGame (WASD/Arrow Keys)
        Click,

        //MainGame
        Look,
        Pause,
        Interact,
        Ability,
        Scroll,
    }
    #endregion

    private PlayerInput playerInput;

    public void StartUp()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    /// <summary>
    /// Change the control scheme.
    /// </summary>
    /// <param name="scheme">The control scheme type.</param>
    public void ChangeControlMap(ControlScheme scheme)
    {
        playerInput.SwitchCurrentActionMap(DoStatic.EnumAsString(scheme));
    }

    /// <summary>
    /// Get the InputAction to read.
    /// </summary>
    /// <param name="control">The controls</param>
    /// <returns>Reference of the InputAction in the current control scheme to read its data.</returns>
    public InputAction GetAction(Controls control)
    {
        return playerInput.actions[DoStatic.EnumAsString(control)];
    }

    /// <summary>
    /// Is the current scheme the same as the scheme given?
    /// </summary>
    /// <param name="scheme">The control scheme.</param>
    /// <returns>True if they are the same scheme.</returns>
    public bool IsCurrentActionMap(ControlScheme scheme)
    {
        return playerInput.currentActionMap.name.Equals(DoStatic.EnumAsString(scheme));
    }
}
