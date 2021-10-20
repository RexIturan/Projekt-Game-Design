using System.Collections.Generic;
using Grid;
using UnityEngine;
using Visual;
using WorldObjects.ScriptableObjects;

namespace WorldObjects {
    public class WorldObjectGridController : MonoBehaviour {

        [SerializeField] private WorldObjectGridContainerSO worldObjectGridContainer;
        [SerializeField] private GridDataSO globalGridData;

        // drawer
        [SerializeField] private PrefabGridDrawer drawer;
        // World Object container
        [SerializeField] private WorldObjectContainerSO worldObjectContainer;

        private void Awake() {
            worldObjectGridContainer.worldObjectGrids = new List<WorldObjectGrid>();
            worldObjectGridContainer.worldObjectGrids.Add(CreateNewWorldObjectGrid());
            FillWorldObjectGrid(worldObjectGridContainer.worldObjectGrids[0], worldObjectContainer.worldObjects[1]);
            // IncreaseGrid(new Vector2Int(-3,-3), new Vector2Int(globalGridData.Width + 3, globalGridData.Height +3));
            // drawer.DrawGrid();
        }

        public WorldObjectGrid CreateNewWorldObjectGrid() {
            return new WorldObjectGrid(
                globalGridData.Width,
                globalGridData.Height,
                globalGridData.CellSize,
                globalGridData.OriginPosition
            );
        }
        
        // TODO move to grid data 
        // bounds are inclusive
        public bool IsInBounds(int x, int y, Vector2Int lowerBounds, Vector2Int upperBounds) {
            return x >= lowerBounds.x &&
                   y >= lowerBounds.y &&
                   x <= upperBounds.x &&
                   y <= upperBounds.y;
        }

        public bool IsInBounds(Vector2Int pos, Vector2Int lowerBounds, Vector2Int upperBounds) {
            return IsInBounds(pos.x, pos.y, lowerBounds, upperBounds);
        }

        // pos in -/- direction
        // returns inclusive bound
        public Vector2Int GetLowerBounds() {
            return WorldPosToGridPos(globalGridData.OriginPosition);
        }

        // pos in -/- direction
        // returns inclusive bound
        public Vector2Int GetUpperBounds(Vector2Int pos, int width, int height) {
            return new Vector2Int(width - 1 + pos.x, height - 1 + pos.y);
        }

        // y == layer
        public Vector2Int WorldPosToGridPos(Vector3 worldPos) {
            var flooredPos = Vector3Int.FloorToInt(worldPos);
            return new Vector2Int(flooredPos.x, flooredPos.z);
        }

        private Vector2Int TilePosToGridPos(Vector2Int pos, Vector2Int lowerBounds) {
            // shift pos into grid space
            return new Vector2Int(
                x: pos.x + Mathf.Abs(lowerBounds.x),
                y: pos.y + Mathf.Abs(lowerBounds.y));
        }
        
        public void AddTileAt(Vector3 pos, WorldObjectTypeSO worldObjectType) {
            AddTileAt(WorldPosToGridPos(pos), 0, worldObjectType);
        }

        public void AddTileAt(Vector2Int pos, int level, WorldObjectTypeSO worldObjectType) {
            
            var lowerBounds = GetLowerBounds();
            var upperBounds = GetUpperBounds(
                WorldPosToGridPos(globalGridData.OriginPosition),
                globalGridData.Width,
                globalGridData.Height);

            Vector2Int newLowerBounds = lowerBounds;
            Vector2Int newUpperBounds = upperBounds;

            if (!IsInBounds(pos.x, pos.y, lowerBounds, upperBounds)) {
                // Debug.Log("Out of Bounds");

                newLowerBounds = new Vector2Int(
                    Mathf.Min(pos.x, lowerBounds.x),
                    Mathf.Min(pos.y, lowerBounds.y)
                );

                newUpperBounds = new Vector2Int(
                    Mathf.Max(pos.x, upperBounds.x),
                    Mathf.Max(pos.y, upperBounds.y)
                );

                OffsetGlobalGridData(lowerBounds, newLowerBounds, newUpperBounds);
                IncreaseWorldObjectGrid(lowerBounds, newLowerBounds, newUpperBounds);

                // TODO newPos?

                // Debug.Log($"pos:{pos}| lower{lowerBounds} upper{upperBounds}| newLower{newLowerBounds} newUpper{newUpperBounds}");
            }
            else {
                // Debug.Log("In Bounds");
                // Debug.Log($"pos:{pos}| lower{lowerBounds} upper{upperBounds}|");
            }

            var newPos = TilePosToGridPos(pos, newLowerBounds);

            // Debug.Log($"tilePosOffsetted {x} {y}");

            worldObjectGridContainer.worldObjectGrids[level].GetGridObject(newPos.x, newPos.y).SetWorldObjectType(worldObjectType);
            
            drawer.DrawGrid();
        }
        
        public void FillWorldObjectGrid(WorldObjectGrid worldObjectGrid, WorldObjectTypeSO worldObjectType) {
            for (int x = 0; x < worldObjectGrid.Width; x++) {
                for (int y = 0; y < worldObjectGrid.Height; y++) {
                    worldObjectGrid.GetGridObject(x, y).SetWorldObjectType(worldObjectType);
                }
            }
        }
        
        // TODO wording
        // TODO maybe => from -> negChange && to -> positiveChange 
        // from = -OO -> origin | to = origin -> +OO
        public void IncreaseWorldObjectGrid(Vector2Int lowerBounds, Vector2Int newLowerBounds, Vector2Int newUpperBounds) {
            var oldWorldObjectGrids = worldObjectGridContainer.worldObjectGrids;

            var offset = TilePosToGridPos(newLowerBounds, lowerBounds) * -1;
            for (int i = 0; i < worldObjectGridContainer.worldObjectGrids.Count; i++) {
                WorldObjectGrid newWorldObjectGrid = CreateNewWorldObjectGrid();
                //TODO default fill
                // FillWorldObjectGrid(newWorldObjectGrid, worldObjectContainer.worldObjects[0]);
                oldWorldObjectGrids[i].CopyTo(newWorldObjectGrid, offset);
                worldObjectGridContainer.worldObjectGrids[i] = newWorldObjectGrid;
            }
        }

        public void OffsetGlobalGridData(Vector2Int lowerBounds, Vector2Int newLowerBounds, Vector2Int newUpperBounds) {
            ChangeBounds(newLowerBounds, newUpperBounds);
        }
        
        private void ChangeBounds(Vector2Int newLowerBounds, Vector2Int newUpperBounds) {
            globalGridData.Width = newUpperBounds.x + Mathf.Abs(newLowerBounds.x) + 1;
            globalGridData.Height = newUpperBounds.y + Mathf.Abs(newLowerBounds.y) + 1;
            globalGridData.OriginPosition = new Vector3(
                x: newLowerBounds.x,
                y: globalGridData.OriginPosition.y,
                z: newLowerBounds.y);
        }
    }
}