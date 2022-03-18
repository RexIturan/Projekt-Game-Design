
namespace UnityEngine.Tilemap_3D {
	[RequireComponent(typeof (Transform))]
	public abstract class GridLayout_3D : MonoBehaviour {

		private Vector3 _cellSize = Vector3.one;
		private Vector3 _cellGap = Vector3.zero;
		private ECellLayout _cellLayout = ECellLayout.Layout_1x1x1;
		private int _maxGridSize = 128;
///// Properties ///////////////////////////////////////////////////////////////////////////////////

		public Vector3 CellSize { get => _cellSize; private set => _cellSize = value; }
		public Vector3 CellGap  { get => _cellGap;  private set => _cellGap = value; }
		public ECellLayout CellLayout { get => _cellLayout;  private set => _cellLayout = value; }
		
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
			public Vector3 LocalToWorld(Vector3 localPosition) {
				return transform.localToWorldMatrix.MultiplyVector(localPosition);
			}
			
			// - World
			public Vector3 WorldToLocal(Vector3 worldPosition) {
				return transform.worldToLocalMatrix.MultiplyVector(worldPosition);
			}
			
			public Vector3 WorldToCell(Vector3 worldPosition) {
				return transform.worldToLocalMatrix.MultiplyVector(worldPosition);
			}
			
			// - Cell
			public Vector3 CellToLocal(Vector3 cellPosition) {
				Vector3 ret = Vector3.zero;
				
				// 
				
				return ret;
			}
		
		#endregion
		
		#region CellCenter Getter/Setter

		#endregion

///// Public Types		
		public enum ECellLayout {
			Layout_1x1x1,
			Layout_2x2x2
		}
	}
}