using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OverlayUIController : MonoBehaviour
{
    
    private VisualElement overlayContainer;
    private VisualElement ingameMenuContainer;

    
    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;
    
    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO enableMenuInput;
    [SerializeField] private VoidEventChannelSO enableGamplayInput;


    private enum screenContent
    {
        NONE,LOAD_SCREEN, SAVE_SCREEN, SETTINGS_SCREEN
    }
    
    // Start is called before the first frame update
    void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        ingameMenuContainer = root.Q<VisualElement>("IngameMenu");

        ingameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
        overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        ingameMenuContainer.Q<Button>("ResumeButton").clicked += HideMenu;
        ingameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;
        ingameMenuContainer.Q<Button>("SaveButton").clicked += ShowSaveScreen;
        ingameMenuContainer.Q<Button>("OptionsButton").clicked += ShowOptionsScreen;
        ingameMenuContainer.Q<Button>("LoadButton").clicked += ShowLoadScreen;

    }

    private void Awake() {
        VisibilityMenuEventChannel.OnEventRaised += SetMenuVisibility;
    }

    void MainMenuButtonPressed()
    {
        // Szene laden
        SceneManager.LoadScene("MainMenu");
    }

    // TODO Refactor
    void SetMenuVisibility(bool value) {
        
        Debug.Log(value);
        
        if (value) {
            ShowMenu();
        }
        else {
            HideMenu();
        }
    }
    
    void ShowMenu()
    {
        enableMenuInput.RaiseEvent();
        // Einstellungen ausblenden und Menü zeigen
        ingameMenuContainer.style.display = DisplayStyle.Flex;
        overlayContainer.style.display = DisplayStyle.None;
    }
    
    void HideMenu()
    {
        enableGamplayInput.RaiseEvent();
        // Einstellungen ausblenden und Menü zeigen
        overlayContainer.style.display = DisplayStyle.Flex;
        ingameMenuContainer.style.display = DisplayStyle.None;
    }
    
    void ShowSaveScreen()
    {
        ScreenContentManager(screenContent.SAVE_SCREEN);
    }
    
    void ShowLoadScreen()
    {
        ScreenContentManager(screenContent.LOAD_SCREEN);
    }
    
    void ShowOptionsScreen()
    {
        ScreenContentManager(screenContent.SETTINGS_SCREEN);
    }

    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
    }

    void ScreenContentManager(screenContent screen)
    {
        // Einzelne Screens getten
        VisualElement saveScreen = ingameMenuContainer.Q<VisualElement>("SaveScreen");
        VisualElement loadScreen = ingameMenuContainer.Q<VisualElement>("LoadScreen");
        VisualElement settingsScreen = ingameMenuContainer.Q<VisualElement>("SettingsContainer");

        switch (screen)
        {
            case screenContent.LOAD_SCREEN:
                loadScreen.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                settingsScreen.style.display = DisplayStyle.None;
                saveScreen.style.display = DisplayStyle.None;
                break;
            case screenContent.SAVE_SCREEN:
                saveScreen.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                settingsScreen.style.display = DisplayStyle.None;
                loadScreen.style.display = DisplayStyle.None;
                break;
            case screenContent.SETTINGS_SCREEN:
                settingsScreen.style.display = DisplayStyle.Flex;
                // Ausblenden aller anderen Screens
                saveScreen.style.display = DisplayStyle.None;
                loadScreen.style.display = DisplayStyle.None;
                break;
        }
    }
}
