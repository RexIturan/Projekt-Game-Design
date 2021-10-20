// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset Asset { get; }
    public @GameInput()
    {
        Asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInput"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""07f865f9-38b2-424c-bfe1-3394bfb08082"",
            ""actions"": [
                {
                    ""name"": ""EndTurn"",
                    ""type"": ""Button"",
                    ""id"": ""825ce0d1-114c-4564-9671-dc645969826f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""48f4cc51-a887-4d95-91a7-06ac7e6ba9bf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""5e4f0dcd-fe27-4e31-ad34-9499e7178129"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClicked"",
                    ""type"": ""Button"",
                    ""id"": ""dd8a1892-e7ef-4d4a-af93-5c8a039c5a57"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3062a1a8-5206-4d4d-80dd-89ce71442b29"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""EndTurn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""32c76346-c7d3-4e62-b104-49cd90939443"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1c04ae54-3679-4ff9-ac7e-5bbae7ec961a"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""471352d6-97a4-42af-bed4-f7946ae6b611"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MouseClicked"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""LevelEditor"",
            ""id"": ""77b5a28f-4e02-4f05-a574-7e0e70b9d0c3"",
            ""actions"": [
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""9bb9c08c-2e56-4670-86d2-63c22c675bbc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e0138707-1d43-4983-b9a7-de2246f43e69"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Camera"",
            ""id"": ""28e447a9-eb7b-4d6b-b247-61b5deb02770"",
            ""actions"": [
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""Value"",
                    ""id"": ""41b0a211-6820-488c-b449-34aa8052fc41"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RotateCamera"",
                    ""type"": ""Value"",
                    ""id"": ""1c373801-4d69-4ee0-9273-5a66f669673c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CameraZoom"",
                    ""type"": ""Value"",
                    ""id"": ""a08026b2-a359-4f99-bceb-a90f70c360d5"",
                    ""expectedControlType"": ""Analog"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""273595d3-cd0d-408e-aeeb-8b4e1a2bdb4d"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""166bc457-7275-4b86-8f21-02c3cb0d42a7"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""8cebb3ed-a5ac-4311-933a-31a1bf4c2df8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""43d72826-51d5-4d3c-b349-f5ca49e82683"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""eb2d000d-2991-4f6e-8d04-9e43a52d743a"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrow Keys"",
                    ""id"": ""8e2bc341-e449-4cbc-9612-946dd70c7446"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1f241b5c-3c7a-4aa6-a7ae-f8f8213a9b53"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""014e809a-3234-46e1-b3be-58862c0e91c3"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""be1d2b16-ab70-412b-847f-747ad7db6980"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1593e5f5-ebf1-48b3-8c24-c1f7155d07ea"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5457a222-7899-42c5-9e5c-3d567827ee8e"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CameraZoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard Rotate"",
                    ""id"": ""19a4f58b-0bbf-46da-b791-b8be0cc30415"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""4ee5937f-cd9d-47be-b4f7-5f64e364ceb0"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""fcd119fe-0c94-458e-9cd2-ac77808352d2"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""RotateCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""ac6aa3a3-1db6-457f-9c28-628797d76e35"",
            ""actions"": [
                {
                    ""name"": ""Confirm"",
                    ""type"": ""Button"",
                    ""id"": ""d6c7767c-7764-4e5e-bd4b-7e70f2674742"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""180e62db-9871-4c8b-8d15-ddd46483fa73"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""46123e31-2822-4ba6-bc5d-4f5d87a0147d"",
                    ""path"": ""<Keyboard>/enter"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42abd253-e46f-4a01-bb05-f88d193cc47c"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Confirm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3aa78bae-d111-45ee-a05c-4a3ca123e0bd"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Pathfinding Debug"",
            ""id"": ""5b80161a-b43e-44c0-950f-42ec70159e52"",
            ""actions"": [
                {
                    ""name"": ""Toggle"",
                    ""type"": ""Button"",
                    ""id"": ""a257f384-b91c-4622-aa3b-685d0e1a2645"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Step"",
                    ""type"": ""Button"",
                    ""id"": ""db5a71d8-3d1a-4f69-b245-38f8f2d9b4c2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ShowCompletePath"",
                    ""type"": ""Button"",
                    ""id"": ""34ff1697-0083-4912-9f6f-5988b68b6f07"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""73550c8f-b57a-46fc-adf3-8291bf4cee4e"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Toggle"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0238f046-3c40-416f-bc62-c0c290e5f38e"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Step"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56097670-e2f7-40b2-aad2-3c641d9ba03a"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""ShowCompletePath"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Inventory"",
            ""id"": ""08413d11-c7a0-4765-9d70-d63dcb1217c5"",
            ""actions"": [
                {
                    ""name"": ""CancelInventory"",
                    ""type"": ""Button"",
                    ""id"": ""6be97bc3-3d5e-40c1-babf-afbae0049aa3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6df524d9-a4e0-4479-bd8a-4438977ba4ba"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CancelInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""caa1823f-6396-4c95-97ad-9a46778dc290"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""CancelInventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""LoadingScreen"",
            ""id"": ""a3cc2e69-42f1-4589-8213-6954aa98a723"",
            ""actions"": [
                {
                    ""name"": ""Continue"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dec1c71d-f870-48c0-b436-b1fe6b5fe934"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""9e56c3d9-adc6-410a-bb1c-0a29457f982d"",
                    ""path"": ""<Keyboard>/anyKey"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""35968cbf-c29f-4944-a512-2e965e6a970e"",
                    ""path"": ""*/Button"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f12239b-87e5-4543-b3b0-a921f63665fc"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Continue"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        }
    ]
}");
        // Gameplay
        _mGameplay = Asset.FindActionMap("Gameplay", throwIfNotFound: true);
        _mGameplayEndTurn = _mGameplay.FindAction("EndTurn", throwIfNotFound: true);
        _mGameplayMenu = _mGameplay.FindAction("Menu", throwIfNotFound: true);
        _mGameplayInventory = _mGameplay.FindAction("Inventory", throwIfNotFound: true);
        _mGameplayMouseClicked = _mGameplay.FindAction("MouseClicked", throwIfNotFound: true);
        // LevelEditor
        _mLevelEditor = Asset.FindActionMap("LevelEditor", throwIfNotFound: true);
        _mLevelEditorMenu = _mLevelEditor.FindAction("Menu", throwIfNotFound: true);
        // Camera
        _mCamera = Asset.FindActionMap("Camera", throwIfNotFound: true);
        _mCameraMoveCamera = _mCamera.FindAction("MoveCamera", throwIfNotFound: true);
        _mCameraRotateCamera = _mCamera.FindAction("RotateCamera", throwIfNotFound: true);
        _mCameraCameraZoom = _mCamera.FindAction("CameraZoom", throwIfNotFound: true);
        // Menu
        _mMenu = Asset.FindActionMap("Menu", throwIfNotFound: true);
        _mMenuConfirm = _mMenu.FindAction("Confirm", throwIfNotFound: true);
        _mMenuCancel = _mMenu.FindAction("Cancel", throwIfNotFound: true);
        // Pathfinding Debug
        _mPathfindingDebug = Asset.FindActionMap("Pathfinding Debug", throwIfNotFound: true);
        _mPathfindingDebugToggle = _mPathfindingDebug.FindAction("Toggle", throwIfNotFound: true);
        _mPathfindingDebugStep = _mPathfindingDebug.FindAction("Step", throwIfNotFound: true);
        _mPathfindingDebugShowCompletePath = _mPathfindingDebug.FindAction("ShowCompletePath", throwIfNotFound: true);
        // Inventory
        _mInventory = Asset.FindActionMap("Inventory", throwIfNotFound: true);
        _mInventoryCancelInventory = _mInventory.FindAction("CancelInventory", throwIfNotFound: true);
        // LoadingScreen
        _mLoadingScreen = Asset.FindActionMap("LoadingScreen", throwIfNotFound: true);
        _mLoadingScreenContinue = _mLoadingScreen.FindAction("Continue", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(Asset);
    }

    public InputBinding? bindingMask
    {
        get => Asset.bindingMask;
        set => Asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => Asset.devices;
        set => Asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => Asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return Asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return Asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        Asset.Enable();
    }

    public void Disable()
    {
        Asset.Disable();
    }

    // Gameplay
    private readonly InputActionMap _mGameplay;
    private IGameplayActions _mGameplayActionsCallbackInterface;
    private readonly InputAction _mGameplayEndTurn;
    private readonly InputAction _mGameplayMenu;
    private readonly InputAction _mGameplayInventory;
    private readonly InputAction _mGameplayMouseClicked;
    public struct GameplayActions
    {
        private @GameInput _mWrapper;
        public GameplayActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @EndTurn => _mWrapper._mGameplayEndTurn;
        public InputAction @Menu => _mWrapper._mGameplayMenu;
        public InputAction @Inventory => _mWrapper._mGameplayInventory;
        public InputAction @MouseClicked => _mWrapper._mGameplayMouseClicked;
        public InputActionMap Get() { return _mWrapper._mGameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (_mWrapper._mGameplayActionsCallbackInterface != null)
            {
                @EndTurn.started -= _mWrapper._mGameplayActionsCallbackInterface.OnEndTurn;
                @EndTurn.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnEndTurn;
                @EndTurn.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnEndTurn;
                @Menu.started -= _mWrapper._mGameplayActionsCallbackInterface.OnMenu;
                @Menu.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnMenu;
                @Menu.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnMenu;
                @Inventory.started -= _mWrapper._mGameplayActionsCallbackInterface.OnInventory;
                @Inventory.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnInventory;
                @MouseClicked.started -= _mWrapper._mGameplayActionsCallbackInterface.OnMouseClicked;
                @MouseClicked.performed -= _mWrapper._mGameplayActionsCallbackInterface.OnMouseClicked;
                @MouseClicked.canceled -= _mWrapper._mGameplayActionsCallbackInterface.OnMouseClicked;
            }
            _mWrapper._mGameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @EndTurn.started += instance.OnEndTurn;
                @EndTurn.performed += instance.OnEndTurn;
                @EndTurn.canceled += instance.OnEndTurn;
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @MouseClicked.started += instance.OnMouseClicked;
                @MouseClicked.performed += instance.OnMouseClicked;
                @MouseClicked.canceled += instance.OnMouseClicked;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // LevelEditor
    private readonly InputActionMap _mLevelEditor;
    private ILevelEditorActions _mLevelEditorActionsCallbackInterface;
    private readonly InputAction _mLevelEditorMenu;
    public struct LevelEditorActions
    {
        private @GameInput _mWrapper;
        public LevelEditorActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @Menu => _mWrapper._mLevelEditorMenu;
        public InputActionMap Get() { return _mWrapper._mLevelEditor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelEditorActions set) { return set.Get(); }
        public void SetCallbacks(ILevelEditorActions instance)
        {
            if (_mWrapper._mLevelEditorActionsCallbackInterface != null)
            {
                @Menu.started -= _mWrapper._mLevelEditorActionsCallbackInterface.OnMenu;
                @Menu.performed -= _mWrapper._mLevelEditorActionsCallbackInterface.OnMenu;
                @Menu.canceled -= _mWrapper._mLevelEditorActionsCallbackInterface.OnMenu;
            }
            _mWrapper._mLevelEditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
            }
        }
    }
    public LevelEditorActions @LevelEditor => new LevelEditorActions(this);

    // Camera
    private readonly InputActionMap _mCamera;
    private ICameraActions _mCameraActionsCallbackInterface;
    private readonly InputAction _mCameraMoveCamera;
    private readonly InputAction _mCameraRotateCamera;
    private readonly InputAction _mCameraCameraZoom;
    public struct CameraActions
    {
        private @GameInput _mWrapper;
        public CameraActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @MoveCamera => _mWrapper._mCameraMoveCamera;
        public InputAction @RotateCamera => _mWrapper._mCameraRotateCamera;
        public InputAction @CameraZoom => _mWrapper._mCameraCameraZoom;
        public InputActionMap Get() { return _mWrapper._mCamera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (_mWrapper._mCameraActionsCallbackInterface != null)
            {
                @MoveCamera.started -= _mWrapper._mCameraActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= _mWrapper._mCameraActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= _mWrapper._mCameraActionsCallbackInterface.OnMoveCamera;
                @RotateCamera.started -= _mWrapper._mCameraActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= _mWrapper._mCameraActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= _mWrapper._mCameraActionsCallbackInterface.OnRotateCamera;
                @CameraZoom.started -= _mWrapper._mCameraActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.performed -= _mWrapper._mCameraActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.canceled -= _mWrapper._mCameraActionsCallbackInterface.OnCameraZoom;
            }
            _mWrapper._mCameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveCamera.started += instance.OnMoveCamera;
                @MoveCamera.performed += instance.OnMoveCamera;
                @MoveCamera.canceled += instance.OnMoveCamera;
                @RotateCamera.started += instance.OnRotateCamera;
                @RotateCamera.performed += instance.OnRotateCamera;
                @RotateCamera.canceled += instance.OnRotateCamera;
                @CameraZoom.started += instance.OnCameraZoom;
                @CameraZoom.performed += instance.OnCameraZoom;
                @CameraZoom.canceled += instance.OnCameraZoom;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Menu
    private readonly InputActionMap _mMenu;
    private IMenuActions _mMenuActionsCallbackInterface;
    private readonly InputAction _mMenuConfirm;
    private readonly InputAction _mMenuCancel;
    public struct MenuActions
    {
        private @GameInput _mWrapper;
        public MenuActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @Confirm => _mWrapper._mMenuConfirm;
        public InputAction @Cancel => _mWrapper._mMenuCancel;
        public InputActionMap Get() { return _mWrapper._mMenu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (_mWrapper._mMenuActionsCallbackInterface != null)
            {
                @Confirm.started -= _mWrapper._mMenuActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= _mWrapper._mMenuActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= _mWrapper._mMenuActionsCallbackInterface.OnConfirm;
                @Cancel.started -= _mWrapper._mMenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= _mWrapper._mMenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= _mWrapper._mMenuActionsCallbackInterface.OnCancel;
            }
            _mWrapper._mMenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Confirm.started += instance.OnConfirm;
                @Confirm.performed += instance.OnConfirm;
                @Confirm.canceled += instance.OnConfirm;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);

    // Pathfinding Debug
    private readonly InputActionMap _mPathfindingDebug;
    private IPathfindingDebugActions _mPathfindingDebugActionsCallbackInterface;
    private readonly InputAction _mPathfindingDebugToggle;
    private readonly InputAction _mPathfindingDebugStep;
    private readonly InputAction _mPathfindingDebugShowCompletePath;
    public struct PathfindingDebugActions
    {
        private @GameInput _mWrapper;
        public PathfindingDebugActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @Toggle => _mWrapper._mPathfindingDebugToggle;
        public InputAction @Step => _mWrapper._mPathfindingDebugStep;
        public InputAction @ShowCompletePath => _mWrapper._mPathfindingDebugShowCompletePath;
        public InputActionMap Get() { return _mWrapper._mPathfindingDebug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(PathfindingDebugActions set) { return set.Get(); }
        public void SetCallbacks(IPathfindingDebugActions instance)
        {
            if (_mWrapper._mPathfindingDebugActionsCallbackInterface != null)
            {
                @Toggle.started -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnToggle;
                @Toggle.performed -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnToggle;
                @Toggle.canceled -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnToggle;
                @Step.started -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnStep;
                @Step.performed -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnStep;
                @Step.canceled -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnStep;
                @ShowCompletePath.started -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnShowCompletePath;
                @ShowCompletePath.performed -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnShowCompletePath;
                @ShowCompletePath.canceled -= _mWrapper._mPathfindingDebugActionsCallbackInterface.OnShowCompletePath;
            }
            _mWrapper._mPathfindingDebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Toggle.started += instance.OnToggle;
                @Toggle.performed += instance.OnToggle;
                @Toggle.canceled += instance.OnToggle;
                @Step.started += instance.OnStep;
                @Step.performed += instance.OnStep;
                @Step.canceled += instance.OnStep;
                @ShowCompletePath.started += instance.OnShowCompletePath;
                @ShowCompletePath.performed += instance.OnShowCompletePath;
                @ShowCompletePath.canceled += instance.OnShowCompletePath;
            }
        }
    }
    public PathfindingDebugActions @PathfindingDebug => new PathfindingDebugActions(this);

    // Inventory
    private readonly InputActionMap _mInventory;
    private IInventoryActions _mInventoryActionsCallbackInterface;
    private readonly InputAction _mInventoryCancelInventory;
    public struct InventoryActions
    {
        private @GameInput _mWrapper;
        public InventoryActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @CancelInventory => _mWrapper._mInventoryCancelInventory;
        public InputActionMap Get() { return _mWrapper._mInventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (_mWrapper._mInventoryActionsCallbackInterface != null)
            {
                @CancelInventory.started -= _mWrapper._mInventoryActionsCallbackInterface.OnCancelInventory;
                @CancelInventory.performed -= _mWrapper._mInventoryActionsCallbackInterface.OnCancelInventory;
                @CancelInventory.canceled -= _mWrapper._mInventoryActionsCallbackInterface.OnCancelInventory;
            }
            _mWrapper._mInventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CancelInventory.started += instance.OnCancelInventory;
                @CancelInventory.performed += instance.OnCancelInventory;
                @CancelInventory.canceled += instance.OnCancelInventory;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // LoadingScreen
    private readonly InputActionMap _mLoadingScreen;
    private ILoadingScreenActions _mLoadingScreenActionsCallbackInterface;
    private readonly InputAction _mLoadingScreenContinue;
    public struct LoadingScreenActions
    {
        private @GameInput _mWrapper;
        public LoadingScreenActions(@GameInput wrapper) { _mWrapper = wrapper; }
        public InputAction @Continue => _mWrapper._mLoadingScreenContinue;
        public InputActionMap Get() { return _mWrapper._mLoadingScreen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool Enabled => Get().enabled;
        public static implicit operator InputActionMap(LoadingScreenActions set) { return set.Get(); }
        public void SetCallbacks(ILoadingScreenActions instance)
        {
            if (_mWrapper._mLoadingScreenActionsCallbackInterface != null)
            {
                @Continue.started -= _mWrapper._mLoadingScreenActionsCallbackInterface.OnContinue;
                @Continue.performed -= _mWrapper._mLoadingScreenActionsCallbackInterface.OnContinue;
                @Continue.canceled -= _mWrapper._mLoadingScreenActionsCallbackInterface.OnContinue;
            }
            _mWrapper._mLoadingScreenActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
            }
        }
    }
    public LoadingScreenActions @LoadingScreen => new LoadingScreenActions(this);
    private int _mKeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (_mKeyboardSchemeIndex == -1) _mKeyboardSchemeIndex = Asset.FindControlSchemeIndex("Keyboard");
            return Asset.controlSchemes[_mKeyboardSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnEndTurn(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMouseClicked(InputAction.CallbackContext context);
    }
    public interface ILevelEditorActions
    {
        void OnMenu(InputAction.CallbackContext context);
    }
    public interface ICameraActions
    {
        void OnMoveCamera(InputAction.CallbackContext context);
        void OnRotateCamera(InputAction.CallbackContext context);
        void OnCameraZoom(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnConfirm(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
    }
    public interface IPathfindingDebugActions
    {
        void OnToggle(InputAction.CallbackContext context);
        void OnStep(InputAction.CallbackContext context);
        void OnShowCompletePath(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnCancelInventory(InputAction.CallbackContext context);
    }
    public interface ILoadingScreenActions
    {
        void OnContinue(InputAction.CallbackContext context);
    }
}
