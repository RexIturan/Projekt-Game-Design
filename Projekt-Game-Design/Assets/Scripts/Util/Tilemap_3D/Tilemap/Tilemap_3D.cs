using UnityEngine;
using UnityEngine.Tilemap_3D;

namespace Util.Tilemap_3D.Tilemap {
	[RequireComponent(typeof (Transform))]
	public class Tilemap_3D : GridLayout_3D {
		[SerializeField] private Vector3 _tileAnchor = new Vector3(0.5f, -0.5f, 0.5f);

		
///// Properties
		public Grid_3D layoutGrid {
			//todo caching?
			get {
				return GetComponentInParent<Grid_3D>();
			}
		}
		
		// public Vector3 GetCellCenterLocal(Vector3Int position) => this._tileAnchor
	}
}