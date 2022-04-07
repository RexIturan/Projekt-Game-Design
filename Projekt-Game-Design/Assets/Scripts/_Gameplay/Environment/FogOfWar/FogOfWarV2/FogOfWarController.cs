using System.Collections.Generic;
using _Gameplay.Environment.FogOfWar.FogOfWarV2.Types;
using Events.ScriptableObjects.FieldOfView;
using Grid;
using UnityEditor;
using UnityEngine;
using UnityEngine.Experimental.Rendering;

namespace _Gameplay.Environment.FogOfWar.FogOfWarV2 {
	public class FogOfWarController : MonoBehaviour {

		#region Monobehaviour Singelton

		private static FogOfWarController instance;
		public static FogOfWarController Current => instance; 
			
		private void Awake() {
			if ( instance == null ) {
				instance = this;
			}
			else {
				Debug.LogWarning("There can only be one UpdateHelper!");
				Destroy(gameObject);
			}
		}

		private void OnDestroy() {
			instance = null;
		}

		#endregion
		
		private const int Hidden = 0;
		private const int Visible = 1;
		private const int Shadow = 2;

		private static readonly int 
			textureId = Shader.PropertyToID("_PlayerViewTexture"),
			dimensionId = Shader.PropertyToID("_TileMapDimensions"),
			offsetId = Shader.PropertyToID("_TileMapOffset");

		[SerializeField] private VoidEventChannelSO ToggleFogOfWarEC;
		[SerializeField] private Texture2D debugViewTexture2D;
		[SerializeField] private Texture2D viewTexture2D;
		[SerializeField] private GridDataSO gridDataSO;
		[SerializeField] private Material fogOfWarMaterial; 
		[SerializeField] private Material fogOfWarHideMaterial;
		[SerializeField] private ViewCacheSO viewCacheSO;
		
		[Header("Recieving Event On"), SerializeField]
		private FOV_ViewEventChannelSO updatePlayerViewEC;
		[SerializeField] private VoidEventChannelSO onLevelLoadedEC;
		
		// cache curtrent view / visited
		private int[,] view;
		private bool fogActive = true;
		
///// Private Functions ////////////////////////////////////////////////////////////////////////////

		private Color GetColor(int value) {
			Color color;
			switch ( value ) {
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

		private Color GetViewColor(int x, int y, int[,] ints) {
			return GetColor(ints[x, y]);
		}
	
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////		
		
		public void UpdatePlayerView(bool[,] newView) {

			//the first update of the fog is to early, idont have time to fix that
			if ( view == null || view is {Length: 0} ) {
				Load(null);
			}
			
			var width = gridDataSO.Width;
			var depth = gridDataSO.Depth;

			Debug.LogWarning($"----- UpdatePlayerView view: w:{view.GetLength(0)} h:{view.GetLength(1)} -----");

			if ( view.GetLength(0) < width || view.GetLength(1) < depth ) {
				Debug.LogError($"Width and depth were to big for the current view data\n" +
				               $"width:{width}, depth:{depth}, view dims {view.GetLength(0)}, {view.GetLength(1)}");
				Load(null);
			}
			
			//convert all to shadow and all in new view to current
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

		private void SetViewTexture() {
			
			Shader.SetGlobalTexture(textureId, viewTexture2D);
			
			// fogOfWarMaterial.SetTexture(textureId, viewTexture2D);
			// fogOfWarHideMaterial.SetTexture(textureId, viewTexture2D);
		}

		private void SetDebugViewTexture() {
			Shader.SetGlobalTexture(textureId, debugViewTexture2D);
			
			// fogOfWarMaterial.SetTexture(textureId, debugViewTexture2D);
			// fogOfWarHideMaterial.SetTexture(textureId, debugViewTexture2D);
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

			// Debug.LogWarning($"----- load view: w:{view.GetLength(0)} h:{view.GetLength(1)} -----");
			
			//todo refactor
			if ( viewTexture2D == null ) {
				viewTexture2D = new Texture2D(view.GetLength(0), view.GetLength(1)) {
					filterMode = FilterMode.Point,
					wrapMode = TextureWrapMode.Clamp
				};	
			}
			else {
				viewTexture2D.Reinitialize(view.GetLength(0), view.GetLength(1));
			}
			viewTexture2D.Apply();

			if ( debugViewTexture2D == null ) {
				debugViewTexture2D = new Texture2D(1, 1) {
					filterMode = FilterMode.Point, wrapMode = TextureWrapMode.Repeat
				};
				debugViewTexture2D.SetPixel(0,0, GetColor(Visible));	
			}
			else {
				debugViewTexture2D.Reinitialize(1, 1);
				debugViewTexture2D.SetPixel(0,0, GetColor(Visible));
			}
			debugViewTexture2D.Apply();
			
			Debug.LogWarning($"----- load view: w:{view.GetLength(0)} h:{view.GetLength(1)} -----\n" +
			                 $"----- view texturue {viewTexture2D}, debug texture {debugViewTexture2D}\n" +
			                 $"----- view mat {fogOfWarMaterial}, debug mat {fogOfWarHideMaterial}\n" +
			                 $"----- {textureId} {dimensionId} {offsetId}");
			
			#if UNITY_EDITOR
				// AssetDatabase.CreateAsset(viewTexture2D, "Assets/viewTexture2D.asset");
				// AssetDatabase.CreateAsset(debugViewTexture2D, "Assets/debugViewTexture2D.asset");
			#endif
			
			// set material variables
			// Shader.SetGlobalTexture(viewTextureId, viewTexture2D);
			SetViewTexture();
		}

		// update texture
		[ContextMenu("UpdateViewTexture")]
		public void UpdateViewTexture() {
			var width = view.GetLength(0);
			var height = view.GetLength(1);
			
			Debug.LogWarning($"----- update view: w:{view.GetLength(0)} h:{view.GetLength(1)} -----\n" +
			                 $"----- view texturue {viewTexture2D}, debug texture {debugViewTexture2D}");
			
			for ( int y = 0; y < height; y++ ) {
				for ( int x = 0; x < width; x++ ) {
					//todo if slow use setPixels and cache view values as color array
					viewTexture2D.SetPixel(x, y, GetViewColor(x, y, view));
				}
			}
			
			viewTexture2D.Apply();
			
			
			Shader.SetGlobalVector(offsetId,
				new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			Shader.SetGlobalVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));
			
			
			// fogOfWarMaterial.SetTexture(viewTextureId, viewTexture2D);
			//todo just change when level changes
			// fogOfWarMaterial.SetVector(offsetId,
			// 	new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			// fogOfWarMaterial.SetVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));
			//
			// fogOfWarHideMaterial.SetVector(offsetId,
			// 	new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			// fogOfWarHideMaterial.SetVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));
			
			Debug.LogWarning($"----- mat{fogOfWarMaterial}, matHide{fogOfWarHideMaterial} ");
			
			//todo why doesnt this work?
			// Shader.SetGlobalTexture(viewTextureId, viewTexture2D);
			// Shader.SetGlobalVector(offsetId,
				// new Vector4(gridDataSO.OriginPosition.x *-1, gridDataSO.OriginPosition.z *-1));
			// Shader.SetGlobalVector(dimensionId, new Vector4(gridDataSO.Width , gridDataSO.Depth));
			UpdateTexture();
		}

		public void InitViewFromSave(List<string> viewSave) {
			if ( viewSave is { Count: > 0 } ) {
				var width = gridDataSO.Width;
				var depth = gridDataSO.Depth;

				bool[,] visible = new bool[width, depth];
			
				for (int y = 0; y < depth; y++) {
					string str = viewSave[y];
					for (int x = 0; x < width; x++) {
						visible[x, y] = str[x].Equals('+'); 
					}
				}
			
				UpdatePlayerView(visible);
			}
		}
		
		public List<string> GetViewAsStringList() {
			List<string> viewSave = new List<string>();
			
			// gen string
			var width = gridDataSO.Width;
			var depth = gridDataSO.Depth;

			if ( view is { } && view.GetLength(0) == width && view.GetLength(1) == depth ) {
				for (int y = 0; y < depth; y++) {
					string str = "";
					for (int x = 0; x < width; x++) {
						if ( view[x, y] == Visible || view[x, y] == Shadow ) {
							str += "+";
						}
						else {
							str += "-";
						}
					}
					viewSave.Add(str);
				}	
			}

			return viewSave;
		}

		private void UpdateTexture() {
			Debug.LogWarning($"----- update fog active: {fogActive}");
			
			if ( fogActive ) {
				SetViewTexture();
			}
			else {
				SetDebugViewTexture();
			}
		}
		
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		private void HandleToggleFogOfWar() {
			fogActive = !fogActive;

			UpdateTexture();
		}
		
		private void HandleLevelLoaded() {
			Load(null);
			UpdateViewTexture();
			
			InitViewFromSave(viewCacheSO.view);
			viewCacheSO.view = null;
			UpdateViewTexture();
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		[ContextMenu("Start")]
		private void Start() {
			Load(null);
		}

		private void OnEnable() {
			Load(null);
			UpdateViewTexture();
			//todo Updateplayervisipon from fieldofview controller

			onLevelLoadedEC.OnEventRaised += HandleLevelLoaded;
			ToggleFogOfWarEC.OnEventRaised += HandleToggleFogOfWar;
			updatePlayerViewEC.OnEventRaised += UpdatePlayerView;
		}

		private void OnDisable() {
			onLevelLoadedEC.OnEventRaised -= HandleLevelLoaded;
			ToggleFogOfWarEC.OnEventRaised -= HandleToggleFogOfWar;
			updatePlayerViewEC.OnEventRaised -= UpdatePlayerView;
		}
	}
}