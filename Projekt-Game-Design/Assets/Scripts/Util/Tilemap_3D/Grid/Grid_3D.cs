using UnityEngine.Tilemaps;

namespace UnityEngine.Tilemap_3D{
	
	[RequireComponent(typeof (Transform))]
	public class Grid_3D : GridLayout_3D {
		[SerializeField] private Vector3 _cellSize = Vector3.one;
		[SerializeField] private Vector3 _cellGap = Vector3.zero;
		[SerializeField] private ECellLayout _cellLayout = ECellLayout.Layout_1x1x1;

		[SerializeField, Range(1, 256)] private int _maxGridSize = 128;
	}
}