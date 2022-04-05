using System.Collections.Generic;
using Grid;
using UnityEngine;
using WorldObjects;

namespace Visual {
    public class PrefabGridDrawer : MonoBehaviour {
	    
        //new
        [SerializeField] private GridDataSO gridData;
        [SerializeField] private TileTypeContainerSO tileTypeContainer;
        [SerializeField] private Transform tileParent;
        
        private GameObject[] _tileObjects;

        private int GetIndexFromVec3(Vector3Int vector3Int) {
	        int x = vector3Int.x;
	        int y = vector3Int.y;
	        int z = vector3Int.z;
	        
	        int width = gridData.Width;
	        int height = gridData.Height;
	        
	        return x + y * width + z * width * height;
        }

        private void ClearAllGameObjects() {
	        if ( _tileObjects is { Length: > 0 } ) {
		        foreach ( var obj in _tileObjects ) {
			        if ( obj is { } ) {
				        Destroy(obj);
			        }
		        }
	        }

	        if ( tileParent is { } ) {
		        var childCount = tileParent.childCount;
		        for ( int i = 0; i < childCount; i++ ) {
			        Destroy(tileParent.GetChild(i).gameObject);
		        }
	        }
        }
        
        public void Init() {
		      ClearAllGameObjects();

	        var len = gridData.Width * gridData.Height * gridData.Depth;
	        _tileObjects = new GameObject[len];
        }

        public void GenerateTiles() {
	        Init();

	        var width = gridData.Width;
	        var height = gridData.Height;
	        var depth = gridData.Depth;


	        for ( int z = 0; z < depth; z++ ) {
		        for ( int y = 0; y < height; y++ ) {
			        for ( int x = 0; x < width; x++ ) {
				        var gridPosition = new Vector3Int(x, y, z);
				        var worldPosition = gridData.GetWorldPosFromGridPos(gridPosition);
				        var tileType = gridData.GetTileTypeAt(gridPosition);
				        GameObject obj = null;
				        
				        if ( tileType is { } ) {
					        var neighbours = gridData.GetNeighbouringTileTypes(gridPosition);
					        obj = CreateNewTile(tileType, worldPosition, neighbours);  
				        }
				        
				        _tileObjects[GetIndexFromVec3(gridPosition)] = obj;
			        }
		        }
	        }
        }

        private GameObject CreateNewTile(TileTypeSO tileType, Vector3 worldPos, List<TileTypeSO> neighbours) {
	        // Instantiate(, worldPos, tileParent);
	        var tileObj = tileType.tileVariance
		        .CreateTile(
			        worldPos + new Vector3(0, gridData.GetCellCenter3D().y, 0), 
			        tileParent, 
			        tileType, 
			        neighbours);
	        
	        return tileObj;
        }
        
        // public void RedrawItems() {
	       //  //todo
	       //  //check if draw array is big enough
	       //  ResizeItemObjects(gridData); 
	       //  
	       //  // var items = gridContainer.items;
        //
	       //  // for each
	       //  for ( int layer = 0; layer < items.Length; layer++ ) {
		      //   var currentItemGrid = items[layer];
		      //   for ( int y = 0; y < currentItemGrid.Depth; y++ ) {
			     //    for ( int x = 0; x < currentItemGrid.Width; x++ ) {
				    //     
				    //     var item = currentItemGrid.GetGridObject(x, y);
				    //     var itemRepresentation = _itemObjects[x, layer, y];
				    //     var worldPos = gridData.GetWorldPosFromGridPos(x, layer, y) + gridData.GetCellCenter();
				    //     var gridPos = new Vector3Int(x, layer, y);
				    //     
				    //     if ( item.Exists() ) {
					   //      // create GameObject at
					   //      if ( itemRepresentation != null ) {
						  //       //check if it is the right object
						  //       ChangeItemGameObject(item.ID, gridPos, _itemObjects);
					   //      }
					   //      else {
						  //       //get prefab from item id
								// 		CreateItemGameObject(item.ID, worldPos, gridPos, _itemObjects);
					   //      }
				    //     }
				    //     else {
					   //      if ( itemRepresentation != null ) {
						  //       RemoveItemGameObject(gridPos, _itemObjects);
					   //      }
				    //     }
				    //     
			     //    }
		      //   }
	       //  }
        // }

        // private void ResizeItemObjects(GridDataSO grid) {
	       //  var width = grid.Width;
	       //  var depth = grid.Depth;
	       //  var height = grid.Height;
        //
	       //  var oldWidth = _itemObjects.GetLength(0);
	       //  var oldHeight = _itemObjects.GetLength(1);
	       //  var oldDepth = _itemObjects.GetLength(2);
        //
	       //  if ( oldWidth != width || oldDepth != depth || oldHeight != height ) {
		      //   RemoveAllGameObjects(_itemObjects);
		      //   _itemObjects = new GameObject[width, height, depth];
	       //  }
        // }

        // private void ChangeItemGameObject(int id, Vector3Int gridPos, GameObject[,,] items) {
	       //  var itemData = itemTypeDictionary.itemList[id];
	       //  var item = items[gridPos.x, gridPos.y, gridPos.z];
	       //  var itemComponent = item.GetComponent<ItemComponent>();
	       //  itemComponent.InitItem(itemData, gridPos);
        // }
        //
        // private void CreateItemGameObject(int id, Vector3 worldPos, Vector3Int gridPos, GameObject[,,] itemComponents) {
	       //  ItemTypeSO itemTypeType = itemTypeDictionary.itemList[id];
	       //  var prefab = itemTypeType.prefab;
	       //  var obj = GameObject.Instantiate(
		      //   original: prefab, 
		      //   position: worldPos,
		      //   rotation: Quaternion.identity);
     //
	    //     obj.transform.SetParent(itemParent.transform);
	    //     itemComponents[gridPos.x, gridPos.y, gridPos.z] = obj;
	    //     ItemComponent itemComponent = itemComponents[gridPos.x, gridPos.y, gridPos.z].GetComponent<ItemComponent>();
					// itemComponent.Type = itemTypeType;
					// itemComponent.InitItem(itemTypeType, gridPos);
					// // itemComponent.Reset();
     //    }

     //    private void RemoveItemGameObject(Vector3Int gridPos, GameObject[,,] items) {
	    //     //todo check if pos fits in items
	    //     var item = items[gridPos.x, gridPos.y, gridPos.z];
	    //     items[gridPos.x, gridPos.y, gridPos.z] = null;
					// GameObject.Destroy(item);
     //    }

        // private void RemoveAllGameObjects(GameObject[,,] objects) {
	       //  foreach ( var obj in objects ) {
		      //   if ( obj != null ) {
			     //    GameObject.Destroy(obj);  
		      //   }
	       //  }
        // }
        //
        // public void DrawGrid() {
        //     
        //     // for each instantiate prefab
        //     // var offset = new Vector2Int((int)globalGridData.OriginPosition.x, (int)globalGridData.OriginPosition.z); 
        //     //
        //     // for (int l = 0; l < worldObjectGridContainer.worldObjectGrids.Count; l++) {
        //     //     var worldObjectGrid = worldObjectGridContainer.worldObjectGrids[l];
        //     //     for (int x = 0; x < worldObjectGrid.Width; x++) {
        //     //         for (int y = 0; y < worldObjectGrid.Depth; y++) {
        //     //             var tile = worldObjectGrid.GetGridObject(x, y).type;
        //     //             
        //     //             if (tile != null) {
        //     //
        //     //                 if (_prefabObjects[x, y] == null) {
        //     //                     _prefabObjects[x,y] = Instantiate(
        //     //                         worldObjectGrid.GetGridObject(x, y).type.prefab, 
        //     //                         new Vector3(x + offset.x, l, y + offset.y), 
        //     //                         Quaternion.identity);
        //     //                 
        //     //                     _prefabObjects[x,y].transform.SetParent(parent);    
        //     //                 }
        //     //                 
        //     //                 
        //     //                     // worldObjectGrid.GetGridObject(x, y).type.prefab, new Vector3Int(x + offset.x, y + offset.y, l),);
        //     //                 // parent
        //     //                 // gridTilemap.SetTile(
        //     //                 //     new Vector3Int(x + offset.x, y + offset.y, l),
        //     //                 //     GetTileFromTileType(worldObjectGrid.GetGridObject(x, y).Type));    
        //     //             }
        //     //             else {
        //     //                 Debug.Log("error tile");
        //     //                 // gridTilemap.SetTile(
        //     //                 //     new Vector3Int(x + offset.x, y + offset.y, l),
        //     //                 //     errorTile);    
        //     //             }
        //     //         }
        //     //     }
        //     // }
        // }

        // private void ClearPrefabParentChildren() {
        //     for (int i = 0; i < itemParent.transform.childCount; i++) {
        //         var obj = itemParent.transform.GetChild(i).gameObject;
        //         GameObject.DestroyImmediate(obj);
        //     }
        // }

        // public void ClearCursor() {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void DrawBoxCursorAt(Vector3 start, Vector3 end) {
        //     throw new System.NotImplementedException();
        // }
        //
        // public void DrawCursorAt(Vector3 pos) {
        //     throw new System.NotImplementedException();
        // }

        // public List<GameObject> GetAllItems() {
	       //  List<GameObject> items = new List<GameObject>();
        //
	       //  ItemGrid[] itemsGrid = gridContainer.items;
	       //  
	       //  for ( int layer = 0; layer < itemsGrid.Length; layer++ ) {
		      //   ItemGrid currentItemGrid = itemsGrid[layer];
		      //   for ( int y = 0; y < currentItemGrid.Depth; y++ ) {
			     //    for ( int x = 0; x < currentItemGrid.Width; x++ ) {
							 //  Item item = currentItemGrid.GetGridObject(x, y);
							 //  GameObject itemRepresentation = _itemObjects[x, layer, y];
        //
							 //  if ( itemRepresentation is { } ) {
								//   items.Add(itemRepresentation);
							 //  }
			     //    }
		      //   }
	       //  }
        //
	       //  return items;
        // }
        
///// Unity Functions //////////////////////////////////////////////////////////////////////////////
 
        //todo fix pls
        // private void Start() {
	       //  //setup parent
	       //  // ClearPrefabParentChildren();
        // }
    }
}