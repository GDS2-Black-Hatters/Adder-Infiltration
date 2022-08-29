//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/Input/MainGameInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MainGameInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MainGameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MainGameInput"",
    ""maps"": [
        {
            ""name"": ""Hub"",
            ""id"": ""cf000ccb-c546-4cec-a970-7c43d1c1b262"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""96b12546-12a8-44f0-8469-aa977bd63937"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""96d9bdfe-75ff-4314-b1c3-2576e3452743"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""5bab815e-373f-4d81-b77e-fe8877a03f02"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""150e572b-aa42-492f-8ba3-c80184d91d28"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5132b51e-8e4b-4d83-a307-b7754d99c9ac"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""MainGame"",
            ""id"": ""94b09bb2-3256-47ff-9695-3f2b96f8a043"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""b0f62c71-5c04-4128-be91-9936aea2b4b3"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""66643286-2ac4-4245-83a0-228a9991deff"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""81b495b9-371c-446a-a50d-ac0bfc2b826d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""747b55e7-e4a8-4a3e-8efd-b89ddb901f91"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""75e81a03-2b5c-428d-9465-d05232ff795a"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f6a0e672-ea18-4f47-9ab6-5386a2e673d7"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""2096ce25-5408-48f6-ac9f-eda0212e8f5f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""e1e39480-5fee-47e5-b719-7303ac452011"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""ff45aa95-ee81-4b8d-ba2e-468db583a174"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""27aa3445-90ac-45c3-95d1-6307cfbf1e0e"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8ffee1d6-c875-46f1-9bb1-63e5194d5455"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1a78499c-fdf3-48f0-9b97-1ee26ce5e16c"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7d7f5d17-68b6-472e-ae81-9795cc2fd1ab"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6893424f-a342-4f6c-9f02-42952febabc6"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""48abc67d-d07e-4afc-9a3a-ab5d78b8770f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Hub
        m_Hub = asset.FindActionMap("Hub", throwIfNotFound: true);
        m_Hub_Move = m_Hub.FindAction("Move", throwIfNotFound: true);
        m_Hub_Click = m_Hub.FindAction("Click", throwIfNotFound: true);
        // MainGame
        m_MainGame = asset.FindActionMap("MainGame", throwIfNotFound: true);
        m_MainGame_Move = m_MainGame.FindAction("Move", throwIfNotFound: true);
        m_MainGame_Look = m_MainGame.FindAction("Look", throwIfNotFound: true);
        m_MainGame_Interact = m_MainGame.FindAction("Interact", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Hub
    private readonly InputActionMap m_Hub;
    private IHubActions m_HubActionsCallbackInterface;
    private readonly InputAction m_Hub_Move;
    private readonly InputAction m_Hub_Click;
    public struct HubActions
    {
        private @MainGameInput m_Wrapper;
        public HubActions(@MainGameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Hub_Move;
        public InputAction @Click => m_Wrapper.m_Hub_Click;
        public InputActionMap Get() { return m_Wrapper.m_Hub; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HubActions set) { return set.Get(); }
        public void SetCallbacks(IHubActions instance)
        {
            if (m_Wrapper.m_HubActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_HubActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_HubActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_HubActionsCallbackInterface.OnMove;
                @Click.started -= m_Wrapper.m_HubActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_HubActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_HubActionsCallbackInterface.OnClick;
            }
            m_Wrapper.m_HubActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
            }
        }
    }
    public HubActions @Hub => new HubActions(this);

    // MainGame
    private readonly InputActionMap m_MainGame;
    private IMainGameActions m_MainGameActionsCallbackInterface;
    private readonly InputAction m_MainGame_Move;
    private readonly InputAction m_MainGame_Look;
    private readonly InputAction m_MainGame_Interact;
    public struct MainGameActions
    {
        private @MainGameInput m_Wrapper;
        public MainGameActions(@MainGameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_MainGame_Move;
        public InputAction @Look => m_Wrapper.m_MainGame_Look;
        public InputAction @Interact => m_Wrapper.m_MainGame_Interact;
        public InputActionMap Get() { return m_Wrapper.m_MainGame; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MainGameActions set) { return set.Get(); }
        public void SetCallbacks(IMainGameActions instance)
        {
            if (m_Wrapper.m_MainGameActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnMove;
                @Look.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnLook;
                @Interact.started -= m_Wrapper.m_MainGameActionsCallbackInterface.OnInteract;
                @Interact.performed -= m_Wrapper.m_MainGameActionsCallbackInterface.OnInteract;
                @Interact.canceled -= m_Wrapper.m_MainGameActionsCallbackInterface.OnInteract;
            }
            m_Wrapper.m_MainGameActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @Interact.started += instance.OnInteract;
                @Interact.performed += instance.OnInteract;
                @Interact.canceled += instance.OnInteract;
            }
        }
    }
    public MainGameActions @MainGame => new MainGameActions(this);
    public interface IHubActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
    }
    public interface IMainGameActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
    }
}
