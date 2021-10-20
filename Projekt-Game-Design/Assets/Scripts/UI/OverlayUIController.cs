using System;
using System.Collections.Generic;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using UnityEngine;
using UnityEngine.UIElements;


public class OverlayUIController : MonoBehaviour {
    // Für die UI Elemente
    private VisualElement _overlayContainer;

    // Action Container
    private VisualElement _actionContainer;

    // PlayerView Container
    private VisualElement _playerViewContainer;

    private TemplateContainer _turnIndicator;

    // Liste der Ability-Visual-Elements und Icons
    private List<VisualElement> _abilityList;
    private List<AbilitySlot> _abilityIconSlotList = new List<AbilitySlot>();

    [Header("Receiving Events On")] 
    [SerializeField] private BoolEventChannelSO showGameOverlayEC;
    [SerializeField] private BoolEventChannelSO showTurnIndicatorEC;
    [SerializeField] private BoolEventChannelSO visibilityInventoryEventChannel;

    // Action Menu
    [SerializeField] private GameObjEventChannelSO playerDeselectedEC;
    [SerializeField] private GameObjActionEventChannelSO playerSelectedEC;

    [Header("Sending Events On")] [SerializeField]
    private VoidEventChannelSO enableGamplayInput;

    [SerializeField] private EFactionEventChannelSO endTurnEC;

    [Header("Sending and Receiving Events On")] [SerializeField]
    private BoolEventChannelSO visibilityMenuEventChannel;


    // Callbackfunktion für die Abilitys
    private Action<int> _callBackAction;

    // Start is called before the first frame update
    void Start() {
        // overlayContainer = root.Q<VisualElement>("OverlayContainer");
        // overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
    }

    private void Awake() {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        _actionContainer = root.Q<VisualElement>("ActionContainer");
        _overlayContainer = root.Q<VisualElement>("OverlayContainer");
        _playerViewContainer = root.Q<VisualElement>("PlayerViewContainer");
        _turnIndicator = root.Q<TemplateContainer>("TurnIndicator");
        _overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        _overlayContainer.Q<Button>("EndTurnButton").clicked += HandleEndTurnUI;
        visibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        visibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        showGameOverlayEC.OnEventRaised += SetGameOverlayVisibility;
        showTurnIndicatorEC.OnEventRaised += SetTurnIndicatorVisibility;
        playerDeselectedEC.OnEventRaised += HandlePlayerDeselected;

        InitializeAbilityList();
        playerSelectedEC.OnEventRaised += HandlePlayerSelected;
    }

    private void HandleEndTurnUI() {
        endTurnEC.RaiseEvent(Faction.Player);
    }

    void SetTurnIndicatorVisibility(bool show) {
        _turnIndicator.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
    }

    void SetGameOverlayVisibility(bool value) {
        if (value) {
            enableGamplayInput.RaiseEvent();
            _overlayContainer.style.display = DisplayStyle.Flex;
        }
        else {
            _overlayContainer.style.display = DisplayStyle.None;
        }
    }

    void HandleOtherScreensOpened(bool value) {
        SetGameOverlayVisibility(false);
    }

    void InitializeAbilityList() {
        _abilityList = new List<VisualElement>();
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action1"));
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action2"));
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action3"));
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action4"));
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action5"));
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action6"));
        _abilityList.Add(_actionContainer.Q<VisualElement>("Action7"));

        _abilityIconSlotList = new List<AbilitySlot>();
        int counter = 0;

        foreach (var ability in _abilityList) {
            _abilityIconSlotList.Add(new AbilitySlot());
            ability.Add(_abilityIconSlotList[counter++]);
        }
    }

    void FlushAbilityListIcons() {
        foreach (var abilitySlot in _abilityIconSlotList) {
            if (abilitySlot != null && abilitySlot.abilityID != -1) {
                abilitySlot.DropAbility();
            }
        }
    }

    void CallBackMouseDownAbility(MouseDownEvent evt, int abilityID) {
        Debug.Log(evt.target);
        _callBackAction(abilityID);
    }

    void CallBackMouseEnterAbility(MouseEnterEvent evt, string description) {
        Label text = _overlayContainer.Q<Label>("AbilityDescription");
        text.style.display = DisplayStyle.Flex;
        text.text = description;
    }

    void CallBackMouseLeaveAbility(MouseLeaveEvent evt) {
        Label text = _overlayContainer.Q<Label>("AbilityDescription");
        text.style.display = DisplayStyle.None;
    }

    void HandlePlayerSelected(GameObject obj, Action<int> callBackAction) {
        // Anzeigen der notwendigen Komponenten
        ShowActionMenu();
        ShowPlayerViewContainer();

        _callBackAction = callBackAction;
        FlushAbilityListIcons();
        List<AbilitySO> abilities = new List<AbilitySO>(obj.GetComponent<PlayerCharacterSC>().Abilities);
        int counter = 0;

        foreach (var ability in abilities) {
            Debug.Log(ability.abilityID);
            _abilityIconSlotList[counter].HoldAbility(ability);
            _abilityIconSlotList[counter]
                .RegisterCallback<MouseDownEvent, int>(CallBackMouseDownAbility,
                    _abilityIconSlotList[counter].abilityID, TrickleDown.NoTrickleDown);
            _abilityIconSlotList[counter]
                .RegisterCallback<MouseEnterEvent, string>(CallBackMouseEnterAbility, ability.description);
            _abilityIconSlotList[counter].RegisterCallback<MouseLeaveEvent>(CallBackMouseLeaveAbility);
            counter++;
        }

        RefreshStats(obj);
        //TODO: Hier die Stats einbauen, für den ausgewählten Spieler
    }

    void RefreshStats(GameObject obj) {
        //VisualElement manaBar = PlayerViewContainer.Q<VisualElement>("ProgressBarManaOverlay");
        VisualElement healthBar = _playerViewContainer.Q<VisualElement>("ProgressBarHealthOverlay");
        VisualElement abilityBar = _playerViewContainer.Q<VisualElement>("ProgressBarAbilityOverlay");

        PlayerCharacterSC playerSC = obj.GetComponent<PlayerCharacterSC>();
        CharacterStats playerStats = obj.GetComponent<PlayerCharacterSC>().CurrentStats;

        healthBar.style.width =
            new StyleLength(Length.Percent((100 * (float) playerSC.HealthPoints / playerStats.maxHealthPoints)));
        abilityBar.style.width =
            new StyleLength(Length.Percent((100 * (float) playerSC.EnergyPoints / playerStats.maxEnergy)));
        //manaBar.style.width = new StyleLength(Length.Percent((100* (float)playerSC.EnergyPoints/playerStats.maxEnergy)));

        // Labels für Stats
        _playerViewContainer.Q<Label>("StrengthLabel").text = playerStats.strength.ToString();
        _playerViewContainer.Q<Label>("DexterityLabel").text = playerStats.dexterity.ToString();
        _playerViewContainer.Q<Label>("IntelligenceLabel").text = playerStats.intelligence.ToString();
        _playerViewContainer.Q<Label>("MovementLabel").text = playerStats.viewDistance.ToString();

        // Image
        VisualElement image = _playerViewContainer.Q<VisualElement>("PlayerPicture");
        image.Clear();
        Image newProfile = new Image();
        image.Add(newProfile);
        newProfile.image = playerSC.playerType.profilePicture.texture;

        // Name and Level
        _playerViewContainer.Q<Label>("PlayerName").text = playerSC.playerName;
        _playerViewContainer.Q<Label>("LevelLabel").text = playerSC.level.ToString();
    }

    void ShowMenu() {
        visibilityMenuEventChannel.RaiseEvent(true);
    }

    void HandlePlayerDeselected(GameObject obj) {
        _actionContainer.style.display = DisplayStyle.None;
        _playerViewContainer.style.display = DisplayStyle.None;
    }

    void ShowPlayerViewContainer() {
        _playerViewContainer.style.display = DisplayStyle.Flex;
    }

    void HidePlayerViewContainer() {
        _playerViewContainer.style.display = DisplayStyle.None;
    }

    void ShowActionMenu() {
        _actionContainer.style.display = DisplayStyle.Flex;
    }
}
