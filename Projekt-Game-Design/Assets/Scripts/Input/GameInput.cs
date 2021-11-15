// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/GameInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @GameInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @GameInput()
    {
        asset = InputActionAsset.FromJson(@"{
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
                },
                {
                    ""name"": ""Help"",
                    ""type"": ""Button"",
                    ""id"": ""8ad52491-5213-4c98-af42-9a54849011e6"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""aa3b30ad-bfa8-43ba-b6bc-e800330eab89"",
                    ""path"": ""<Keyboard>/h"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Help"",
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
                },
                {
                    ""name"": ""Select"",
                    ""type"": ""Button"",
                    ""id"": ""72519a15-4b3c-4a4c-8028-9f87df117735"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Paint"",
                    ""type"": ""Button"",
                    ""id"": ""3b530e48-1595-4ea1-99fa-7c907370d3ee"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Box"",
                    ""type"": ""Button"",
                    ""id"": ""88d68441-6200-4960-ab90-4c1d91d8b23d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Fill"",
                    ""type"": ""Button"",
                    ""id"": ""e65d6032-6861-491c-977c-526f5833273b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ResetLevel"",
                    ""type"": ""Button"",
                    ""id"": ""6817b2fd-2740-4539-83a2-1c0ed4d4eb21"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""af168704-90de-49a0-8c7a-8b8985df6c98"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cfbd23ac-cc7f-456c-accd-7c3cf19c2b3e"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Paint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""62936ca9-76d7-42c2-b4dd-df9b1b360dd9"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Box"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""761cc7cf-f7dc-49be-a03a-0f127014596e"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Fill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb95693f-8872-46f2-a38e-faf1eb8e4cb0"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResetLevel"",
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
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_EndTurn = m_Gameplay.FindAction("EndTurn", throwIfNotFound: true);
        m_Gameplay_Menu = m_Gameplay.FindAction("Menu", throwIfNotFound: true);
        m_Gameplay_Inventory = m_Gameplay.FindAction("Inventory", throwIfNotFound: true);
        m_Gameplay_MouseClicked = m_Gameplay.FindAction("MouseClicked", throwIfNotFound: true);
        m_Gameplay_Help = m_Gameplay.FindAction("Help", throwIfNotFound: true);
        // Inventory
        m_Inventory = asset.FindActionMap("Inventory", throwIfNotFound: true);
        m_Inventory_CancelInventory = m_Inventory.FindAction("CancelInventory", throwIfNotFound: true);
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_MoveCamera = m_Camera.FindAction("MoveCamera", throwIfNotFound: true);
        m_Camera_RotateCamera = m_Camera.FindAction("RotateCamera", throwIfNotFound: true);
        m_Camera_CameraZoom = m_Camera.FindAction("CameraZoom", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_Confirm = m_Menu.FindAction("Confirm", throwIfNotFound: true);
        m_Menu_Cancel = m_Menu.FindAction("Cancel", throwIfNotFound: true);
        // LoadingScreen
        m_LoadingScreen = asset.FindActionMap("LoadingScreen", throwIfNotFound: true);
        m_LoadingScreen_Continue = m_LoadingScreen.FindAction("Continue", throwIfNotFound: true);
        // LevelEditor
        m_LevelEditor = asset.FindActionMap("LevelEditor", throwIfNotFound: true);
        m_LevelEditor_Menu = m_LevelEditor.FindAction("Menu", throwIfNotFound: true);
        m_LevelEditor_Select = m_LevelEditor.FindAction("Select", throwIfNotFound: true);
        m_LevelEditor_Paint = m_LevelEditor.FindAction("Paint", throwIfNotFound: true);
        m_LevelEditor_Box = m_LevelEditor.FindAction("Box", throwIfNotFound: true);
        m_LevelEditor_Fill = m_LevelEditor.FindAction("Fill", throwIfNotFound: true);
        m_LevelEditor_ResetLevel = m_LevelEditor.FindAction("ResetLevel", throwIfNotFound: true);
        // Pathfinding Debug
        m_PathfindingDebug = asset.FindActionMap("Pathfinding Debug", throwIfNotFound: true);
        m_PathfindingDebug_Toggle = m_PathfindingDebug.FindAction("Toggle", throwIfNotFound: true);
        m_PathfindingDebug_Step = m_PathfindingDebug.FindAction("Step", throwIfNotFound: true);
        m_PathfindingDebug_ShowCompletePath = m_PathfindingDebug.FindAction("ShowCompletePath", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_EndTurn;
    private readonly InputAction m_Gameplay_Menu;
    private readonly InputAction m_Gameplay_Inventory;
    private readonly InputAction m_Gameplay_MouseClicked;
    private readonly InputAction m_Gameplay_Help;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @EndTurn => m_Wrapper.m_Gameplay_EndTurn;
        public InputAction @Menu => m_Wrapper.m_Gameplay_Menu;
        public InputAction @Inventory => m_Wrapper.m_Gameplay_Inventory;
        public InputAction @MouseClicked => m_Wrapper.m_Gameplay_MouseClicked;
        public InputAction @Help => m_Wrapper.m_Gameplay_Help;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @EndTurn.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndTurn;
                @EndTurn.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndTurn;
                @EndTurn.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEndTurn;
                @Menu.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMenu;
                @Inventory.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @MouseClicked.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClicked;
                @MouseClicked.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClicked;
                @MouseClicked.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClicked;
                @Help.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHelp;
                @Help.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHelp;
                @Help.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHelp;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
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
                @Help.started += instance.OnHelp;
                @Help.performed += instance.OnHelp;
                @Help.canceled += instance.OnHelp;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // Inventory
    private readonly InputActionMap m_Inventory;
    private IInventoryActions m_InventoryActionsCallbackInterface;
    private readonly InputAction m_Inventory_CancelInventory;
    public struct InventoryActions
    {
        private @GameInput m_Wrapper;
        public InventoryActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @CancelInventory => m_Wrapper.m_Inventory_CancelInventory;
        public InputActionMap Get() { return m_Wrapper.m_Inventory; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(InventoryActions set) { return set.Get(); }
        public void SetCallbacks(IInventoryActions instance)
        {
            if (m_Wrapper.m_InventoryActionsCallbackInterface != null)
            {
                @CancelInventory.started -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCancelInventory;
                @CancelInventory.performed -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCancelInventory;
                @CancelInventory.canceled -= m_Wrapper.m_InventoryActionsCallbackInterface.OnCancelInventory;
            }
            m_Wrapper.m_InventoryActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CancelInventory.started += instance.OnCancelInventory;
                @CancelInventory.performed += instance.OnCancelInventory;
                @CancelInventory.canceled += instance.OnCancelInventory;
            }
        }
    }
    public InventoryActions @Inventory => new InventoryActions(this);

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_MoveCamera;
    private readonly InputAction m_Camera_RotateCamera;
    private readonly InputAction m_Camera_CameraZoom;
    public struct CameraActions
    {
        private @GameInput m_Wrapper;
        public CameraActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveCamera => m_Wrapper.m_Camera_MoveCamera;
        public InputAction @RotateCamera => m_Wrapper.m_Camera_RotateCamera;
        public InputAction @CameraZoom => m_Wrapper.m_Camera_CameraZoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @MoveCamera.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCamera;
                @MoveCamera.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMoveCamera;
                @RotateCamera.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCamera;
                @RotateCamera.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnRotateCamera;
                @CameraZoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraZoom;
                @CameraZoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnCameraZoom;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
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
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_Confirm;
    private readonly InputAction m_Menu_Cancel;
    public struct MenuActions
    {
        private @GameInput m_Wrapper;
        public MenuActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Confirm => m_Wrapper.m_Menu_Confirm;
        public InputAction @Cancel => m_Wrapper.m_Menu_Cancel;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @Confirm.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Confirm.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnConfirm;
                @Cancel.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnCancel;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
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

    // LoadingScreen
    private readonly InputActionMap m_LoadingScreen;
    private ILoadingScreenActions m_LoadingScreenActionsCallbackInterface;
    private readonly InputAction m_LoadingScreen_Continue;
    public struct LoadingScreenActions
    {
        private @GameInput m_Wrapper;
        public LoadingScreenActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Continue => m_Wrapper.m_LoadingScreen_Continue;
        public InputActionMap Get() { return m_Wrapper.m_LoadingScreen; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LoadingScreenActions set) { return set.Get(); }
        public void SetCallbacks(ILoadingScreenActions instance)
        {
            if (m_Wrapper.m_LoadingScreenActionsCallbackInterface != null)
            {
                @Continue.started -= m_Wrapper.m_LoadingScreenActionsCallbackInterface.OnContinue;
                @Continue.performed -= m_Wrapper.m_LoadingScreenActionsCallbackInterface.OnContinue;
                @Continue.canceled -= m_Wrapper.m_LoadingScreenActionsCallbackInterface.OnContinue;
            }
            m_Wrapper.m_LoadingScreenActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Continue.started += instance.OnContinue;
                @Continue.performed += instance.OnContinue;
                @Continue.canceled += instance.OnContinue;
            }
        }
    }
    public LoadingScreenActions @LoadingScreen => new LoadingScreenActions(this);

    // LevelEditor
    private readonly InputActionMap m_LevelEditor;
    private ILevelEditorActions m_LevelEditorActionsCallbackInterface;
    private readonly InputAction m_LevelEditor_Menu;
    private readonly InputAction m_LevelEditor_Select;
    private readonly InputAction m_LevelEditor_Paint;
    private readonly InputAction m_LevelEditor_Box;
    private readonly InputAction m_LevelEditor_Fill;
    private readonly InputAction m_LevelEditor_ResetLevel;
    public struct LevelEditorActions
    {
        private @GameInput m_Wrapper;
        public LevelEditorActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menu => m_Wrapper.m_LevelEditor_Menu;
        public InputAction @Select => m_Wrapper.m_LevelEditor_Select;
        public InputAction @Paint => m_Wrapper.m_LevelEditor_Paint;
        public InputAction @Box => m_Wrapper.m_LevelEditor_Box;
        public InputAction @Fill => m_Wrapper.m_LevelEditor_Fill;
        public InputAction @ResetLevel => m_Wrapper.m_LevelEditor_ResetLevel;
        public InputActionMap Get() { return m_Wrapper.m_LevelEditor; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(LevelEditorActions set) { return set.Get(); }
        public void SetCallbacks(ILevelEditorActions instance)
        {
            if (m_Wrapper.m_LevelEditorActionsCallbackInterface != null)
            {
                @Menu.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnMenu;
                @Select.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnSelect;
                @Select.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnSelect;
                @Select.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnSelect;
                @Paint.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnPaint;
                @Paint.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnPaint;
                @Paint.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnPaint;
                @Box.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnBox;
                @Box.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnBox;
                @Box.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnBox;
                @Fill.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnFill;
                @Fill.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnFill;
                @Fill.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnFill;
                @ResetLevel.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnResetLevel;
                @ResetLevel.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnResetLevel;
                @ResetLevel.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnResetLevel;
            }
            m_Wrapper.m_LevelEditorActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Select.started += instance.OnSelect;
                @Select.performed += instance.OnSelect;
                @Select.canceled += instance.OnSelect;
                @Paint.started += instance.OnPaint;
                @Paint.performed += instance.OnPaint;
                @Paint.canceled += instance.OnPaint;
                @Box.started += instance.OnBox;
                @Box.performed += instance.OnBox;
                @Box.canceled += instance.OnBox;
                @Fill.started += instance.OnFill;
                @Fill.performed += instance.OnFill;
                @Fill.canceled += instance.OnFill;
                @ResetLevel.started += instance.OnResetLevel;
                @ResetLevel.performed += instance.OnResetLevel;
                @ResetLevel.canceled += instance.OnResetLevel;
            }
        }
    }
    public LevelEditorActions @LevelEditor => new LevelEditorActions(this);

    // Pathfinding Debug
    private readonly InputActionMap m_PathfindingDebug;
    private IPathfindingDebugActions m_PathfindingDebugActionsCallbackInterface;
    private readonly InputAction m_PathfindingDebug_Toggle;
    private readonly InputAction m_PathfindingDebug_Step;
    private readonly InputAction m_PathfindingDebug_ShowCompletePath;
    public struct PathfindingDebugActions
    {
        private @GameInput m_Wrapper;
        public PathfindingDebugActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Toggle => m_Wrapper.m_PathfindingDebug_Toggle;
        public InputAction @Step => m_Wrapper.m_PathfindingDebug_Step;
        public InputAction @ShowCompletePath => m_Wrapper.m_PathfindingDebug_ShowCompletePath;
        public InputActionMap Get() { return m_Wrapper.m_PathfindingDebug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PathfindingDebugActions set) { return set.Get(); }
        public void SetCallbacks(IPathfindingDebugActions instance)
        {
            if (m_Wrapper.m_PathfindingDebugActionsCallbackInterface != null)
            {
                @Toggle.started -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnToggle;
                @Toggle.performed -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnToggle;
                @Toggle.canceled -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnToggle;
                @Step.started -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnStep;
                @Step.performed -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnStep;
                @Step.canceled -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnStep;
                @ShowCompletePath.started -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnShowCompletePath;
                @ShowCompletePath.performed -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnShowCompletePath;
                @ShowCompletePath.canceled -= m_Wrapper.m_PathfindingDebugActionsCallbackInterface.OnShowCompletePath;
            }
            m_Wrapper.m_PathfindingDebugActionsCallbackInterface = instance;
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
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnEndTurn(InputAction.CallbackContext context);
        void OnMenu(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMouseClicked(InputAction.CallbackContext context);
        void OnHelp(InputAction.CallbackContext context);
    }
    public interface IInventoryActions
    {
        void OnCancelInventory(InputAction.CallbackContext context);
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
    public interface ILoadingScreenActions
    {
        void OnContinue(InputAction.CallbackContext context);
    }
    public interface ILevelEditorActions
    {
        void OnMenu(InputAction.CallbackContext context);
        void OnSelect(InputAction.CallbackContext context);
        void OnPaint(InputAction.CallbackContext context);
        void OnBox(InputAction.CallbackContext context);
        void OnFill(InputAction.CallbackContext context);
        void OnResetLevel(InputAction.CallbackContext context);
    }
    public interface IPathfindingDebugActions
    {
        void OnToggle(InputAction.CallbackContext context);
        void OnStep(InputAction.CallbackContext context);
        void OnShowCompletePath(InputAction.CallbackContext context);
    }
}
