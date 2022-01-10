using System.Collections.Generic;
using UnityEngine.UIElements;
using UI.Components.Character;
using UnityEngine;

namespace UI.Components.Character {
	public class CharacterStatusValuePanel : VisualElement {
///// USS Class Names //////////////////////////////////////////////////////////////////////////////
		private static readonly string baseUssClassName = "characterStatusValuePanel";
		
		private static readonly string containerSuffix = "container";
		private static readonly string characterIconContainerSuffix = "characterIconContainer";
		private static readonly string ProgressBarContainerSuffix = "progressBarContainer";
		private static readonly string StatusValueFieldContainerSuffix = "statusValueFieldContainer";
		private static readonly string StatusValueFieldRowSuffix = "statusValueFieldRow";
		
		private static readonly string defaultStyleSheet = "UI/characterStatusValuePanel";
		private static readonly string healthBarStyleSheet = "UI/healthBar";
		private static readonly string energyBarStyleSheet = "UI/energyBar";
		private static readonly string armorBarStyleSheet = "UI/armorBar";
///// PRIVATE VARIABLES ////////////////////////////////////////////////////////////////////////////
		//todo char
		// icon
		// name
		// level
		
		//todo status bars
		// health
		// energy
		// armor
		
		//todo StatusValueNumPanel
		// movement
		// vision
		// strength
		// dexterity
		// inelligence

///// UI ELEMENTS //////////////////////////////////////////////////////////////////////////////////

		private VisualElement _container;
		
		private VisualElement _progressBarContainer;
		private List<ProgressBar> _progressBars;

		private VisualElement _statusValueFieldContainer;
		private List<VisualElement> _statusValueRow;
		private List<StatusValueField> _statusValueFields;

///// PROPERTIES ///////////////////////////////////////////////////////////////////////////////////
 
		public int Rows { get; set; }
		
		public CharacterIcon CharIcon { get; set; }
		
		//healthbar
		public ProgressBar HealthBar { get; set; }
		//energy
		public ProgressBar EnergyBar { get; set; }
		//armor
		public ProgressBar ArmorBar { get; set; }
		
		//Strength
		public StatusValueField StrengthField { get; set; }
		//Dexterity
		public StatusValueField DexterityField { get; set; }
		//Intelligence
		public StatusValueField IntelligenceField { get; set; }
		//Movement
		public StatusValueField MovementField { get; set; }
		public StatusValueField VisionField { get; set; }
		
///// PRIVATE FUNCTIONS ////////////////////////////////////////////////////////////////////////////

		private void BuildComponents() {

			//init base Component
			this.name = "CharacterStatusValuePanel";

			//default styleSheet
			this.styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
			this.AddToClassList(baseUssClassName);

			_container = new VisualElement {
				name = "CharacterStatusValuePanel-Container"
			};
			_container.AddToClassList(GetClassNameWithSuffix(containerSuffix));
			
			CharIcon = new CharacterIcon {};
			CharIcon.UpdateComponent();
			CharIcon.AddToClassList(GetClassNameWithSuffix(characterIconContainerSuffix));

			// Status Bars
			_progressBarContainer = new VisualElement {
				name = "CharacterStatusValuePanel-ProgressBarContainer"
			};
			_progressBarContainer.AddToClassList(GetClassNameWithSuffix(ProgressBarContainerSuffix));
			
			for ( int i = 0; i < _progressBars.Count; i++ ) {
				_progressBarContainer.Add(_progressBars[i]);
			}
						
			//Status Value Fields
			int cols = _statusValueFields.Count / Rows;
			cols += _statusValueFields.Count % 2 > 0 ? 1 : 0;
			
			_statusValueFieldContainer = new VisualElement {
				name = "CharacterStatusValuePanel-StatusValueFieldContainer",
			};
			_statusValueFieldContainer.AddToClassList(GetClassNameWithSuffix(StatusValueFieldContainerSuffix));
			
			_statusValueRow.Clear();
			for ( int i = 0; i < Rows; i++ ) {
				var row = new VisualElement {
					name = $"CharacterStatusValuePanel-StatusValueRow-{i}"
				};
				row.AddToClassList(GetClassNameWithSuffix(StatusValueFieldRowSuffix));
				for ( int j = 0; j < cols; j++ ) {
					var index = j + i * cols;
					var field = index < _statusValueFields.Count ? _statusValueFields[index] : null; 
					if( field is {} ) {
						row.Add(field);	
					}
				}
				_statusValueRow.Add(row);
				_statusValueFieldContainer.Add(_statusValueRow[i]);
			}
			
			_container.Add(CharIcon);
			_container.Add(_progressBarContainer);
			_container.Add(_statusValueFieldContainer);
			this.Add(_container);
		}

///// Util /////////////////////////////////////////////////////////////////////////////////////////

		/// 
		private string GetClassNameWithSuffix(string suffix) {
			return $"{baseUssClassName}-{suffix}";
		}

///// PUBLIC FUNCTIONS  ////////////////////////////////////////////////////////////////////////////

		public void UpdateComponents() {
			//todo
		}

		public void SetViibility(bool visibility) {
			if ( visibility ) {
				this.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.Flex);				
			}
			else {
				this.style.display = new StyleEnum<DisplayStyle>(DisplayStyle.None);
			}
		}

///// PUBLIC CONSTRUCTORS //////////////////////////////////////////////////////////////////////////
		public new class UxmlFactory : UxmlFactory<CharacterStatusValuePanel, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is CharacterStatusValuePanel element ) {
					element.Clear();
					
					element.BuildComponents();
					element.UpdateComponents();
				}
			}
		}
		
		public CharacterStatusValuePanel() : this("test char", 2){}

		public CharacterStatusValuePanel(string name, int rows) {
			_progressBars = new List<ProgressBar>();
			_statusValueFields = new List<StatusValueField>();
			
			_statusValueRow = new List<VisualElement>();
			
			Rows = 1;
			
			//todo remove test
			HealthBar = new ProgressBar("Health", 10, 10, 0);
			HealthBar.styleSheets.Add(Resources.Load<StyleSheet>(healthBarStyleSheet));

			EnergyBar = new ProgressBar("Energy", 30, 30, 0);
			EnergyBar.styleSheets.Add(Resources.Load<StyleSheet>(energyBarStyleSheet));
			
			ArmorBar = new ProgressBar("Armor", 10, 10, 0);
			ArmorBar.styleSheets.Add(Resources.Load<StyleSheet>(armorBarStyleSheet));
			
			_progressBars.Add(HealthBar);
			_progressBars.Add(EnergyBar);
			// _progressBars.Add(ArmorBar);

			StrengthField = new StatusValueField("Strength", 5, 0);
			DexterityField = new StatusValueField("Dexterity", 5, 0);
			IntelligenceField = new StatusValueField("Intelligence", 5, 0);
			MovementField = new StatusValueField("Movement", 5, 0);
			VisionField = new StatusValueField("Vision Range", 10, 0);

			// _statusValueFields.Add(StrengthField);
			// _statusValueFields.Add(DexterityField);
			// _statusValueFields.Add(IntelligenceField);
			_statusValueFields.Add(MovementField);
			_statusValueFields.Add(VisionField);
			// _statusValueFields.Add(new StatusValueField("NA", 0, 0));
			
			BuildComponents();
			UpdateComponents();
		}
	}
}