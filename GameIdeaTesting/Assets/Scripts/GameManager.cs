using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using StateMachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using Util;

namespace DefaultNamespace {
    public class GameManager : MonoBehaviour {
        [SerializeField] private StateMachine.StateMachine stateMachine;
        [SerializeField] private InputReader inputReader;

        // [SerializeField] private InputReader inputReader;
        [Header("Grid")] [SerializeField] private int cellSize;
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private Vector3 origin;

        [Header("Pathfinding")] [SerializeField]
        private bool diagonal;

        [SerializeField] private bool debug;

        [Header("Units")] [SerializeField] private GameObject unitPrefab;
        [SerializeField] private Vector2Int[] spawnPointsTeam1;
        [SerializeField] private Vector2Int[] spawnPointsTeam2;
        [SerializeField] private List<GridUnit> team1;
        [SerializeField] private List<GridUnit> team2;
        [SerializeField] private Color team1Color;
        [SerializeField] private Color team2Color;
        private List<GridUnit> currentTeam;

        [Header("TileMap")] [SerializeField] private Tilemap tilemap;
        [SerializeField] private Tilemap uiMap;
        [SerializeField] private Tilemap cursorMap;
        [SerializeField] private TileBase grayTile;
        [SerializeField] private TileBase blueTile;
        [SerializeField] private TileBase redTile;
        [SerializeField] private TileBase orangeTile;


        private GenericGrid<int> grid;
        private Util.Pathfinding pathfinding;
        private Vector2Int selectedTile;


        private void Awake() {
            // grid = new GenericGrid<int> (gridSize.x, gridSize.y, cellSize, origin, (g, x, y) => 0, true);
            pathfinding = new Util.Pathfinding(gridSize.x, gridSize.y, diagonal, debug);
            DrawGrid();
            InitUnits();
            currentTeam = team1;
            inputReader.leftClickEvent += OnLeftClick;
            inputReader.rightClickEvent += OnRightClick;
            inputReader.endTurnEvent += OnEndTurn;
        }

        private void OnDestroy() {
            inputReader.leftClickEvent -= OnLeftClick;
            inputReader.rightClickEvent -= OnRightClick;
            inputReader.endTurnEvent -= OnEndTurn;
        }

        private void InitUnits() {
            var cellCenter = pathfinding.GetGrid.CellSize / 2;

            foreach (var pos in spawnPointsTeam1) {
                var newPos = new Vector3(pos.x + cellCenter, 0, pos.y + cellCenter);
                var obj = Instantiate(unitPrefab, newPos, Quaternion.identity);
                obj.GetComponentsInChildren<MeshRenderer>()[0].material.color = team1Color;
                var unit = obj.GetComponent<GridUnit>();
                team1.Add(unit);
                var node = pathfinding.GetGrid.GetGridObject(newPos);
                // node.isWalkable = false;
                node.unit = unit;
            }

            foreach (var pos in spawnPointsTeam2) {
                var newPos = new Vector3(pos.x + cellCenter, 0, pos.y + cellCenter);
                var obj = Instantiate(unitPrefab, newPos, Quaternion.identity);
                obj.GetComponentsInChildren<MeshRenderer>()[0].material.color = team2Color;
                var unit = obj.GetComponent<GridUnit>();
                team2.Add(unit);
                var node = pathfinding.GetGrid.GetGridObject(newPos);
                // node.isWalkable = false;
                node.unit = unit;
            }
        }

        
        private void DrawGrid() {
            var grid = pathfinding.GetGrid;
            for (int x = 0; x < grid.Width; x++) {
                for (int y = 0; y < grid.Height; y++) {
                    tilemap.SetTile(new Vector3Int(x, y, 0), grayTile);
                }
            }
        }

        private void Update() {
            if (Mouse.current.rightButton.isPressed) {
                Vector3 pos = Util.MousePosition.GetMouseWorldPosition();
                var grid = pathfinding.GetGrid;
                grid.GetXY(pos, out int x, out int y);

                var unit = grid.GetGridObject(x, y).unit;
                if (unit != null) {
                    showAllAttackRange(x, y, unit.attackRange);
                }
            }
            if (Mouse.current.rightButton.wasReleasedThisFrame) {
                resetUiTilemap();
                
            }
        }

        private void OnLeftClick() {
            Vector3 pos = Util.MousePosition.GetMouseWorldPosition();
            var grid = pathfinding.GetGrid;
            grid.GetXY(pos, out int x, out int y);

            // Debug.Log($"left click on {pos}, on grid pos {x},{y} in bounds:{grid.IsInBounds(x, y)}");

            if (grid.IsInBounds(x, y)) {
                switch (stateMachine.currentState) {
                    case State.IDLE:
                        // select unit
                        var gridUnit = grid.GetGridObject(x, y).unit;
                        if (gridUnit != null) {
                            if (currentTeam.Contains(gridUnit)) {
                                // show stats
                                // show possible moves
                                // switch to move phase
                                if (!gridUnit.hasMoved) {
                                    showAllPosibleMoves(x, y, gridUnit.moveDistance);
                                    stateMachine.currentState = State.MOVE_PHASE;
                                    selectedTile = new Vector2Int(x, y);
                                }
                            }
                            else {
                                //show stats
                            }
                        }

                        break;
                    case State.MOVE_PHASE:
                        // move to x/y
                        var unit = grid.GetGridObject(selectedTile.x, selectedTile.y).unit;
                        if (tryMoveUnitTo(selectedTile.x, selectedTile.y, x, y, unit.moveDistance,
                            out List<PathNode> path)) {
                            resetUiTilemap();
                            StartCoroutine(moveUnit(selectedTile, unit, path));
                        }

                        break;
                    case State.ATTACK_PHASE:
                        var attackingUnit = grid.GetGridObject(selectedTile.x, selectedTile.y).unit;
                        if (attackingUnit != null && !attackingUnit.hasAttacked) {
                            if (tryAttackUnit(x, y, attackingUnit.attackPower)) {
                                resetUiTilemap();
                                attackingUnit.hasAttacked = true;
                                stateMachine.currentState = State.IDLE;
                            }    
                        }
                        // attack x/y
                        break;
                    default:
                        break;
                }
            }
        }

        private void OnRightClick() {

            switch (stateMachine.currentState) {
                case State.IDLE:
                    break;
                case State.MOVE_PHASE:
                    //deselect unit
                    resetUiTilemap();
                    stateMachine.currentState = State.IDLE;
                    break;
                case State.ATTACK_PHASE:
                    stateMachine.currentState = State.IDLE;
                    break;
                default:
                    break;
            }
        }

        private void OnEndTurn() {
            foreach (var unit in currentTeam) {
                unit.hasMoved = false;
                unit.hasAttacked = false;
            }

            // reset movement points
            stateMachine.currentState = State.IDLE;

            if (currentTeam == team1) {
                currentTeam = team2;
            }
            else {
                currentTeam = team1;
            }
        }

        private IEnumerator moveUnit(Vector2Int unitPos, GridUnit unit, List<PathNode> path) {
            var cellCenter = pathfinding.GetGrid.CellSize / 2;
            var grid = pathfinding.GetGrid;
            var node = grid.GetGridObject(unitPos.x, unitPos.y);
            // node.isWalkable = true;
            node.unit = null;
            
            // set unit has moved 
            unit.hasMoved = true;

            //move unit
            foreach (var pathNode in path) {
                unit.transform.position = new Vector3(pathNode.x + cellCenter, 0, pathNode.y + cellCenter);
                yield return new WaitForSeconds(0.1f);
            }

            //set ref in grid
            var finalNode = path[path.Count - 1];
            // finalNode.isWalkable = false;
            finalNode.unit = unit;
            
            // todo move out of here
            stateMachine.currentState = State.ATTACK_PHASE;
            selectedTile = new Vector2Int(path[path.Count - 1].x, path[path.Count - 1].y);
            showAllAttackableUnits(path[path.Count - 1].x, path[path.Count - 1].y, unit.attackRange);
        }

        private bool tryMoveUnitTo(int unitX, int unitY, int x, int y, int moveDistance, out List<PathNode> pathNodes) {
            var path = pathfinding.FindPath(unitX, unitY, x, y);
            if (path != null) {
                if (path.Count <= moveDistance && path[path.Count - 1].unit == null) {
                    pathNodes = path;
                }
                else {
                    pathNodes = null;
                    return false;
                }
            }
            else {
                pathNodes = null;
                return false;
            }

            return true;
        }
        
        private bool tryAttackUnit(int x, int y, int attackPower) {
            var grid = pathfinding.GetGrid;
            var unit = grid.GetGridObject(x, y).unit;
            if (unit != null) {

                unit.setCurrentHealth(-attackPower);
                
                if (unit.currentHealth <= 0) {
                    if (team1.Contains(unit)) {
                        team1.Remove(unit);
                    }
                    else {
                        team2.Remove(unit);
                    }
                    GameObject.Destroy(unit.gameObject);
                    // unit.gameObject.SetActive(false);
                }
                return true;
            }
            return false;
        }
        
        private void showAllAttackRange(int unitX, int unitY, int attackRange) {
            var grid = pathfinding.GetGrid;

            resetUiTilemap();

            for (int x = unitX - attackRange; x <= unitX + attackRange; x++) {
                for (int y = unitY - attackRange; y <= unitY + attackRange; y++) {
                    if (grid.IsInBounds(x, y)) {
                        // Position is in bounds
                        var path = pathfinding.FindPath(unitX, unitY, x, y, true);
                        if (path != null) {
                            // Ther is a path
                            if (path.Count-1 <= attackRange) {
                                // path within moveDistance
                                uiMap.SetTile(new Vector3Int(x, y, 0), redTile);
                            }
                        }
                    }
                }
            }
        }

        private void showAllAttackableUnits(int unitX, int unitY, int attackRange) {
            var grid = pathfinding.GetGrid;

            resetUiTilemap();

            for (int x = unitX - attackRange; x <= unitX + attackRange; x++) {
                for (int y = unitY - attackRange; y <= unitY + attackRange; y++) {
                    if (grid.IsInBounds(x, y)) {
                        // Position is in bounds
                        var path = pathfinding.FindPath(unitX, unitY, x, y, true);
                        if (path != null) {
                            // Ther is a path
                            if (path.Count-1 <= attackRange) {
                                // path within moveDistance
                                var unit = grid.GetGridObject(x, y).unit;
                                if (unit != null) {
                                    if (!currentTeam.Contains(unit)) {
                                        uiMap.SetTile(new Vector3Int(x, y, 0), redTile);
                                    }
                                }

                                // else {
                                //     uiMap.SetTile(new Vector3Int(x, y, 0), blueTile);
                                // }
                            }
                        }
                    }
                }
            }
        }

        private void showAllPosibleMoves(int unitX, int unitY, int moveDistance) {
            var grid = pathfinding.GetGrid;

            resetUiTilemap();

            for (int x = unitX - moveDistance; x <= unitX + moveDistance; x++) {
                for (int y = unitY - moveDistance; y <= unitY + moveDistance; y++) {
                    if (grid.IsInBounds(x, y)) {
                        // Position is in bounds
                        if (grid.GetGridObject(x, y).isWalkable) {
                            // Position is walkable
                            var path = pathfinding.FindPath(unitX, unitY, x, y);
                            if (path != null) {
                                // Ther is a path
                                if (path.Count <= moveDistance) {
                                    // path within moveDistance
                                    var unit = grid.GetGridObject(x, y).unit;
                                    if (unit != null) {
                                        if (!currentTeam.Contains(unit)) {
                                            uiMap.SetTile(new Vector3Int(x, y, 0), redTile);
                                        }
                                    }
                                    else {
                                        uiMap.SetTile(new Vector3Int(x, y, 0), blueTile);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void resetUiTilemap() {
            uiMap.ClearAllTiles();
        }
    }
}