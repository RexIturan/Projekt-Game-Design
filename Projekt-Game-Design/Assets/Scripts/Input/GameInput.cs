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
                    ""name"": ""SelectAbility_10"",
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
                    ""action"": ""SelectAbility_10"",
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
        },
        {
            ""name"": ""UI"",
            ""id"": ""b24b720b-ba88-46eb-9f6f-9c74f5144e3c"",
            ""actions"": [
                {
                    ""name"": ""Navigate"",
                    ""type"": ""Value"",
                    ""id"": ""101141d0-796a-48ad-a818-98163f943312"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""5fc23e61-e204-4b28-99f3-235a90f29129"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""fa05b8b5-8676-47c4-b622-14ecb36d0445"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""20ea5686-2426-4298-b93f-f9d24715ca1c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Point"",
                    ""type"": ""PassThrough"",
                    ""id"": ""79470215-0c87-446b-806d-43032f7ed3d8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Click"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a3dd94ad-4830-40cf-a049-77117d229235"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""ScrollWheel"",
                    ""type"": ""PassThrough"",
                    ""id"": ""9c31132d-fa46-4971-82aa-40620e52ce40"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MiddleClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""6bf05c12-907e-4841-b18b-5bcad5dd7a47"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5876fb09-8d08-49d2-afbb-8bfc9d819437"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDevicePosition"",
                    ""type"": ""PassThrough"",
                    ""id"": ""a6c408bf-0c18-4e4a-b427-baf6e36e9add"",
                    ""expectedControlType"": ""Vector3"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TrackedDeviceOrientation"",
                    ""type"": ""PassThrough"",
                    ""id"": ""dde4de3f-9e80-4995-9d70-1e3d88e1a6e6"",
                    ""expectedControlType"": ""Quaternion"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Gamepad"",
                    ""id"": ""63e5880d-496f-406c-9506-b41355797e5a"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""592a1ad5-68ea-4dbc-a15f-491980e4bfc1"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""up"",
                    ""id"": ""2da2673b-51ce-4838-ba40-c81f87377184"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d87bc875-df62-4fae-8850-f092e4ac8ba4"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ca5cd568-2e2f-4af1-844f-387a4c4db85b"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""98b1ba08-bb15-47d4-a466-46d6570033d6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bca59761-68ae-478e-94d6-62a2f4890309"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f17fa8d9-42b2-415e-a1f8-07a9901b43f4"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""1f0c6efa-9aa8-49a2-a0b0-66b3aa4d6651"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6566c741-6892-45a5-9492-d2c9c5f4ea95"",
                    ""path"": ""<Gamepad>/dpad"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Gamepad"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Joystick"",
                    ""id"": ""0dc3a6a0-fa52-40f1-9b2d-b0ad247e50ed"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""d1a67e46-01b1-44cd-a2f1-a447c60ceb73"",
                    ""path"": ""<Joystick>/stick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""77ecae79-1d01-4a0f-8a4c-ccbc47c57aec"",
                    ""path"": ""<Joystick>/stick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""98779eeb-0776-4c94-aeb4-7f13456eb0c4"",
                    ""path"": ""<Joystick>/stick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""61f836ed-cc06-4b85-8415-7616c58c8b00"",
                    ""path"": ""<Joystick>/stick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Joystick"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""f49d19b4-731a-4b70-bee2-d5fdb8c524e4"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Navigate"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""638ad1fd-8c84-49cc-b8b3-bd1fe1b8b581"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""ad6d3fce-a458-4cc3-868a-f4eeff2910eb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""3e67cdfb-dee3-46ac-9757-ed701941e032"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ed2cc36f-82d6-48a3-8765-895a3391aa73"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Navigate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""cd035c03-d7da-4191-8511-5071f73f4e68"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e6ae0e00-e34e-4abd-ba1b-04b337036025"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""55da22a3-b438-41e9-bc63-7d133bb1e3ef"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d5f44e7e-0195-408d-8e48-833fda3ef25b"",
                    ""path"": ""<Pen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b78856f3-4cef-49ec-98a6-76235e6ff195"",
                    ""path"": ""<Touchscreen>/touch*/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Point"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3213d596-1435-4515-ac6e-957de9a47138"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ff0da451-bcdc-4b2a-9548-1be0f2d0f5a5"",
                    ""path"": ""<Pen>/tip"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a3ec086-1c9e-41f9-af31-9b23a653a98f"",
                    ""path"": ""<Touchscreen>/touch*/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f8fa57e5-5e86-4c84-bfe4-c97fafff4e8c"",
                    ""path"": ""<XRController>/trigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3693346e-a362-42a4-b9d6-81400f811bde"",
                    ""path"": ""<Mouse>/scroll"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""ScrollWheel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fc889e62-0043-49fa-8d33-e0c6beab75e7"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""MiddleClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb84a0e3-6a95-47fa-8f9d-e5fcd1a05f0f"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": "";Keyboard&Mouse"",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""21931d28-5d77-4496-8bcc-1e5fd823e191"",
                    ""path"": ""<XRController>/devicePosition"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDevicePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5753aaf9-4e74-480c-9aa4-383699d045ee"",
                    ""path"": ""<XRController>/deviceRotation"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""XR"",
                    ""action"": ""TrackedDeviceOrientation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""228f4461-8217-4d22-810e-a7b3adb2d3a4"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ec678a4-d406-43ee-8b4e-543264a6a59f"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Button With Sign Modifier"",
                    ""id"": ""6eb685e8-9fbb-4240-9dc9-81c1c2c5fe4f"",
                    ""path"": ""ButtonWithSignModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""7ead0ef1-8c73-43ad-88a9-0a693c07e134"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""button"",
                    ""id"": ""c239b3a0-93c7-49f5-8d6d-f34024bbe4f7"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard&Mouse"",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
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
        m_Gameplay_SelectAbility_10 = m_Gameplay.FindAction("SelectAbility_10", throwIfNotFound: true);
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
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Navigate = m_UI.FindAction("Navigate", throwIfNotFound: true);
        m_UI_Submit = m_UI.FindAction("Submit", throwIfNotFound: true);
        m_UI_Cancel = m_UI.FindAction("Cancel", throwIfNotFound: true);
        m_UI_Tab = m_UI.FindAction("Tab", throwIfNotFound: true);
        m_UI_Point = m_UI.FindAction("Point", throwIfNotFound: true);
        m_UI_Click = m_UI.FindAction("Click", throwIfNotFound: true);
        m_UI_ScrollWheel = m_UI.FindAction("ScrollWheel", throwIfNotFound: true);
        m_UI_MiddleClick = m_UI.FindAction("MiddleClick", throwIfNotFound: true);
        m_UI_RightClick = m_UI.FindAction("RightClick", throwIfNotFound: true);
        m_UI_TrackedDevicePosition = m_UI.FindAction("TrackedDevicePosition", throwIfNotFound: true);
        m_UI_TrackedDeviceOrientation = m_UI.FindAction("TrackedDeviceOrientation", throwIfNotFound: true);
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
    private readonly InputAction m_Gameplay_SelectAbility_10;
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
        public InputAction @SelectAbility_10 => m_Wrapper.m_Gameplay_SelectAbility_10;
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
                @SelectAbility_10.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_10;
                @SelectAbility_10.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_10;
                @SelectAbility_10.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnSelectAbility_10;
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
                @SelectAbility_10.started += instance.OnSelectAbility_10;
                @SelectAbility_10.performed += instance.OnSelectAbility_10;
                @SelectAbility_10.canceled += instance.OnSelectAbility_10;
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

    // UI
    private readonly InputActionMap m_UI;
    private IUIActions m_UIActionsCallbackInterface;
    private readonly InputAction m_UI_Navigate;
    private readonly InputAction m_UI_Submit;
    private readonly InputAction m_UI_Cancel;
    private readonly InputAction m_UI_Tab;
    private readonly InputAction m_UI_Point;
    private readonly InputAction m_UI_Click;
    private readonly InputAction m_UI_ScrollWheel;
    private readonly InputAction m_UI_MiddleClick;
    private readonly InputAction m_UI_RightClick;
    private readonly InputAction m_UI_TrackedDevicePosition;
    private readonly InputAction m_UI_TrackedDeviceOrientation;
    public struct UIActions
    {
        private @GameInput m_Wrapper;
        public UIActions(@GameInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Navigate => m_Wrapper.m_UI_Navigate;
        public InputAction @Submit => m_Wrapper.m_UI_Submit;
        public InputAction @Cancel => m_Wrapper.m_UI_Cancel;
        public InputAction @Tab => m_Wrapper.m_UI_Tab;
        public InputAction @Point => m_Wrapper.m_UI_Point;
        public InputAction @Click => m_Wrapper.m_UI_Click;
        public InputAction @ScrollWheel => m_Wrapper.m_UI_ScrollWheel;
        public InputAction @MiddleClick => m_Wrapper.m_UI_MiddleClick;
        public InputAction @RightClick => m_Wrapper.m_UI_RightClick;
        public InputAction @TrackedDevicePosition => m_Wrapper.m_UI_TrackedDevicePosition;
        public InputAction @TrackedDeviceOrientation => m_Wrapper.m_UI_TrackedDeviceOrientation;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void SetCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterface != null)
            {
                @Navigate.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Navigate.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNavigate;
                @Submit.started -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Submit.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnSubmit;
                @Cancel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Cancel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnCancel;
                @Tab.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTab;
                @Point.started -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Point.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnPoint;
                @Click.started -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnClick;
                @ScrollWheel.started -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @ScrollWheel.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnScrollWheel;
                @MiddleClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @MiddleClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnMiddleClick;
                @RightClick.started -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @RightClick.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnRightClick;
                @TrackedDevicePosition.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnTrackedDeviceOrientation;
            }
            m_Wrapper.m_UIActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Navigate.started += instance.OnNavigate;
                @Navigate.performed += instance.OnNavigate;
                @Navigate.canceled += instance.OnNavigate;
                @Submit.started += instance.OnSubmit;
                @Submit.performed += instance.OnSubmit;
                @Submit.canceled += instance.OnSubmit;
                @Cancel.started += instance.OnCancel;
                @Cancel.performed += instance.OnCancel;
                @Cancel.canceled += instance.OnCancel;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @Point.started += instance.OnPoint;
                @Point.performed += instance.OnPoint;
                @Point.canceled += instance.OnPoint;
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @ScrollWheel.started += instance.OnScrollWheel;
                @ScrollWheel.performed += instance.OnScrollWheel;
                @ScrollWheel.canceled += instance.OnScrollWheel;
                @MiddleClick.started += instance.OnMiddleClick;
                @MiddleClick.performed += instance.OnMiddleClick;
                @MiddleClick.canceled += instance.OnMiddleClick;
                @RightClick.started += instance.OnRightClick;
                @RightClick.performed += instance.OnRightClick;
                @RightClick.canceled += instance.OnRightClick;
                @TrackedDevicePosition.started += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.performed += instance.OnTrackedDevicePosition;
                @TrackedDevicePosition.canceled += instance.OnTrackedDevicePosition;
                @TrackedDeviceOrientation.started += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.performed += instance.OnTrackedDeviceOrientation;
                @TrackedDeviceOrientation.canceled += instance.OnTrackedDeviceOrientation;
            }
        }
    }
    public UIActions @UI => new UIActions(this);
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
        void OnSelectAbility_10(InputAction.CallbackContext context);
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
    public interface IUIActions
    {
        void OnNavigate(InputAction.CallbackContext context);
        void OnSubmit(InputAction.CallbackContext context);
        void OnCancel(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnPoint(InputAction.CallbackContext context);
        void OnClick(InputAction.CallbackContext context);
        void OnScrollWheel(InputAction.CallbackContext context);
        void OnMiddleClick(InputAction.CallbackContext context);
        void OnRightClick(InputAction.CallbackContext context);
        void OnTrackedDevicePosition(InputAction.CallbackContext context);
        void OnTrackedDeviceOrientation(InputAction.CallbackContext context);
    }
}
