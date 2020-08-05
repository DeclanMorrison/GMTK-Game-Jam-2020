// GENERATED AUTOMATICALLY FROM 'Assets/Controls/EngineerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @EngineerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @EngineerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""EngineerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""1d4d56e2-e8d3-4382-b37a-8d259a8cc028"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""cc5a33ab-7523-4302-8156-d206d0a07304"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Arms1"",
                    ""type"": ""Value"",
                    ""id"": ""2a4eee39-46fe-476d-b1ad-16366831b9ae"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump1"",
                    ""type"": ""Button"",
                    ""id"": ""2eb92a61-7515-4c9f-9c20-e5b9a27fabf6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickupThrow"",
                    ""type"": ""Button"",
                    ""id"": ""41d218ae-e51f-4d34-b790-4b1683f25eac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7d9c6bf9-0964-49a0-b66d-dab1eb7ac7fc"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c2cbf497-caa6-4ec4-a455-2d5f7452df6d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arms1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85732f4a-b57d-462a-92a2-48e13d87a6fb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7942ccdb-feda-43eb-8272-cbaa05473710"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickupThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Arms1 = m_Gameplay.FindAction("Arms1", throwIfNotFound: true);
        m_Gameplay_Jump1 = m_Gameplay.FindAction("Jump1", throwIfNotFound: true);
        m_Gameplay_PickupThrow = m_Gameplay.FindAction("PickupThrow", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Arms1;
    private readonly InputAction m_Gameplay_Jump1;
    private readonly InputAction m_Gameplay_PickupThrow;
    public struct GameplayActions
    {
        private @EngineerControls m_Wrapper;
        public GameplayActions(@EngineerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Arms1 => m_Wrapper.m_Gameplay_Arms1;
        public InputAction @Jump1 => m_Wrapper.m_Gameplay_Jump1;
        public InputAction @PickupThrow => m_Wrapper.m_Gameplay_PickupThrow;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Arms1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArms1;
                @Arms1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArms1;
                @Arms1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnArms1;
                @Jump1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump1;
                @Jump1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump1;
                @Jump1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump1;
                @PickupThrow.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickupThrow;
                @PickupThrow.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickupThrow;
                @PickupThrow.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnPickupThrow;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Arms1.started += instance.OnArms1;
                @Arms1.performed += instance.OnArms1;
                @Arms1.canceled += instance.OnArms1;
                @Jump1.started += instance.OnJump1;
                @Jump1.performed += instance.OnJump1;
                @Jump1.canceled += instance.OnJump1;
                @PickupThrow.started += instance.OnPickupThrow;
                @PickupThrow.performed += instance.OnPickupThrow;
                @PickupThrow.canceled += instance.OnPickupThrow;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnArms1(InputAction.CallbackContext context);
        void OnJump1(InputAction.CallbackContext context);
        void OnPickupThrow(InputAction.CallbackContext context);
    }
}
