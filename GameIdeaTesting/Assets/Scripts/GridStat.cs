using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridStat : MonoBehaviour
{
    public int visited = -1;

    public int x = 0;

    public int y = 0;

    // 0 = Nichts, 1 = Rocks, 2 = Forest, 3 = Water
    public int attribut = 0; 
    
    private void OnMouseDown()
    {
        Debug.Log("Grid wurde angeklickt.");
        GameEvents.current.GridClicked();
    }
}
