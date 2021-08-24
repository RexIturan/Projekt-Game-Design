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

    public event Action<GameObject> onPlayerClicked;
    public event Action onGridClicked;

    public void PlayerClicked(GameObject obj)
    {
        if (onPlayerClicked != null)
        {
            onPlayerClicked(obj);
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
