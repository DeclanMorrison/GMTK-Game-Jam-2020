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
                    ""interactions"": ""Press""
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
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""011e31f2-9101-4421-b9ae-27e13d6df040"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""6563cc4a-2c51-484c-8d39-34e733e391bf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""bd68e41d-ed8b-4bd3-baa4-a7300e817b83"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7bb3638a-c755-444c-8c2e-562f43a553a6"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""bb27a797-10e7-4076-9daa-509b17777668"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""c2cbf497-caa6-4ec4-a455-2d5f7452df6d"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Arms1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""22909dfd-f286-4b22-9140-a0ec049281ae"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Arms1"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""45d03088-7fcd-475d-b120-fb494dbe6adf"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Arms1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d63b5f7a-4e9b-4b9b-a38b-0b467a538cf4"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Arms1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""c56cbf08-adf0-45d9-bbb5-da686990a0f0"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Arms1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a67b6eed-42fd-4b22-b94b-52e4967414f1"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Arms1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""85732f4a-b57d-462a-92a2-48e13d87a6fb"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Jump1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""44a696ac-71f1-415b-83ed-2dd0e3e8d1aa"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""Jump1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9e6850a-978c-4fde-a91b-65c8f0d85247"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
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
                    ""groups"": ""Controller"",
                    ""action"": ""PickupThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6a2a071d-77c1-4501-a6f8-07cc9f34496f"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse;KeyboardOnly"",
                    ""action"": ""PickupThrow"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""KeyboardOnly"",
            ""bindingGroup"": ""KeyboardOnly"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard&Mouse"",
            ""bindingGroup"": ""Keyboard&Mouse"",
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
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    private int m_KeyboardOnlySchemeIndex = -1;
    public InputControlScheme KeyboardOnlyScheme
    {
        get
        {
            if (m_KeyboardOnlySchemeIndex == -1) m_KeyboardOnlySchemeIndex = asset.FindControlSchemeIndex("KeyboardOnly");
            return asset.controlSchemes[m_KeyboardOnlySchemeIndex];
        }
    }
    private int m_KeyboardMouseSchemeIndex = -1;
    public InputControlScheme KeyboardMouseScheme
    {
        get
        {
            if (m_KeyboardMouseSchemeIndex == -1) m_KeyboardMouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard&Mouse");
            return asset.controlSchemes[m_KeyboardMouseSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnArms1(InputAction.CallbackContext context);
        void OnJump1(InputAction.CallbackContext context);
        void OnPickupThrow(InputAction.CallbackContext context);
    }
}
