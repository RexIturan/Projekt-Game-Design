using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    // Singleton nutzen, damit das EventSystem einmalig ist
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action onPlayerClicked;
    public event Action onGridClicked;

    public void PlayerClicked()
    {
        if (onPlayerClicked != null)
        {
            onPlayerClicked();
        }
        
    }
    
    public void GridClicked()
    {
        if (onGridClicked != null)
        {
            onGridClicked();
        }
        
    }
    
}
