using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

public sealed class ActionInputSubscriber : MonoBehaviour
{
    public enum CallBackContext
    {
        started,
        performed,
        canceled
    }

    public const CallBackContext started = CallBackContext.started;
    public const CallBackContext performed = CallBackContext.performed;
    public const CallBackContext canceled = CallBackContext.canceled;

    public class ActionDelegate
    {
        private ControlScheme controlScheme;
        private CallBackContext callBackContext;
        private InputAction inputAction;
        private Action<InputAction.CallbackContext> inputDelegate;

        public ActionDelegate(ControlScheme controlScheme, InputAction inputAction, CallBackContext callBackContext, Action<InputAction.CallbackContext> inputDelegate)
        {
            this.inputAction = inputAction;
            this.callBackContext = callBackContext;
            this.inputDelegate = inputDelegate;
            this.controlScheme = controlScheme;
        }

        public void SubscribeAction()
        {
            GameManager.InputManager.ChangeControlMap(controlScheme);
            switch (callBackContext)
            {
                case started:
                    inputAction.started += inputDelegate;
                    break;

                case performed:
                    inputAction.performed += inputDelegate;
                    break;

                case canceled:
                    inputAction.canceled += inputDelegate;
                    break;
            }
        }

        public void UnsubscribeAction()
        {
            GameManager.InputManager.ChangeControlMap(controlScheme);
            switch (callBackContext)
            {
                case CallBackContext.started:
                    inputAction.started -= inputDelegate;
                    break;

                case CallBackContext.performed:
                    inputAction.performed -= inputDelegate;
                    break;

                case CallBackContext.canceled:
                    inputAction.canceled -= inputDelegate;
                    break;
            }
        }
    }

    private List<ActionDelegate> actionDelegates = new();

    /// <summary>
    /// Add Input actions to be automated.
    /// This should only once and only after being instantiated.
    /// </summary>
    /// <param name="actionDelegates">The list of actions.</param>
    public void AddActions(List<ActionDelegate> actionDelegates)
    {
        this.actionDelegates = actionDelegates;
        OnEnable(); //Needs to be called as OnEnabled is called immediately on instantiation.
    }

    private void OnEnable()
    {
        foreach (ActionDelegate action in actionDelegates)
        {
            action.SubscribeAction();
        }
    }

    public void SubscribeActions()
    {
        foreach (ActionDelegate action in actionDelegates)
        {
            action.UnsubscribeAction();
        }
    }
}
