using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class OverlayUIController : MonoBehaviour
{
    // Für die UI Elemente
    private VisualElement overlayContainer;
    
    // Action Container
    private VisualElement ActionContainer;
    
    // Liste der Ability-Visual-Elements und Icons
    private List<VisualElement> AbilityList;
    private List<AbilitySlot> AbilityIconSlotList = new List<AbilitySlot>();

    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityGameOverlayEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    // Für das ActionMenü
    [SerializeField] private BoolEventChannelSO VisibilityActionContainerEventChannel;
    [SerializeField] private GameObjEventChannelSO ActionsFromPlayerEventChannel;

    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO enableGamplayInput;
    
    [Header("Sending and Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;

    // Start is called before the first frame update
    void Start()
    {
        
        // overlayContainer = root.Q<VisualElement>("OverlayContainer");
        // overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        
    }

    private void Awake()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        ActionContainer = root.Q<VisualElement>("ActionContainer");
        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityGameOverlayEventChannel.OnEventRaised  += HandleGameOverlay;
        VisibilityActionContainerEventChannel.OnEventRaised += HandleActionMenuVisibility;

        InitializeAbilityList();
        ActionsFromPlayerEventChannel.OnEventRaised += HandlePlayerSelected;
    }

    void HandleGameOverlay(bool value)
    {
        if (value)
        {
            enableGamplayInput.RaiseEvent();
            overlayContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            overlayContainer.style.display = DisplayStyle.None;
        }
        
    }
    
    void HandleActionMenuVisibility(bool value)
    {
        if (value)
        {
            ShowActionMenu();
        }
        else
        {
            HideActionMenu();
        }
        
    }
    void HandleOtherScreensOpened(bool value)
    {
        HandleGameOverlay(false);
    }

    void InitializeAbilityList()
    {
        AbilityList = new List<VisualElement>();
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action1"));
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action2"));
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action3"));
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action4"));
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action5"));
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action6"));
        AbilityList.Add(ActionContainer.Q<VisualElement>("Action7"));

        AbilityIconSlotList = new List<AbilitySlot>();
        int counter = 0;

        foreach (var ability in AbilityList)
        {
            AbilityIconSlotList.Add(new AbilitySlot());
            ability.Add(AbilityIconSlotList[counter++]);
        }
    }

    void FlushAbilityListIcons()
    {
        foreach (var abilitySlot in AbilityIconSlotList)
        {
            if (abilitySlot != null && abilitySlot.AbilityID != -1)
            {
                abilitySlot.DropAbility();
            }
        }
    }

    void HandlePlayerSelected(GameObject obj)
    {
        FlushAbilityListIcons();
        List<AbilitySO> abilities = new List<AbilitySO>(obj.GetComponent<PlayerCharacterSC>().Abilitys);
        int counter = 0;
        
        foreach (var ability in abilities)
        {
            AbilityIconSlotList[counter++].HoldAbility(ability);
        }
        
        //TODO: Hier die Stats einbauen, für den ausgewählten Spieler
    }

    void ShowMenu()
    {
        VisibilityMenuEventChannel.RaiseEvent(true);
    }

    void HideActionMenu()
    {
        ActionContainer.style.display = DisplayStyle.None;
    }

    void ShowActionMenu()
    {
        ActionContainer.style.display = DisplayStyle.Flex;
    }

    void HandleClickOnAbility()
    {
        
    }
    
    
}