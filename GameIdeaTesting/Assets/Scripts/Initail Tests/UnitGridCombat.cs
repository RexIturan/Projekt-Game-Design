using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;



public class UnitGridCombat : MonoBehaviour {
    
    [SerializeField] private Team team;
    [SerializeField] public int speed;
    
    
    // todo remove its scuffed
    public GridDrawer gridContainer;
    
    public enum Team {
        blue,
        red
    }

    public void MoveTo(int x, int y) {
        var pathfinder = new Pathfinding();
        var g = pathfinder.initPathfindingMap(gridContainer.grid);
        var possible = pathfinder.getPossiblePaths(
            (int) transform.position.x, 
            (int) transform.position.z, 
            speed);
        var path = pathfinder.FindPath(
            (int) transform.position.x, 
            (int) transform.position.z, 
            x, 
            y, 
            possible);
        
        if (path.Count == 0) 
            return;

        var lastPos = path[path.Count-1];
        transform.position = new Vector3(lastPos.x, 0, lastPos.y);

    } 
    
}
