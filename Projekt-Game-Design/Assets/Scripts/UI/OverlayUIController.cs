using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OverlayUIController : MonoBehaviour
{
    
    private VisualElement overlayContainer;
    private VisualElement ingameMenuContainer;

    private bool menuOpen;
    
    [Header("Receiving Events On")]
    [SerializeField] private VoidEventChannelSO ToggleMenuChannel;
    
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
        ToggleMenuChannel.OnEventRaised += ToggleMenu;
    }

    void MainMenuButtonPressed()
    {
        // Szene laden
        SceneManager.LoadScene("MainMenu");
    }

    // TODO Refactor
    void ToggleMenu() {
        if (menuOpen) {
            HideMenu();
        }
        else {
            ShowMenu();
        }
    }
    
    void ShowMenu()
    {
        // Einstellungen ausblenden und Menü zeigen
        menuOpen = true;
        ingameMenuContainer.style.display = DisplayStyle.Flex;
        overlayContainer.style.display = DisplayStyle.None;
    }
    
    void HideMenu()
    {
        // Einstellungen ausblenden und Menü zeigen
        menuOpen = false;
        overlayContainer.style.display = DisplayStyle.Flex;
        ingameMenuContainer.style.display = DisplayStyle.None;
    }
    
    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
    }
}
