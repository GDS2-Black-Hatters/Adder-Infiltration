using UnityEngine;
using UnityEngine.InputSystem; //Required module

/// <summary>
/// Attach this to an empty GameObject
/// </summary>
public class InputManagerTutorial : MonoBehaviour
{
    private InputManager input;
    private InputAction ClickAction;
    private InputAction LookAction;
    private InputAction MoveAction;

    private void Start()
    {
        input = GameManager.InputManager; //Just for short typing.

        ClickAction = input.GetAction(InputManager.Controls.Click);
        LookAction = input.GetAction(InputManager.Controls.Look);

        ///NOTES:
        /// -   InputManager starts off in the Hub control scheme
        /// -   When grabbing an action that shares the same name across multiple schemes,
        ///     it returns a reference of the **current control scheme**.
        input.ChangeControlMap(InputManager.ControlScheme.MainGame);
        MoveAction = input.GetAction(InputManager.Controls.Move);

        input.ChangeControlMap(InputManager.ControlScheme.Hub);
    }

    private void Update()
    {
        if (ClickAction.triggered)
        {
            Debug.Log("Click Action was triggered! Changing it to Main Game Control Scheme.");
            input.ChangeControlMap(InputManager.ControlScheme.MainGame);
        }

        if (LookAction.ReadValue<Vector2>() != Vector2.zero)
        {
            Debug.Log("Main Game + Mouse Movement Detected!");
        }

        //Remember, this cannot be true unless the current control scheme is MainGame.
        if (MoveAction.ReadValue<Vector2>() != Vector2.zero)
        {
            Debug.Log("Move Action was triggered! Changing it to Hub Control Scheme.");
            input.ChangeControlMap(InputManager.ControlScheme.Hub);
        }
    }
}
