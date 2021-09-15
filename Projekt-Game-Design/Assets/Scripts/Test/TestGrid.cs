using System;
using System.Collections;
using System.Collections.Generic;
using Grid;
using UnityEngine;
using Util;

public class TestGrid : MonoBehaviour {
    [SerializeField] private GridContainerSO gridContainer;

    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float cellSize;
    [SerializeField] private Vector3 originPosition; 
    [SerializeField] private bool showDebug;

    public Transform TextDebug;
    
    private void Awake() {
        if (gridContainer.tileGrids.Count >= 1) {
            gridContainer.tileGrids[0].CreateDebugDisplay(); 
        }
    }

    public void CreateGrid() {
        TileGrid grid = new TileGrid(width, height, cellSize, originPosition, showDebug, TextDebug);

        if (gridContainer.tileGrids.Count >= 1) {
            gridContainer.tileGrids[0] = grid;    
        }
        else {
            gridContainer.tileGrids.Add(grid); 
        }
        
    }
}