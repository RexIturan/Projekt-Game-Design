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

    // Für das Ingame Menü
    private VisualElement ingameMenuContainer;

    // Für das Inventar
    private VisualElement inventoryContainer;

    [Header("Receiving Events On")] [SerializeField]
    private BoolEventChannelSO VisibilityMenuEventChannel;

    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;

    [Header("Sending Events On")] [SerializeField]
    private VoidEventChannelSO enableMenuInput;

    [SerializeField] private VoidEventChannelSO enableGamplayInput;
    [SerializeField] private VoidEventChannelSO enableInventoryInput;


    // Zum bestimmen welcher Content im IngameMenu dargestellt werden soll
    private enum menuScreenContent
    {
        NONE,
        LOAD_SCREEN,
        SAVE_SCREEN,
        SETTINGS_SCREEN
    }

    //Zum bestimmen welches Overlay gerade aktiv sein soll
    private enum screenOverlay
    {
        NONE,
        GAME_UI_OVERLAY,
        INGAME_MENU,
        INVENTORY
    }

    //Für das Inventar
    private enum inventoryTab
    {
        NONE,
        ITEMS,
        ARMORY,
        WEAPONS
    }

    // Start is called before the first frame update
    void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;

        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        ingameMenuContainer = root.Q<VisualElement>("IngameMenu");
        inventoryContainer = root.Q<VisualElement>("InventoryOverlay");

        ingameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
        overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        ingameMenuContainer.Q<Button>("ResumeButton").clicked += HideMenu;
        ingameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;
        ingameMenuContainer.Q<Button>("SaveButton").clicked += ShowSaveScreen;
        ingameMenuContainer.Q<Button>("OptionsButton").clicked += ShowOptionsScreen;
        ingameMenuContainer.Q<Button>("LoadButton").clicked += ShowLoadScreen;
        inventoryContainer.Q<Button>("Tab1").clicked += HandleItemTabPressed;
        inventoryContainer.Q<Button>("Tab2").clicked += HandleArmoryTabPressed;
        inventoryContainer.Q<Button>("Tab3").clicked += HandleWeaponsTabPressed;
    }

    private void Awake()
    {
        VisibilityMenuEventChannel.OnEventRaised += SetMenuVisibility;
        VisibilityInventoryEventChannel.OnEventRaised += SetInventoryVisibility;
    }

    void MainMenuButtonPressed()
    {
        // Szene laden
        SceneManager.LoadScene("MainMenu");
    }

    // TODO Refactor
    void SetMenuVisibility(bool value)
    {
        Debug.Log(value);

        if (value)
        {
            ShowMenu();
        }
        else
        {
            HideMenu();
        }
    }

    void SetInventoryVisibility(bool value)
    {
        Debug.Log(value);

        if (value)
        {
            ShowInventory();
        }
        else
        {
            HideMenu();
        }
    }

    void ShowMenu()
    {
        OverlayManager(screenOverlay.INGAME_MENU);
    }

    void HideMenu()
    {
        OverlayManager(screenOverlay.GAME_UI_OVERLAY);
    }

    void ShowSaveScreen()
    {
        MenuScreenContentManager(menuScreenContent.SAVE_SCREEN);
    }

    void ShowLoadScreen()
    {
        MenuScreenContentManager(menuScreenContent.LOAD_SCREEN);
    }

    void ShowOptionsScreen()
    {
        MenuScreenContentManager(menuScreenContent.SETTINGS_SCREEN);
    }

    void ShowInventory()
    {
        OverlayManager(screenOverlay.INVENTORY);
    }

    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
    }

    void HandleItemTabPressed()
    {
        InventoryManager(inventoryTab.ITEMS, inventoryContainer.Q<Button>("Tab1"));
    }

    void HandleArmoryTabPressed()
    {
        InventoryManager(inventoryTab.ARMORY, inventoryContainer.Q<Button>("Tab2"));
    }

    void HandleWeaponsTabPressed()
    {
        InventoryManager(inventoryTab.WEAPONS, inventoryContainer.Q<Button>("Tab3"));
    }

    void OverlayManager(screenOverlay screenOverlay)
    {
        switch (screenOverlay)
        {
            case screenOverlay.GAME_UI_OVERLAY:
                enableGamplayInput.RaiseEvent();
                overlayContainer.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                ingameMenuContainer.style.display = DisplayStyle.None;
                inventoryContainer.style.display = DisplayStyle.None;
                break;
            case screenOverlay.INGAME_MENU:
                enableMenuInput.RaiseEvent();
                ingameMenuContainer.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                overlayContainer.style.display = DisplayStyle.None;
                inventoryContainer.style.display = DisplayStyle.None;
                break;
            case screenOverlay.INVENTORY:
                // TODO Channel muss noch erstellt werden
                enableInventoryInput.RaiseEvent();
                inventoryContainer.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                overlayContainer.style.display = DisplayStyle.None;
                ingameMenuContainer.style.display = DisplayStyle.None;
                break;
        }
    }

    void MenuScreenContentManager(menuScreenContent menuScreen)
    {
        // Einzelne Screens getten
        VisualElement saveScreen = ingameMenuContainer.Q<VisualElement>("SaveScreen");
        VisualElement loadScreen = ingameMenuContainer.Q<VisualElement>("LoadScreen");
        VisualElement settingsScreen = ingameMenuContainer.Q<VisualElement>("SettingsContainer");

        switch (menuScreen)
        {
            case menuScreenContent.LOAD_SCREEN:
                loadScreen.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                settingsScreen.style.display = DisplayStyle.None;
                saveScreen.style.display = DisplayStyle.None;
                break;
            case menuScreenContent.SAVE_SCREEN:
                saveScreen.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                settingsScreen.style.display = DisplayStyle.None;
                loadScreen.style.display = DisplayStyle.None;
                break;
            case menuScreenContent.SETTINGS_SCREEN:
                settingsScreen.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                saveScreen.style.display = DisplayStyle.None;
                loadScreen.style.display = DisplayStyle.None;
                break;
        }
    }

    void resetAllTabs()
    {
        inventoryContainer.Q<Button>("Tab1").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab2").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab3").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab4").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab5").RemoveFromClassList("ClickedTab");


        inventoryContainer.Q<Button>("Tab1").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab2").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab3").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab4").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab5").AddToClassList("UnclickedTab");
    }

    void InventoryManager(inventoryTab tab, Button button)
    {
        resetAllTabs();
        button.RemoveFromClassList("UnclickedTab");
        button.AddToClassList("ClickedTab");

        switch (tab)
        {
            case inventoryTab.ITEMS:
                break;
            case inventoryTab.ARMORY:
                break;
            case inventoryTab.WEAPONS:
                break;
        }
    }
}