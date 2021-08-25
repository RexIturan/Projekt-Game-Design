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

    public event Action<Transform> playerMoved;

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

    public void PlayerMoved(Transform transform)
    {
        if (playerMoved != null)
        {
            playerMoved(transform);
        }
    }
    
}
