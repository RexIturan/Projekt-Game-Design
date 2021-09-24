using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;



public class MainMenuUIController : MonoBehaviour
{
    private Button startButton;
    private Button loadLevelButton;
    private Button settingsButton;
    private Button exitButton;
    private Button backButton;
    private VisualElement menuContainer;
    private VisualElement settingsContainer;
    private VisualElement loadGame;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // Holen der Buttons
        startButton = root.Q<Button>("startButton");
        loadLevelButton = root.Q<Button>("loadLevelButton");
        settingsButton = root.Q<Button>("settingsButton");
        exitButton = root.Q<Button>("exitButton");
        backButton = root.Q<Button>("backButton");
        
        // Holen der Menü Container
        menuContainer = root.Q<VisualElement>("menuContainer");
        settingsContainer = root.Q<VisualElement>("SettingsContainer");
        loadGame = root.Q<VisualElement>("LoadScreen");

        startButton.clicked += StartButtonPressed;
        exitButton.clicked += QuitGame;
        backButton.clicked += BackButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
        loadLevelButton.clicked += LoadLevelButtonPressed;
        loadGame.Q<Button>("BackButton").clicked += BackButtonLoadGamePressed;
    }
    
    void SettingsButtonPressed()
    {
        // Menü ausblenden und Einstellungen zeigen
        menuContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.Flex;
    }
    
    void LoadLevelButtonPressed()
    {
        // Menü ausblenden und Einstellungen zeigen
        menuContainer.style.display = DisplayStyle.None;
        loadGame.style.display = DisplayStyle.Flex;
    }
    
    void BackButtonPressed()
    {
        // Einstellungen ausblenden und Menü zeigen
        menuContainer.style.display = DisplayStyle.Flex;
        settingsContainer.style.display = DisplayStyle.None;
    }
    
    void BackButtonLoadGamePressed()
    {
        // Einstellungen ausblenden und Menü zeigen
        menuContainer.style.display = DisplayStyle.Flex;
        loadGame.style.display = DisplayStyle.None;
    }

    void StartButtonPressed()
    {
        // Szene laden
        SceneManager.LoadScene("GameDesign");
    }
    
    void QuitGame()
    {
        // Spiel beenden
        Application.Quit();
    }

}
