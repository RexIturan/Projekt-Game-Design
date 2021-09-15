using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class OverlayUIController : MonoBehaviour
{
    
    private VisualElement overlayContainer;
    private VisualElement ingameMenuContainer;
    
    // Start is called before the first frame update
    void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        ingameMenuContainer = root.Q<VisualElement>("IngameMenu");

        ingameMenuContainer.Q<Button>("MainMenuButton").clicked += MainMenuButtonPressed;
        overlayContainer.Q<Button>("IngameMenuButton").clicked += IngameMenuButtonPressed;
        ingameMenuContainer.Q<Button>("ResumeButton").clicked += ResumeButtonPressed;
        ingameMenuContainer.Q<Button>("QuitButton").clicked += QuitGame;

    }
    
    
    void MainMenuButtonPressed()
    {
        // Szene laden
        SceneManager.LoadScene("MainMenu");
    }
    
    void IngameMenuButtonPressed()
    {
        // Einstellungen ausblenden und Menü zeigen
        ingameMenuContainer.style.display = DisplayStyle.Flex;
        overlayContainer.style.display = DisplayStyle.None;
    }
    
    void ResumeButtonPressed()
    {
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
