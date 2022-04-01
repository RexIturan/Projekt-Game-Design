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
				/// Creates new tile effect in surroundings. Then removes this effect from the Tile Effect Controller. 
				/// </summary>
				/// <param name="tileEffectController">TileEffect component that has this effect </param>
				override public void OnAction(TileEffectController tileEffectController) {
						Vector3Int center = tileEffectController.GetComponent<GridTransform>().gridPosition;

						foreach ( Vector3Int neighbor in GetSurroundings(center) ) {
								createTileEffectEC.RaiseEvent(tileEffectPrefab, neighbor);
						}

						tileEffectController.RemoveEffect(this);
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
