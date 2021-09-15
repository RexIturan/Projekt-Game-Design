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
    
    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
    }
}
