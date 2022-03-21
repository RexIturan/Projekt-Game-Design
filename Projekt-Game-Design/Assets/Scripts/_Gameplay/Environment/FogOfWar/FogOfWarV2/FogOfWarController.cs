using Events.ScriptableObjects.FieldOfView;
using Grid;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace _Gameplay.Environment.FogOfWar.FogOfWarV2 {
	public class FogOfWarController : MonoBehaviour {

		private const int Hidden = 0;
		private const int Visible = 1;
		private const int Shadow = 2;

		private static readonly int viewTextureId = Shader.PropertyToID("_PlayerViewTexture"),
			dimensionId = Shader.PropertyToID("_TileMapDimensions"),
			offsetId = Shader.PropertyToID("_TileMapOffset");
		
		[SerializeField] private Texture2D viewTexture2D;
		[SerializeField] private GraphicsFormat textureFormat = GraphicsFormat.R8_SInt;
		[SerializeField] private GridDataSO gridDataSO;
		[SerializeField] private Material fogOfWarMaterial; 
		[SerializeField] private Material fogOfWarHideMaterial;
		
		[Header("Sending Event On"), SerializeField]
		private FOV_ViewEventChannelSO updatePlayerViewEC;
		
		// cache curtrent view / visited
		private int[,] view;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private Color GetViewColor(int x, int y, int[,] ints) {
			Color color;
			switch ( view[x,y] ) {
				//current
				case Visible:
					color = new Color(1, 0, 0); 
					break;
				//old
				case Shadow:
					color = new Color(0.5f, 0, 0);
					break;
				//shadow
				case Hidden:
				default:
					color = new Color(0.0f, 0, 0);
					break;
			}

			return color;
		}
	
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////		
		
		public void UpdatePlayerView(bool[,] newView) {
			//convert all to shadow and all in new view to current

			var width = gridDataSO.Width;
			var depth = gridDataSO.Depth;
					
			for ( int y = 0; y < depth; y++ ) {
				for ( int x = 0; x < width; x++ ) {
					if ( view[x, y] == Visible ) {
						view[x, y] = Shadow;
					}
					
					if ( newView[x, y] ) {
						view[x, y] = Visible;
					}
				}
			}
			
			// update view
			UpdateViewTexture();
		}
		
///// Public Functions /////////////////////////////////////////////////////////////////////////////
		
		// load view from save
		public void Load(int[,] viewData) {

			if ( viewData is { } ) {
				view = ( int[,] )viewData.Clone();	
			}
			else {
				view = new int[gridDataSO.Width, gridDataSO.Depth];
			}

			//todo refactor
			viewTexture2D = new Texture2D(view.GetLength(0), view.GetLength(1)) {
				filterMode = FilterMode.Point,
				wrapMode = TextureWrapMode.Clamp
			};

			// set material variables
			// Shader.SetGlobalTexture(viewTextureId, viewTexture2D);
			fogOfWarMaterial.SetTexture(viewTextureId, viewTexture2D);
			fogOfWarHideMaterial.SetTexture(viewTextureId, viewTexture2D);
		}

		// update texture
		[ContextMenu("UpdateViewTexture")]
		public void UpdateViewTexture() {
			var width = view.GetLength(0);
			var height = view.GetLength(1);
			
			for ( int y = 0; y < height; y++ ) {
				for ( int x = 0; x < width; x++ ) {
					//todo if slow use setPixels and cache view values as color array
					viewTexture2D.SetPixel(x, y, GetViewColor(x,y,view));
				}
			}
			
			viewTexture2D.Apply();
			// fogOfWarMaterial.SetTexture(viewTextureId, viewTexture2D);
			//todo just change when level changes
			fogOfWarMaterial.SetVector(offsetId,
				new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			fogOfWarMaterial.SetVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));

			fogOfWarHideMaterial.SetVector(offsetId,
				new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			fogOfWarHideMaterial.SetVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));
			
			//todo why doesnt this work?
			// Shader.SetGlobalTexture(viewTextureId, viewTexture2D);
			// Shader.SetGlobalVector(offsetId,
				// new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			// Shader.SetGlobalVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));
		}

///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		[ContextMenu("Start")]
		private void Start() {
			Load(null);
		}

		private void OnEnable() {
			updatePlayerViewEC.OnEventRaised += UpdatePlayerView;
		}

		private void OnDisable() {
			updatePlayerViewEC.OnEventRaised -= UpdatePlayerView;
		}
	}
}