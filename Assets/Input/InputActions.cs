//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/InputActions.inputactions
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

public partial class @InputActions: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""7754666e-1953-454b-894b-83f1a5397b3a"",
            ""actions"": [
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b2556790-c4c9-40e3-af8d-41f36d5eeb2e"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""56585f93-4f4c-4ec4-953e-5b1f641d68d7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseHold"",
                    ""type"": ""Button"",
                    ""id"": ""5174fe58-33af-4bbd-a18d-5df05a89ed7f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f833edc3-2b85-4d78-bdbb-f7297e3417fd"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyControlScheme"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e148b892-8986-4310-aeb4-38ad440ccafb"",
                    ""path"": ""<Touchscreen>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyControlScheme"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""a52e184e-c24e-4fe9-833b-9bf63f6a8e13"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2ccfd1aa-ffe5-4aad-a48a-e4f6021c6580"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""31853e12-87e7-4762-99ce-0b026c2f838b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""9de322da-08f1-48aa-86f9-d0fcd3cd51b3"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""24e07de1-bce8-4b5c-8e83-232d5d9f606e"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""8f6677d8-8342-47ca-802d-0ef299312da1"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseHold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Intro"",
            ""id"": ""64b72f3a-eee9-4798-a299-4a7dedd3183a"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""199534cc-7ff0-45a0-8c4d-50eaf1b01cc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Touch"",
                    ""type"": ""Value"",
                    ""id"": ""8e15a429-4684-4c5b-9c17-c267f7b07393"",
                    ""expectedControlType"": ""Touch"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""8b6c86ed-9298-4d35-952b-7e2d08752bfb"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyControlScheme"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c700705e-98a7-4074-9321-0f8fdd771151"",
                    ""path"": ""<Touchscreen>/primaryTouch"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""MyControlScheme"",
                    ""action"": ""Touch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""MyControlScheme"",
            ""bindingGroup"": ""MyControlScheme"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Look = m_Player.FindAction("Look", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_MouseHold = m_Player.FindAction("MouseHold", throwIfNotFound: true);
        // Intro
        m_Intro = asset.FindActionMap("Intro", throwIfNotFound: true);
        m_Intro_Click = m_Intro.FindAction("Click", throwIfNotFound: true);
        m_Intro_Touch = m_Intro.FindAction("Touch", throwIfNotFound: true);
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

    // Player
    private readonly InputActionMap m_Player;
    private List<IPlayerActions> m_PlayerActionsCallbackInterfaces = new List<IPlayerActions>();
    private readonly InputAction m_Player_Look;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_MouseHold;
    public struct PlayerActions
    {
        private @InputActions m_Wrapper;
        public PlayerActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Look => m_Wrapper.m_Player_Look;
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @MouseHold => m_Wrapper.m_Player_MouseHold;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Add(instance);
            @Look.started += instance.OnLook;
            @Look.performed += instance.OnLook;
            @Look.canceled += instance.OnLook;
            @Movement.started += instance.OnMovement;
            @Movement.performed += instance.OnMovement;
            @Movement.canceled += instance.OnMovement;
            @MouseHold.started += instance.OnMouseHold;
            @MouseHold.performed += instance.OnMouseHold;
            @MouseHold.canceled += instance.OnMouseHold;
        }

        private void UnregisterCallbacks(IPlayerActions instance)
        {
            @Look.started -= instance.OnLook;
            @Look.performed -= instance.OnLook;
            @Look.canceled -= instance.OnLook;
            @Movement.started -= instance.OnMovement;
            @Movement.performed -= instance.OnMovement;
            @Movement.canceled -= instance.OnMovement;
            @MouseHold.started -= instance.OnMouseHold;
            @MouseHold.performed -= instance.OnMouseHold;
            @MouseHold.canceled -= instance.OnMouseHold;
        }

        public void RemoveCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Intro
    private readonly InputActionMap m_Intro;
    private List<IIntroActions> m_IntroActionsCallbackInterfaces = new List<IIntroActions>();
    private readonly InputAction m_Intro_Click;
    private readonly InputAction m_Intro_Touch;
    public struct IntroActions
    {
        private @InputActions m_Wrapper;
        public IntroActions(@InputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Intro_Click;
        public InputAction @Touch => m_Wrapper.m_Intro_Touch;
        public InputActionMap Get() { return m_Wrapper.m_Intro; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(IntroActions set) { return set.Get(); }
        public void AddCallbacks(IIntroActions instance)
        {
            if (instance == null || m_Wrapper.m_IntroActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_IntroActionsCallbackInterfaces.Add(instance);
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
            @Touch.started += instance.OnTouch;
            @Touch.performed += instance.OnTouch;
            @Touch.canceled += instance.OnTouch;
        }

        private void UnregisterCallbacks(IIntroActions instance)
        {
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
            @Touch.started -= instance.OnTouch;
            @Touch.performed -= instance.OnTouch;
            @Touch.canceled -= instance.OnTouch;
        }

        public void RemoveCallbacks(IIntroActions instance)
        {
            if (m_Wrapper.m_IntroActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IIntroActions instance)
        {
            foreach (var item in m_Wrapper.m_IntroActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_IntroActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public IntroActions @Intro => new IntroActions(this);
    private int m_MyControlSchemeSchemeIndex = -1;
    public InputControlScheme MyControlSchemeScheme
    {
        get
        {
            if (m_MyControlSchemeSchemeIndex == -1) m_MyControlSchemeSchemeIndex = asset.FindControlSchemeIndex("MyControlScheme");
            return asset.controlSchemes[m_MyControlSchemeSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnLook(InputAction.CallbackContext context);
        void OnMovement(InputAction.CallbackContext context);
        void OnMouseHold(InputAction.CallbackContext context);
    }
    public interface IIntroActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnTouch(InputAction.CallbackContext context);
    }
}
