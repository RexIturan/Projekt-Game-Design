using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class OverlayUIController : MonoBehaviour
{
    // Für die UI Elemente
    private VisualElement OverlayContainer;
    
    // Action Container
    private VisualElement ActionContainer;
    
    // PlayerView Container
    private VisualElement PlayerViewContainer;
    
    // Liste der Ability-Visual-Elements und Icons
    private List<VisualElement> AbilityList;
    private List<AbilitySlot> AbilityIconSlotList = new List<AbilitySlot>();

    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityGameOverlayEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    // Für das ActionMenü
    [SerializeField] private GameObjEventChannelSO VisibilityActionContainerEventChannel;
    [SerializeField] private GameObjActionEventChannelSO ActionsFromPlayerEventChannel;

    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO enableGamplayInput;
    [SerializeField] private TurnIndicatorEventChannelSO endTurnEC;
    
    [Header("Sending and Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;
    
    
    // Callbackfunktion für die Abilitys
    private Action<int> CallBackAction;

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
        OverlayContainer = root.Q<VisualElement>("OverlayContainer");
        PlayerViewContainer = root.Q<VisualElement>("PlayerViewContainer");
        OverlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        OverlayContainer.Q<Button>("EndTurnButton").clicked += HandleEndTurnUI;
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityGameOverlayEventChannel.OnEventRaised  += HandleGameOverlay;
        //VisibilityActionContainerEventChannel.OnEventRaised += HideActionMenu;

        InitializeAbilityList();
        ActionsFromPlayerEventChannel.OnEventRaised += HandlePlayerSelected;
    }

    private void HandleEndTurnUI() {
        // Debug.Log("end turn ui");
        endTurnEC.RaiseEvent(ETurnIndicator.player);
    }

    void HandleGameOverlay(bool value)
    {
        if (value)
        {
            enableGamplayInput.RaiseEvent();
            OverlayContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            OverlayContainer.style.display = DisplayStyle.None;
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

    void CallBackMouseDownAbility(MouseDownEvent evt, int abilityID)
    {
        CallBackAction(abilityID);
    }
    
    void CallBackMouseEnterAbility(MouseEnterEvent evt, string description)
    {
        Label text = OverlayContainer.Q<Label>("AbilityDescription");
        text.style.display = DisplayStyle.Flex;
        text.text = description;
    }
    
    void CallBackMouseLeaveAbility(MouseLeaveEvent evt)
    {
        Label text = OverlayContainer.Q<Label>("AbilityDescription");
        text.style.display = DisplayStyle.None;
    }

    void HandlePlayerSelected(GameObject obj, Action<int> callBackAction)
    {
        // Anzeigen der notwendigen Komponenten
        ShowActionMenu();
        ShowPlayerViewContainer();
        
        CallBackAction = callBackAction;
        FlushAbilityListIcons();
        List<AbilitySO> abilities = new List<AbilitySO>(obj.GetComponent<PlayerCharacterSC>().Abilitys);
        int counter = 0;
        
        foreach (var ability in abilities)
        {
            AbilityIconSlotList[counter].HoldAbility(ability);
            AbilityIconSlotList[counter].RegisterCallback<MouseDownEvent, int>(CallBackMouseDownAbility,AbilityIconSlotList[counter].AbilityID);
            AbilityIconSlotList[counter].RegisterCallback<MouseEnterEvent, string>(CallBackMouseEnterAbility,ability.description);
            AbilityIconSlotList[counter].RegisterCallback<MouseLeaveEvent>(CallBackMouseLeaveAbility);
            counter++;
        }

        RefreshStats(obj);
        //TODO: Hier die Stats einbauen, für den ausgewählten Spieler
    }

    void RefreshStats(GameObject obj)
    {
        //VisualElement manaBar = PlayerViewContainer.Q<VisualElement>("ProgressBarManaOverlay");
        VisualElement healthBar = PlayerViewContainer.Q<VisualElement>("ProgressBarHealthOverlay");
        VisualElement abilityBar = PlayerViewContainer.Q<VisualElement>("ProgressBarAbilityOverlay");

        PlayerCharacterSC playerSC = obj.GetComponent<PlayerCharacterSC>(); 
        CharacterStats playerStats = obj.GetComponent<PlayerCharacterSC>().CurrentStats; 
        
        healthBar.style.width = new StyleLength(Length.Percent((100* (float)playerSC.HealthPoints/playerStats.maxHealthPoints)));
        abilityBar.style.width = new StyleLength(Length.Percent((100* (float)playerSC.EnergyPoints/playerStats.maxEnergy)));
        //manaBar.style.width = new StyleLength(Length.Percent((100* (float)playerSC.EnergyPoints/playerStats.maxEnergy)));
        
        // Labels für Stats
        PlayerViewContainer.Q<Label>("StrengthLabel").text = playerStats.strength.ToString();
        PlayerViewContainer.Q<Label>("DexterityLabel").text = playerStats.dexterity.ToString();
        PlayerViewContainer.Q<Label>("IntelligenceLabel").text = playerStats.intelligence.ToString();
        PlayerViewContainer.Q<Label>("MovementLabel").text = playerStats.viewDistance.ToString();
    }

    void ShowMenu()
    {
        VisibilityMenuEventChannel.RaiseEvent(true);
    }

    void HideActionMenu(GameObject obj)
    {
        ActionContainer.style.display = DisplayStyle.None;
    }

    void ShowPlayerViewContainer()
    {
        PlayerViewContainer.style.display = DisplayStyle.Flex;
    }
    
    void HidePlayerViewContainer()
    {
        PlayerViewContainer.style.display = DisplayStyle.None;
    }
    
    void ShowActionMenu()
    {
        ActionContainer.style.display = DisplayStyle.Flex;
    }
}