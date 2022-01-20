

namespace UnityEngine.Tilemap_3D {
	
	[RequireComponent(typeof (Transform))]
	public class GridLayout_3d : MonoBehaviour {

///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public Vector3 CellSize { get; set; }
		public Vector3 CellGap  { get; set; }

		public Bounds GridBounds { get; set; }
		
///// Public Function //////////////////////////////////////////////////////////////////////////////		

		#region Bounds

			// AABB Bound Type
			// - vector3int: center
			// - vector3int: extends
			
			// init Bounds
			// change Bounds
		
		#endregion


		
		#region Position Transformations

			//position Transformations
			// - Local
			// - World
			// - Cell
		
		#endregion

		
		
		#region CellCenter Getter/Setter

		#endregion
	}
}