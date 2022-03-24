using System.Collections.Generic;
using Level.Grid;
using Level.Grid.CharacterGrid;
using Level.Grid.ItemGrid;
using Level.Grid.ObjectGrid;
using UnityEngine;
using Util;

namespace Grid {
    [CreateAssetMenu(fileName = "newGridContainer", menuName = "Grid/GridContainer", order = 0)]
    public class GridContainerSO : ScriptableObject {
        // public GridDataSO globalGridData;
        //todo use array?
        public List<TileGrid> tileGrids = new List<TileGrid>();

        // public ItemGrid[] items;
        // public CharacterGrid[] characters;
        // public ObjectGrid[] objects;

        // public void InitGridContainer() {
	       //  items = new ItemGrid[1];
	       //  characters = new CharacterGrid[1];
	       //  objects = new ObjectGrid[1];
        // }

        public void InitGrids(GridDataSO gridData) {
	        var layerNum = gridData.Height;
	        
	        tileGrids = new List<TileGrid>();
	        // items = new ItemGrid[layerNum];
	        // characters = new CharacterGrid[layerNum];
	        // objects = new ObjectGrid[layerNum];
	        
	        for ( int i = 0; i < layerNum; i++ ) {
		        tileGrids.Add(CreateNewTileGrid(gridData));
		        // items[i] = CreateNewItemGrid(gridData);
		        // characters[i] = CreateNewCharacterGrid(gridData);
		        // objects[i] = CreateNewObjectGrid(gridData);
	        }
        }

        #region Copy

        public void CopyAllGrids(Vector2Int originOffset, GridDataSO gridData) {
	        CopyTileGrid(originOffset, gridData);
	        // CopyCharacterGrid(originOffset, gridData);
	        // CopyItemGrid(originOffset, gridData);
	        // CopyObjectGrid(originOffset, gridData);
        }
        
        private void CopyTileGrid(Vector2Int originOffset, GridDataSO gridData) {
	        var oldTileGrids = tileGrids;
	        for ( int i = 0; i < tileGrids.Count; i++ ) {
		        TileGrid newTileGrid = CreateNewTileGrid(gridData);
				
		        // if ( i == 0 ) {
			       //  FillTileGrid(newTileGrid, tileTypesContainer.tileTypes[1].id);
		        // }
		        // else {
			       //  FillTileGrid(newTileGrid, tileTypesContainer.tileTypes[0].id);
		        // }

		        oldTileGrids[i].CopyTo(newTileGrid, originOffset * -1);
		        tileGrids[i] = newTileGrid;
	        }
        }
		
        // private void CopyCharacterGrid(Vector2Int originOffset, GridDataSO gridData) {
	       //  var oldCharacterGrids = characters;
			     //
	       //  for ( int i = 0; i < characters.Length; i++ ) {
		      //   var newCharacterGrid = CreateNewCharacterGrid(gridData);
        //
		      //   oldCharacterGrids[i].CopyTo(newCharacterGrid, originOffset * -1);
		      //   characters[i] = newCharacterGrid;
	       //  }
        // }
		
        // private void CopyItemGrid(Vector2Int originOffset, GridDataSO gridData) {
	       //  var oldItemGrids = items;
			     //
	       //  for ( int i = 0; i < items.Length; i++ ) {
		      //   var newItemGrid = CreateNewItemGrid(gridData);
        //
		      //   oldItemGrids[i].CopyTo(newItemGrid, originOffset * -1);
		      //   items[i] = newItemGrid;
	       //  }
        // }
		
        // private void CopyObjectGrid(Vector2Int originOffset, GridDataSO gridData) {
	       //  var oldObjectGrids = objects;
			     //
	       //  for ( int i = 0; i < items.Length; i++ ) {
		      //   var newObjectGrid = CreateNewObjectGrid(gridData);
        //
		      //   oldObjectGrids[i].CopyTo(newObjectGrid, originOffset * -1);
		      //   objects[i] = newObjectGrid;
	       //  }
        // }

        #endregion

        #region Create New

        private TileGrid CreateNewTileGrid(GridDataSO gridData) {
	        return new TileGrid(
		        gridData.Width,
		        gridData.Depth,
		        gridData.CellSize,
		        gridData.OriginPosition
	        );
        }

        private CharacterGrid CreateNewCharacterGrid(GridDataSO gridData) {
	        return new CharacterGrid(
		        gridData.Width,
		        gridData.Depth,
		        gridData.CellSize,
		        gridData.OriginPosition
	        );
        }

        private ItemGrid CreateNewItemGrid(GridDataSO gridData) {
	        return new ItemGrid(
		        gridData.Width,
		        gridData.Depth,
		        gridData.CellSize,
		        gridData.OriginPosition
	        );
        }
		
        private ObjectGrid CreateNewObjectGrid(GridDataSO gridData) {
	        return new ObjectGrid(
		        gridData.Width,
		        gridData.Depth,
		        gridData.CellSize,
		        gridData.OriginPosition
	        );
        }
        
        #endregion
    }
}