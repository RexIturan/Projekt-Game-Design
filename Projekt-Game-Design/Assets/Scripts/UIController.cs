using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button loadLevelButton;
    public Button settingsButton;
    public Button exitButton;
    public Button backButton;
    public VisualElement menuContainer;
    public VisualElement settingsContainer;
    
    
    // Start is called before the first frame update
    void Start()
    {
        // Holen des UMXL Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        // Holen der Buttons
        startButton = root.Q<Button>("startButton");
        loadLevelButton = root.Q<Button>("loadLevelButton");
        settingsButton = root.Q<Button>("settingsButton");
        exitButton = root.Q<Button>("exitButton");
        backButton = root.Q<Button>("backButton");
        
        // Holen der Menü Container
        menuContainer = root.Q<VisualElement>("menuContainer");
        settingsContainer = root.Q<VisualElement>("settingsContainer");

        startButton.clicked += StartButtonPressed;
        exitButton.clicked += QuitGame;
        backButton.clicked += BackButtonPressed;
        settingsButton.clicked += SettingsButtonPressed;
    }
    
    void SettingsButtonPressed()
    {
        // Menü ausblenden und Einstellungen zeigen
        menuContainer.style.display = DisplayStyle.None;
        settingsContainer.style.display = DisplayStyle.Flex;
    }
    
    void BackButtonPressed()
    {
        // Einstellungen ausblenden und Menü zeigen
        menuContainer.style.display = DisplayStyle.Flex;
        settingsContainer.style.display = DisplayStyle.None;
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
