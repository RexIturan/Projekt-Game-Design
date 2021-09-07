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
    
    
    // Start is called before the first frame update
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startButton = root.Q<Button>("startButton");
        loadLevelButton = root.Q<Button>("loadLevelButton");
        settingsButton = root.Q<Button>("settingsButton");
        exitButton = root.Q<Button>("exitButton");

        startButton.clicked += StartButtonPressed;
        exitButton.clicked += QuitGame;
    }

    void StartButtonPressed()
    {
        SceneManager.LoadScene("GameDesign");
    }
    
    void QuitGame()
    {
        Application.Quit();
    }

}
