using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Tooltip
{
		/// <summary>
		/// Attached to UI gameobject in scene to display tooltips. 
		/// </summary>
		public class TooltipLayer : MonoBehaviour {
			
			#region Monobehaviour Singelton

			private static TooltipLayer instance;
			public static TooltipLayer Current => instance; 
			
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
			
			
				private static readonly string defaultStyleSheet = "tooltip";
				private static readonly string className = "tooltipLayer";

				[SerializeField] private UIDocument uIDocument;
				private VisualElement layerElement;

				private void Start()
				{
						// Adding to root
						VisualElement root = uIDocument.rootVisualElement;
						root.Add(layerElement);
				}

				public void Initialize()
				{
						// Creating new container element 
						layerElement = new VisualElement();

						layerElement.AddToClassList(className);
						layerElement.pickingMode = PickingMode.Ignore;

						// Setting up style
						layerElement.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
				}

				/// <summary>
				/// Searches for the tooltip layer VisualElement in the game scene. 
				/// If no such element has been created by the tooltip layer component, 
				/// then it will be created then. 
				/// </summary>
				/// <returns>VisualElement of the tooltip layer or null if no tooltip layer component could be found </returns>
				public static VisualElement FindTooltipLayer()
				{
						TooltipLayer tooltipLayerComponent = FindObjectOfType<TooltipLayer>();

						if ( tooltipLayerComponent )
						{
								if ( tooltipLayerComponent.layerElement == null )
										tooltipLayerComponent.Initialize();

								return tooltipLayerComponent.layerElement;
						}
						else
						{
								Debug.LogError("No tooltip layer component found in scene. ");
								return null;
						}
				}
		}
}