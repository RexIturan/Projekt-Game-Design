using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Mode {
    play,
    edit,
    test,
}

public class GridDrawer : MonoBehaviour {
    
    private Transform gridTransform;
    public Mode mode;
    
    public bool drawTestDebugGrid;
    public bool drawCost;
    public Vector2Int gridSize = new Vector2Int(1, 1);
    // public Color xColor = Color.red;
    // public Color yColor = Color.cyan;

    public bool showNumbers = true;
    public GameObject textTile;
    public GameObject textTileParent;
    private GameObject[,] textTiles;
    
    // data layer
    public int[,] grid;
    
    // grid graph, used by A*
    // private int[,] gridGraph;

    // private int[,] testMap = {
    //     {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    //     {1, 3, 2, 1, 1, 1, 1, 1, 1, 1},
    //     {1, 3, 1, 1, 1, 1, 1, 1, 1, 1},
    //     {1, 1, 2, 1, 1, 5, 1, 1, 1, 1},
    //     {1, 1, 1, 1, 1, 5, 1, 1, 1, 1},
    //     {1, 1, 1, 1, 1, 5, 1, 1, 1, 1},
    //     {1, 3, 2, 1, 1, 5, 1, 1, 1, 1},
    //     {1, 3, 1, 5, 5, 5, 1, 1, 1, 1},
    //     {1, 1, 2, 1, 1, 1, 1, 1, 1, 1},
    //     {1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
    // };

    private Pathfinding pathfinder;

    public Tilemap tilemap;

    public Vector3Int[] pos;
    public TileBase[] tiles;
    public TileBase grayTile;
    public TileBase blueTile;

    public int initCost = 1;
    public int speed = 2;
    
    private void Awake() {
        gridTransform = gameObject.transform;
        textTiles = new GameObject[gridSize.x, gridSize.y];
        grid = new int[gridSize.x, gridSize.y];
        
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                grid[x, y] = initCost;
                var obj = Instantiate(textTile, new Vector3(x, 0, y), Quaternion.Euler(90, 0, 0));
                obj.transform.SetParent(textTileParent.transform);
                textTiles[x, y] = obj;
                var textComponent = textTiles[x, y].GetComponent<TextMeshPro>();
                textComponent.SetText(grid[x, y].ToString());
                tilemap.SetTile(new Vector3Int(x, y, 0), grayTile);
            }
        }
    }
    
    private void Update() {
        // if (drawTestDebugGrid) {
        //     drawGrid();    
        // }
        //
        // if (drawCost) {
        //     textTileParent.SetActive(true);
        // }
        // else {
        //     textTileParent.SetActive(false);
        // }

        // switch (mode) {
        //     case Mode.test:
        //         if (Input.GetMouseButtonDown(0)) {
        //             for (int x = 0; x < gridSize.x; x++) {
        //                 for (int y = 0; y < gridSize.y; y++) {
        //                     tilemap.SetTile(new Vector3Int(x, y, 0), grayTile);
        //                 }
        //             }
        //             
        //             Plane plane = new Plane(Vector3.up, 0);
        //
        //             Vector3 worldPosition = new Vector3();
        //             float distance;
        //             RaycastHit hit;
        //             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //             // if (Physics.Raycast(ray.origin, ray.direction * 100, out hit)) {
        //             //     Debug.Log(hit.transform.position);
        //             // }   
        //             if (plane.Raycast(ray, out distance))
        //             {
        //                 worldPosition = ray.GetPoint(distance);
        //             }
        //             
        //             Debug.Log(Mathf.Floor(worldPosition.x) + " " + Mathf.Floor(worldPosition.z));
        //             
        //             pathfinder = new Pathfinding();
        //             var g = pathfinder.initPathfindingMap(grid);
        //             // foreach (var n in g) {
        //             //     Debug.Log("cost: " + n.cost);
        //             //     foreach (var id in n.neighbours) {
        //             //         Debug.Log("coord: " + pathfinder.indexToCoord(id) + " id: " + id);
        //             //         Debug.Log("id cost: " + g[id].cost);
        //             //     }
        //             // }
        //             var result = pathfinder.getPossiblePaths((int) Mathf.Floor(worldPosition.x), (int) Mathf.Floor(worldPosition.z), speed);
        //         
        //             // Debug.Log("possible moves: " + result.Count);
        //             foreach (var node in result) {
        //                 // Debug.Log(String.Format(
        //                 //     "id {0}, pos {1}, cost {2}, parent {3}",
        //                 //     node.id,
        //                 //     pathfinder.indexToCoord(node.id), 
        //                 //     node.cost, 
        //                 //     node.parent));
        //                 var pos = pathfinder.indexToCoord(node.id);
        //                 // textTiles[pos.x, pos.y].GetComponent<TextMeshPro>().SetText(node.cost.ToString());
        //                 tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), blueTile);
        //             }
        //         } 
        //         break;
        //     case Mode.edit:
        //         if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) {
        //             Plane plane = new Plane(Vector3.up, 0);
        //             Vector3 worldPosition = new Vector3();
        //             float distance;
        //             Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //             if (plane.Raycast(ray, out distance))
        //             {
        //                 worldPosition = ray.GetPoint(distance);
        //             }
        //
        //             Debug.Log(Mathf.Floor(worldPosition.x) + " " + Mathf.Floor(worldPosition.z));
        //             
        //             int x = (int) Mathf.Floor(worldPosition.x);
        //             int y = (int) Mathf.Floor(worldPosition.z);
        //
        //             if (x >= 0 && x < gridSize.x && y >= 0 && y < gridSize.y) {
        //                 if (Input.GetMouseButtonDown(0)) {
        //                     grid[x, y]++;    
        //                 }
        //                 if (Input.GetMouseButtonDown(1) && grid[x, y] > 0) {
        //                     grid[x, y]--;
        //                 }
        //
        //                 // todo -> update text grid
        //                 for (int i = 0; i < gridSize.x; i++) {
        //                     for (int j = 0; j < gridSize.y; j++) {
        //                         textTiles[i, j].GetComponent<TextMeshPro>().SetText(grid[i, j].ToString());
        //                     }
        //                 }
        //             }
        //         }
        //
        //
        //         break;
        //     case Mode.play:
        //         
        //         // reset movement tiles
        //         for (int x = 0; x < gridSize.x; x++) {
        //             for (int y = 0; y < gridSize.y; y++) {
        //                 tilemap.SetTile(new Vector3Int(x, y, 0), grayTile);
        //             }
        //         }
        //
        //         break;
        // }
        
    }

    public void resetMovementTiles() {
        for (int x = 0; x < gridSize.x; x++) {
            for (int y = 0; y < gridSize.y; y++) {
                tilemap.SetTile(new Vector3Int(x, y, 0), grayTile);
            }
        }
    }
    
    public void showPathsFrom(int x, int y, int speed) {
        resetMovementTiles();
        
        pathfinder = new Pathfinding();
        var g = pathfinder.initPathfindingMap(grid);
       
        var result = pathfinder.getPossiblePaths(x, y, speed);
                
        foreach (var node in result) {
            var pos = pathfinder.indexToCoord(node.id);
            tilemap.SetTile(new Vector3Int(pos.x, pos.y, 0), blueTile);
        }
    }
    
    private void drawGrid() {
        // for (int z = 0; z <= gridSize.y; z++) {
        //     Debug.DrawLine(
        //         transform.position
        //         - new Vector3(gridSize.x/2, 0, gridSize.y/2)
        //         + new Vector3(0, 0, z),
        //         transform.position
        //         - new Vector3(gridSize.x/2, 0, gridSize.y/2)
        //         + new Vector3(gridSize.x, 0, z), 
        //         xColor);
        // }
        // for (int x = 0; x <= gridSize.x; x++) {
        //     Debug.DrawLine(
        //         transform.position
        //         - new Vector3(gridSize.x/2, 0, gridSize.y/2)
        //         + new Vector3(x, 0, 0),
        //         transform.position
        //         - new Vector3(gridSize.x/2, 0, gridSize.y/2)
        //         + new Vector3(x, 0, gridSize.y),
        //         yColor);
        // }
    }
}
