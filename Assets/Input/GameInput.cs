//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Input/GameInput.inputactions
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

public partial class @GameInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""CameraMovement"",
            ""id"": ""e39cb924-7b9a-4762-8dcd-ce8e314675d5"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""c65b6b4d-4b99-4ada-8f70-89e03ccd349b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""46e48212-e3fa-4dfb-a3f3-0266138a2cd2"",
                    ""path"": ""<Mouse>/leftButton"",
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
            ""name"": ""KeyboardMovement"",
            ""id"": ""084282e5-2a13-4464-b652-fa3276b7b4f6"",
            ""actions"": [
                {
                    ""name"": ""KeyboardMove"",
                    ""type"": ""Value"",
                    ""id"": ""ed1ec42f-a1a3-4e71-90ac-47ecdb3d656b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3f99e55a-112c-43e2-bbe8-38b851089e6c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d7b55d55-f76b-4166-ac50-3fb4f7c3c941"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""f9f9ce80-1e39-46e1-9b31-efc9e433183f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""e6ae7163-1c50-414e-8b60-657137926aaa"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""52937528-f6c5-43b7-bfbb-436a224c4fff"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""KeyboardMove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""FlagPlacement"",
            ""id"": ""14c65dc7-c639-45e6-9fcf-40844b6232ca"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""5495febb-0a5c-4cd9-ac5e-8a087c725580"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""db04a0a4-aa61-4a7a-b307-fab2d652b86f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7588507b-8070-4df6-be1c-0d3f3fa40b04"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f44dc3cf-2cd3-4c6a-803d-2f74b669dcc9"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""e2daa03e-32df-4d98-9c1e-31d87af62984"",
            ""actions"": [
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""6cb110f3-f51a-4837-b6f3-a4f7481c9d1f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""e12253bc-a3e0-48ec-a5cd-c34f0b04c852"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""e4700ecb-fa9e-4742-a95e-9257ca3ff0c8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""de7e91a1-1d82-44d6-960e-568a488aba18"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c2f7668-ddc9-44ff-b9d7-d194d02e28a1"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8bfb7b34-56f8-4e7a-bd35-5efdc7be934d"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CameraMovement
        m_CameraMovement = asset.FindActionMap("CameraMovement", throwIfNotFound: true);
        m_CameraMovement_Click = m_CameraMovement.FindAction("Click", throwIfNotFound: true);
        // KeyboardMovement
        m_KeyboardMovement = asset.FindActionMap("KeyboardMovement", throwIfNotFound: true);
        m_KeyboardMovement_KeyboardMove = m_KeyboardMovement.FindAction("KeyboardMove", throwIfNotFound: true);
        // FlagPlacement
        m_FlagPlacement = asset.FindActionMap("FlagPlacement", throwIfNotFound: true);
        m_FlagPlacement_LeftClick = m_FlagPlacement.FindAction("LeftClick", throwIfNotFound: true);
        m_FlagPlacement_RightClick = m_FlagPlacement.FindAction("RightClick", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_MouseDelta = m_Mouse.FindAction("MouseDelta", throwIfNotFound: true);
        m_Mouse_Click = m_Mouse.FindAction("Click", throwIfNotFound: true);
        m_Mouse_MousePosition = m_Mouse.FindAction("MousePosition", throwIfNotFound: true);
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

    // CameraMovement
    private readonly InputActionMap m_CameraMovement;
    private List<ICameraMovementActions> m_CameraMovementActionsCallbackInterfaces = new List<ICameraMovementActions>();
    private readonly InputAction m_CameraMovement_Click;
    public struct CameraMovementActions
    {
        private @GameInput m_Wrapper;
        public CameraMovementActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_CameraMovement_Click;
        public InputActionMap Get() { return m_Wrapper.m_CameraMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraMovementActions set) { return set.Get(); }
        public void AddCallbacks(ICameraMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Add(instance);
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
        }

        private void UnregisterCallbacks(ICameraMovementActions instance)
        {
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
        }

        public void RemoveCallbacks(ICameraMovementActions instance)
        {
            if (m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(ICameraMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_CameraMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_CameraMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public CameraMovementActions @CameraMovement => new CameraMovementActions(this);

    // KeyboardMovement
    private readonly InputActionMap m_KeyboardMovement;
    private List<IKeyboardMovementActions> m_KeyboardMovementActionsCallbackInterfaces = new List<IKeyboardMovementActions>();
    private readonly InputAction m_KeyboardMovement_KeyboardMove;
    public struct KeyboardMovementActions
    {
        private @GameInput m_Wrapper;
        public KeyboardMovementActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @KeyboardMove => m_Wrapper.m_KeyboardMovement_KeyboardMove;
        public InputActionMap Get() { return m_Wrapper.m_KeyboardMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeyboardMovementActions set) { return set.Get(); }
        public void AddCallbacks(IKeyboardMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_KeyboardMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_KeyboardMovementActionsCallbackInterfaces.Add(instance);
            @KeyboardMove.started += instance.OnKeyboardMove;
            @KeyboardMove.performed += instance.OnKeyboardMove;
            @KeyboardMove.canceled += instance.OnKeyboardMove;
        }

        private void UnregisterCallbacks(IKeyboardMovementActions instance)
        {
            @KeyboardMove.started -= instance.OnKeyboardMove;
            @KeyboardMove.performed -= instance.OnKeyboardMove;
            @KeyboardMove.canceled -= instance.OnKeyboardMove;
        }

        public void RemoveCallbacks(IKeyboardMovementActions instance)
        {
            if (m_Wrapper.m_KeyboardMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IKeyboardMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_KeyboardMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_KeyboardMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public KeyboardMovementActions @KeyboardMovement => new KeyboardMovementActions(this);

    // FlagPlacement
    private readonly InputActionMap m_FlagPlacement;
    private List<IFlagPlacementActions> m_FlagPlacementActionsCallbackInterfaces = new List<IFlagPlacementActions>();
    private readonly InputAction m_FlagPlacement_LeftClick;
    private readonly InputAction m_FlagPlacement_RightClick;
    public struct FlagPlacementActions
    {
        private @GameInput m_Wrapper;
        public FlagPlacementActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftClick => m_Wrapper.m_FlagPlacement_LeftClick;
        public InputAction @RightClick => m_Wrapper.m_FlagPlacement_RightClick;
        public InputActionMap Get() { return m_Wrapper.m_FlagPlacement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(FlagPlacementActions set) { return set.Get(); }
        public void AddCallbacks(IFlagPlacementActions instance)
        {
            if (instance == null || m_Wrapper.m_FlagPlacementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_FlagPlacementActionsCallbackInterfaces.Add(instance);
            @LeftClick.started += instance.OnLeftClick;
            @LeftClick.performed += instance.OnLeftClick;
            @LeftClick.canceled += instance.OnLeftClick;
            @RightClick.started += instance.OnRightClick;
            @RightClick.performed += instance.OnRightClick;
            @RightClick.canceled += instance.OnRightClick;
        }

        private void UnregisterCallbacks(IFlagPlacementActions instance)
        {
            @LeftClick.started -= instance.OnLeftClick;
            @LeftClick.performed -= instance.OnLeftClick;
            @LeftClick.canceled -= instance.OnLeftClick;
            @RightClick.started -= instance.OnRightClick;
            @RightClick.performed -= instance.OnRightClick;
            @RightClick.canceled -= instance.OnRightClick;
        }

        public void RemoveCallbacks(IFlagPlacementActions instance)
        {
            if (m_Wrapper.m_FlagPlacementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IFlagPlacementActions instance)
        {
            foreach (var item in m_Wrapper.m_FlagPlacementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_FlagPlacementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public FlagPlacementActions @FlagPlacement => new FlagPlacementActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private List<IMouseActions> m_MouseActionsCallbackInterfaces = new List<IMouseActions>();
    private readonly InputAction m_Mouse_MouseDelta;
    private readonly InputAction m_Mouse_Click;
    private readonly InputAction m_Mouse_MousePosition;
    public struct MouseActions
    {
        private @GameInput m_Wrapper;
        public MouseActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MouseDelta => m_Wrapper.m_Mouse_MouseDelta;
        public InputAction @Click => m_Wrapper.m_Mouse_Click;
        public InputAction @MousePosition => m_Wrapper.m_Mouse_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void AddCallbacks(IMouseActions instance)
        {
            if (instance == null || m_Wrapper.m_MouseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_MouseActionsCallbackInterfaces.Add(instance);
            @MouseDelta.started += instance.OnMouseDelta;
            @MouseDelta.performed += instance.OnMouseDelta;
            @MouseDelta.canceled += instance.OnMouseDelta;
            @Click.started += instance.OnClick;
            @Click.performed += instance.OnClick;
            @Click.canceled += instance.OnClick;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
        }

        private void UnregisterCallbacks(IMouseActions instance)
        {
            @MouseDelta.started -= instance.OnMouseDelta;
            @MouseDelta.performed -= instance.OnMouseDelta;
            @MouseDelta.canceled -= instance.OnMouseDelta;
            @Click.started -= instance.OnClick;
            @Click.performed -= instance.OnClick;
            @Click.canceled -= instance.OnClick;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
        }

        public void RemoveCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IMouseActions instance)
        {
            foreach (var item in m_Wrapper.m_MouseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_MouseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public MouseActions @Mouse => new MouseActions(this);
    public interface ICameraMovementActions
    {
        void OnClick(InputAction.CallbackContext context);
    }
    public interface IKeyboardMovementActions
    {
        void OnKeyboardMove(InputAction.CallbackContext context);
    }
    public interface IFlagPlacementActions
    {
        void OnLeftClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnMouseDelta(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
    }
}