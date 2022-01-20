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
                },
                {
                    ""name"": ""SelectAbility_1"",
                    ""type"": ""Button"",
                    ""id"": ""3d7abaad-7467-4764-9e8a-4dd0d079ac0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_2"",
                    ""type"": ""Button"",
                    ""id"": ""96759fd7-32cf-4182-a8d0-0dbb07ea41f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_3"",
                    ""type"": ""Button"",
                    ""id"": ""7083ba25-ce31-44d5-81c5-b5fe00c19a1b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_4"",
                    ""type"": ""Button"",
                    ""id"": ""8e669e36-64af-426b-99e4-79137e6c7e2f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_5"",
                    ""type"": ""Button"",
                    ""id"": ""56d68a15-d394-4450-b6d8-ce0af7b33628"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_6"",
                    ""type"": ""Button"",
                    ""id"": ""5e5b2553-ea98-4c38-93cc-6d842d3799cb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_7"",
                    ""type"": ""Button"",
                    ""id"": ""c3a3f65c-57a7-4c14-9633-0345e18be06e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_8"",
                    ""type"": ""Button"",
                    ""id"": ""f9374130-9019-4c76-8bc4-3c52fa1a8fda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_9"",
                    ""type"": ""Button"",
                    ""id"": ""17065a47-3c67-458c-87b3-1ae46f5c29b3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectAbility_0"",
                    ""type"": ""Button"",
                    ""id"": ""6765738c-7c8d-4272-8413-0efd945dfbc2"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""b05e1310-85bf-4ab8-9e26-e3af2b68fd93"",
                    ""path"": ""<Keyboard>/1"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_1"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""512fba3d-e08a-41ed-be75-f8fdf64cae5c"",
                    ""path"": ""<Keyboard>/2"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_2"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f0bd1c7d-b6d6-4541-a3ab-c5f98f7d6a21"",
                    ""path"": ""<Keyboard>/3"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_3"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5835e803-c209-4453-a265-6f58d333c758"",
                    ""path"": ""<Keyboard>/4"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_4"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f06bc0f3-00d4-4b4d-a20c-6e627ab08c9a"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_5"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7d3ff16b-17f1-4f91-81f3-eb5e3444cdf2"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_6"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0563b8f1-fdea-4ba2-b418-6a2ea0225db1"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_7"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8df83850-43bd-48b5-bee0-a522f926aa2d"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_8"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d2dfbe8a-7eda-4670-b7ff-8e469ca44cd4"",
                    ""path"": ""<Keyboard>/9"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_9"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5bfffa2b-b24a-4b2f-8536-0d33870c8279"",
                    ""path"": ""<Keyboard>/0"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""SelectAbility_0"",
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
                },
                {
                    ""name"": ""Terrain"",
                    ""type"": ""Button"",
                    ""id"": ""bd8afcb7-85dc-4b91-af01-e6c4f55c72f3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Item"",
                    ""type"": ""Button"",
                    ""id"": ""bc69cd6b-7c70-43d5-93e1-384f83dff62e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Character"",
                    ""type"": ""Button"",
                    ""id"": ""5f3d0037-30f6-49f1-adf9-99dc1b2c1eef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Objects"",
                    ""type"": ""Button"",
                    ""id"": ""e7b80cc6-fafe-4609-ba5f-3967d81e38a5"",
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
                },
                {
                    ""name"": """",
                    ""id"": ""f9b6801b-4f88-4942-9a82-29796f1c9faa"",
                    ""path"": ""<Keyboard>/5"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Terrain"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ca274ee0-a9c9-4cf8-842d-fc69f1ee0429"",
                    ""path"": ""<Keyboard>/6"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Item"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""845f84f3-6bdc-48a5-b684-7d76555cc0a9"",
                    ""path"": ""<Keyboard>/7"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Character"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""49166d04-8b9a-432e-a923-0b90e58aaf60"",
                    ""path"": ""<Keyboard>/8"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Objects"",
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
        m_Gameplay_SelectAbility_1 = m_Gameplay.FindAction("SelectAbility_1", throwIfNotFound: true);
        m_Gameplay_SelectAbility_2 = m_Gameplay.FindAction("SelectAbility_2", throwIfNotFound: true);
        m_Gameplay_SelectAbility_3 = m_Gameplay.FindAction("SelectAbility_3", throwIfNotFound: true);
        m_Gameplay_SelectAbility_4 = m_Gameplay.FindAction("SelectAbility_4", throwIfNotFound: true);
        m_Gameplay_SelectAbility_5 = m_Gameplay.FindAction("SelectAbility_5", throwIfNotFound: true);
        m_Gameplay_SelectAbility_6 = m_Gameplay.FindAction("SelectAbility_6", throwIfNotFound: true);
        m_Gameplay_SelectAbility_7 = m_Gameplay.FindAction("SelectAbility_7", throwIfNotFound: true);
        m_Gameplay_SelectAbility_8 = m_Gameplay.FindAction("SelectAbility_8", throwIfNotFound: true);
        m_Gameplay_SelectAbility_9 = m_Gameplay.FindAction("SelectAbility_9", throwIfNotFound: true);
        m_Gameplay_SelectAbility_0 = m_Gameplay.FindAction("SelectAbility_0", throwIfNotFound: true);
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
        m_LevelEditor_Terrain = m_LevelEditor.FindAction("Terrain", throwIfNotFound: true);
        m_LevelEditor_Item = m_LevelEditor.FindAction("Item", throwIfNotFound: true);
        m_LevelEditor_Character = m_LevelEditor.FindAction("Character", throwIfNotFound: true);
        m_LevelEditor_Objects = m_LevelEditor.FindAction("Objects", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_SelectAbility_1;
    private readonly InputAction m_Gameplay_SelectAbility_2;
    private readonly InputAction m_Gameplay_SelectAbility_3;
    private readonly InputAction m_Gameplay_SelectAbility_4;
    private readonly InputAction m_Gameplay_SelectAbility_5;
    private readonly InputAction m_Gameplay_SelectAbility_6;
    private readonly InputAction m_Gameplay_SelectAbility_7;
    private readonly InputAction m_Gameplay_SelectAbility_8;
    private readonly InputAction m_Gameplay_SelectAbility_9;
    private readonly InputAction m_Gameplay_SelectAbility_0;
    public struct GameplayActions
    {
        private @GameInput m_Wrapper;
        public GameplayActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @EndTurn => m_Wrapper.m_Gameplay_EndTurn;
        public InputAction @Menu => m_Wrapper.m_Gameplay_Menu;
        public InputAction @Inventory => m_Wrapper.m_Gameplay_Inventory;
        public InputAction @MouseClicked => m_Wrapper.m_Gameplay_MouseClicked;
        public InputAction @Help => m_Wrapper.m_Gameplay_Help;
        public InputAction @SelectAbility_1 => m_Wrapper.m_Gameplay_SelectAbility_1;
        public InputAction @SelectAbility_2 => m_Wrapper.m_Gameplay_SelectAbility_2;
        public InputAction @SelectAbility_3 => m_Wrapper.m_Gameplay_SelectAbility_3;
        public InputAction @SelectAbility_4 => m_Wrapper.m_Gameplay_SelectAbility_4;
        public InputAction @SelectAbility_5 => m_Wrapper.m_Gameplay_SelectAbility_5;
        public InputAction @SelectAbility_6 => m_Wrapper.m_Gameplay_SelectAbility_6;
        public InputAction @SelectAbility_7 => m_Wrapper.m_Gameplay_SelectAbility_7;
        public InputAction @SelectAbility_8 => m_Wrapper.m_Gameplay_SelectAbility_8;
        public InputAction @SelectAbility_9 => m_Wrapper.m_Gameplay_SelectAbility_9;
        public InputAction @SelectAbility_0 => m_Wrapper.m_Gameplay_SelectAbility_0;
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
                @SelectAbility_1.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_1;
                @SelectAbility_1.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_1;
                @SelectAbility_1.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_1;
                @SelectAbility_2.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_2;
                @SelectAbility_2.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_2;
                @SelectAbility_2.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_2;
                @SelectAbility_3.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_3;
                @SelectAbility_3.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_3;
                @SelectAbility_3.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_3;
                @SelectAbility_4.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_4;
                @SelectAbility_4.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_4;
                @SelectAbility_4.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_4;
                @SelectAbility_5.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_5;
                @SelectAbility_5.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_5;
                @SelectAbility_5.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_5;
                @SelectAbility_6.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_6;
                @SelectAbility_6.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_6;
                @SelectAbility_6.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_6;
                @SelectAbility_7.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_7;
                @SelectAbility_7.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_7;
                @SelectAbility_7.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_7;
                @SelectAbility_8.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_8;
                @SelectAbility_8.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_8;
                @SelectAbility_8.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_8;
                @SelectAbility_9.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_9;
                @SelectAbility_9.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_9;
                @SelectAbility_9.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_9;
                @SelectAbility_0.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_0;
                @SelectAbility_0.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_0;
                @SelectAbility_0.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_0;
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
                @SelectAbility_1.started += instance.OnSelectAbility_1;
                @SelectAbility_1.performed += instance.OnSelectAbility_1;
                @SelectAbility_1.canceled += instance.OnSelectAbility_1;
                @SelectAbility_2.started += instance.OnSelectAbility_2;
                @SelectAbility_2.performed += instance.OnSelectAbility_2;
                @SelectAbility_2.canceled += instance.OnSelectAbility_2;
                @SelectAbility_3.started += instance.OnSelectAbility_3;
                @SelectAbility_3.performed += instance.OnSelectAbility_3;
                @SelectAbility_3.canceled += instance.OnSelectAbility_3;
                @SelectAbility_4.started += instance.OnSelectAbility_4;
                @SelectAbility_4.performed += instance.OnSelectAbility_4;
                @SelectAbility_4.canceled += instance.OnSelectAbility_4;
                @SelectAbility_5.started += instance.OnSelectAbility_5;
                @SelectAbility_5.performed += instance.OnSelectAbility_5;
                @SelectAbility_5.canceled += instance.OnSelectAbility_5;
                @SelectAbility_6.started += instance.OnSelectAbility_6;
                @SelectAbility_6.performed += instance.OnSelectAbility_6;
                @SelectAbility_6.canceled += instance.OnSelectAbility_6;
                @SelectAbility_7.started += instance.OnSelectAbility_7;
                @SelectAbility_7.performed += instance.OnSelectAbility_7;
                @SelectAbility_7.canceled += instance.OnSelectAbility_7;
                @SelectAbility_8.started += instance.OnSelectAbility_8;
                @SelectAbility_8.performed += instance.OnSelectAbility_8;
                @SelectAbility_8.canceled += instance.OnSelectAbility_8;
                @SelectAbility_9.started += instance.OnSelectAbility_9;
                @SelectAbility_9.performed += instance.OnSelectAbility_9;
                @SelectAbility_9.canceled += instance.OnSelectAbility_9;
                @SelectAbility_0.started += instance.OnSelectAbility_0;
                @SelectAbility_0.performed += instance.OnSelectAbility_0;
                @SelectAbility_0.canceled += instance.OnSelectAbility_0;
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
    private readonly InputAction m_LevelEditor_Terrain;
    private readonly InputAction m_LevelEditor_Item;
    private readonly InputAction m_LevelEditor_Character;
    private readonly InputAction m_LevelEditor_Objects;
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
        public InputAction @Terrain => m_Wrapper.m_LevelEditor_Terrain;
        public InputAction @Item => m_Wrapper.m_LevelEditor_Item;
        public InputAction @Character => m_Wrapper.m_LevelEditor_Character;
        public InputAction @Objects => m_Wrapper.m_LevelEditor_Objects;
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
                @Terrain.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnTerrain;
                @Terrain.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnTerrain;
                @Terrain.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnTerrain;
                @Item.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnItem;
                @Item.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnItem;
                @Item.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnItem;
                @Character.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnCharacter;
                @Character.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnCharacter;
                @Character.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnCharacter;
                @Objects.started -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnObjects;
                @Objects.performed -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnObjects;
                @Objects.canceled -= m_Wrapper.m_LevelEditorActionsCallbackInterface.OnObjects;
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
                @Terrain.started += instance.OnTerrain;
                @Terrain.performed += instance.OnTerrain;
                @Terrain.canceled += instance.OnTerrain;
                @Item.started += instance.OnItem;
                @Item.performed += instance.OnItem;
                @Item.canceled += instance.OnItem;
                @Character.started += instance.OnCharacter;
                @Character.performed += instance.OnCharacter;
                @Character.canceled += instance.OnCharacter;
                @Objects.started += instance.OnObjects;
                @Objects.performed += instance.OnObjects;
                @Objects.canceled += instance.OnObjects;
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
        void OnSelectAbility_1(InputAction.CallbackContext context);
        void OnSelectAbility_2(InputAction.CallbackContext context);
        void OnSelectAbility_3(InputAction.CallbackContext context);
        void OnSelectAbility_4(InputAction.CallbackContext context);
        void OnSelectAbility_5(InputAction.CallbackContext context);
        void OnSelectAbility_6(InputAction.CallbackContext context);
        void OnSelectAbility_7(InputAction.CallbackContext context);
        void OnSelectAbility_8(InputAction.CallbackContext context);
        void OnSelectAbility_9(InputAction.CallbackContext context);
        void OnSelectAbility_0(InputAction.CallbackContext context);
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
        void OnTerrain(InputAction.CallbackContext context);
        void OnItem(InputAction.CallbackContext context);
        void OnCharacter(InputAction.CallbackContext context);
        void OnObjects(InputAction.CallbackContext context);
    }
    public interface IPathfindingDebugActions
    {
        void OnToggle(InputAction.CallbackContext context);
        void OnStep(InputAction.CallbackContext context);
        void OnShowCompletePath(InputAction.CallbackContext context);
    }
}
