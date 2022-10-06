using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class ActionInputSubscriber : MonoBehaviour
{
    public enum CallbackContext
    {
        Started,
        Performed,
        Canceled
    }

    public class ActionDelegate
    {
        private readonly CallbackContext callBackContext;
        private readonly InputAction inputAction;
        private readonly Action<InputAction.CallbackContext> inputDelegate;

        public ActionDelegate(InputAction inputAction, CallbackContext callBackContext, Action<InputAction.CallbackContext> inputDelegate)
        {
            this.inputAction = inputAction;
            this.callBackContext = callBackContext;
            this.inputDelegate = inputDelegate;
        }

        public void SubscribeAction()
        {
            switch (callBackContext)
            {
                case CallbackContext.Started:
                    inputAction.started += inputDelegate;
                    break;

                case CallbackContext.Performed:
                    inputAction.performed += inputDelegate;
                    break;

                case CallbackContext.Canceled:
                    inputAction.canceled += inputDelegate;
                    break;
            }
        }

        public void UnsubscribeAction()
        {
            switch (callBackContext)
            {
                case CallbackContext.Started:
                    inputAction.started -= inputDelegate;
                    break;

                case CallbackContext.Performed:
                    inputAction.performed -= inputDelegate;
                    break;

                case CallbackContext.Canceled:
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
