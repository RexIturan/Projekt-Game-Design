using Characters;
using Characters.Types;
using Combat;
using Events.ScriptableObjects;
using Grid;
using Level.Grid;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Target effect that creates new target effect in the surrounding tiles. 
		/// </summary>
		[System.Serializable]
		[CreateAssetMenu(fileName = "te_newExpandEffect", menuName = "WorldObjects/TileEffects/Expand Effect")]
    public class ExpandEffectSO : TileEffectSO
		{
				[SerializeField] private GameObject tileEffectPrefab;
				[SerializeField] private CreateTileEffectEventChannelSO createTileEffectEC;

				/// <summary>
				/// Requirements for the tile in which the effect will be placed 
				/// </summary>
				[SerializeField] private TileProperties expansionRequirementsTop;

				/// <summary>
				/// Requirements for the tile beneath the tile in which the effect will be placed 
				/// </summary>
				[SerializeField] private TileProperties expansionRequirementsGround;

				/// <summary>
				/// Creates new tile effect in surroundings. Then removes this effect from the Tile Effect Controller. 
				/// </summary>
				/// <param name="tileEffectController">TileEffect component that has this effect </param>
				override public void OnAction(TileEffectController tileEffectController) {
						Vector3Int center = tileEffectController.GetComponent<GridTransform>().gridPosition;
						GridController gridController = GridController.FindGridController();

						if(gridController) {
								foreach(Vector3Int neighbor in GetSurroundings(center)) {
										TileTypeSO tile = gridController.GetTileAt(neighbor);
										TileTypeSO groundTile = gridController.GetTileAt(neighbor - new Vector3Int(0, 1, 0));

										if ( tile && groundTile &&
												HasAllFlags((int)expansionRequirementsTop, (int)tile.properties) &&
												HasAllFlags((int)expansionRequirementsGround, (int)groundTile.properties)) {
												createTileEffectEC.RaiseEvent(tileEffectPrefab, neighbor);
										}
								}
						}
						else 
								Debug.LogError("Could not find grid controller. Cannot expand tile effect. ");

						tileEffectController.RemoveEffect(this);
				}

				private bool HasAllFlags(int requiredFlags, int actualFlags) {
						return (requiredFlags & actualFlags).Equals(requiredFlags);
				}

				private List<Vector3Int> GetSurroundings(Vector3Int center) {
						List<Vector3Int> surroundings = new List<Vector3Int>();
						surroundings.Add(center + new Vector3Int(1, 0, 0));
						surroundings.Add(center + new Vector3Int(0, 0, 1));
						surroundings.Add(center + new Vector3Int(-1, 0, 0));
						surroundings.Add(center + new Vector3Int(0, 0, -1));
						return surroundings;
				}
    }
}
