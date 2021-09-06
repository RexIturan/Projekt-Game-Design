using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using Util;

public class TestGrid : MonoBehaviour {
    [SerializeField] private GridsSO tileGrids;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition; 
    [SerializeField] private bool showDebug;

    public Transform TextDebug;
    
    private void Awake() {
        if (tileGrids.grids.Count >= 1) {
            tileGrids.grids[0].CreateDebugDisplay(); 
        }
    }

    public void CreateGrid() {
        GenericGrid<Tile> grid = new GenericGrid<Tile>(width, height, cellSize, originPosition,
            (GenericGrid<Tile> g, int x, int y) => new Tile(g, x, y), showDebug, TextDebug);

        if (tileGrids.grids.Count >= 1) {
            tileGrids.grids[0] = grid;    
        }
        else {
            tileGrids.grids.Add(grid); 
        }
        
    }
}
