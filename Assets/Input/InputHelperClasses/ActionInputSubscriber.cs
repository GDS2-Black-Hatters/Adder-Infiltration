using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

public sealed class ActionInputSubscriber : MonoBehaviour
{
    public enum CallBackContext
    {
        Started,
        Performed,
        Canceled
    }

    public class ActionDelegate
    {
        private readonly ControlScheme controlScheme;
        private readonly CallBackContext callBackContext;
        private readonly InputAction inputAction;
        private readonly Action<InputAction.CallbackContext> inputDelegate;

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
                case CallBackContext.Started:
                    inputAction.started += inputDelegate;
                    break;

                case CallBackContext.Performed:
                    inputAction.performed += inputDelegate;
                    break;

                case CallBackContext.Canceled:
                    inputAction.canceled += inputDelegate;
                    break;
            }
        }

        public void UnsubscribeAction()
        {
            //GameManager.InputManager.ChangeControlMap(controlScheme);
            switch (callBackContext)
            {
                case CallBackContext.Started:
                    inputAction.started -= inputDelegate;
                    break;

                case CallBackContext.Performed:
                    inputAction.performed -= inputDelegate;
                    break;

                case CallBackContext.Canceled:
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
        UnsubscribeActions();
        this.actionDelegates = actionDelegates;
        SubscribeActions();
    }

    private void OnEnable()
    {
        SubscribeActions();
    }

    private void OnDisable()
    {
        UnsubscribeActions();
    }

    private void SubscribeActions()
    {
        foreach (ActionDelegate action in actionDelegates)
        {
            action.SubscribeAction();
        }
    }

    private void UnsubscribeActions()
    {
        foreach (ActionDelegate action in actionDelegates)
        {
            action.UnsubscribeAction();
        }
    }
}
