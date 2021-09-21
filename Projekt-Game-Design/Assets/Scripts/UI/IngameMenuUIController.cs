using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class IngameMenuUIController : MonoBehaviour
{
    // Für das Ingame Menü
    private VisualElement ingameMenuContainer;
    
    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    
    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO enableMenuInput;
    
    [Header("Sending and Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityGameOverlayEventChannel;

    // Zum bestimmen welcher Content im IngameMenu dargestellt werden soll
    private enum menuScreenContent
    {
        NONE,
        LOAD_SCREEN,
        SAVE_SCREEN,
        SETTINGS_SCREEN
    }

    private void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        ingameMenuContainer = root.Q<VisualElement>("IngameMenu");
        ingameMenuContainer.Q<Button>("SaveButton").clicked += ShowSaveScreen;
        ingameMenuContainer.Q<Button>("OptionsButton").clicked += ShowOptionsScreen;
        ingameMenuContainer.Q<Button>("LoadButton").clicked += ShowLoadScreen;
        
        ingameMenuContainer.Q<Button>("ResumeButton").clicked += HideMenu;
        ingameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;
        
        ingameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
    }

    void HideMenu()
    {
        VisibilityGameOverlayEventChannel.RaiseEvent(true);
    }
    
    private void Awake()
    {
        VisibilityMenuEventChannel.OnEventRaised += HandleGameOverlay;
        VisibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityGameOverlayEventChannel.OnEventRaised  += HandleOtherScreensOpened;
    }
    
    void HandleGameOverlay(bool value)
    {
        if (value)
        {
            enableMenuInput.RaiseEvent();
            ingameMenuContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            ingameMenuContainer.style.display = DisplayStyle.None;
            //HideMenu();
        }
        
    }

    void HandleOtherScreensOpened(bool value)
    {
        HandleGameOverlay(false);
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
    
    void MainMenuButtonPressed()
    {
        // Szene laden
        SceneManager.LoadScene("MainMenu");
    }
    
    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
    }
}
