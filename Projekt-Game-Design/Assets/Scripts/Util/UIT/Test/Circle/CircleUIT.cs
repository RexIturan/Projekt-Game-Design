using System;
using System.Collections.Generic;
using GDP01.Util.Util.UIT.Rendering;
using UnityEngine;
using UnityEngine.Profiling;
using UnityEngine.UIElements;

namespace GDP01.Util.UIT.Test {
	public class CircleUIT : VisualElement {

		protected static CustomStyleProperty<Color> s_ColorProperty = new CustomStyleProperty<Color>("--circle-color");
		
		private float _radius = 1;
		private int _rings = 1;
		private int _sections = 10;
		
///// Properties ///////////////////////////////////////////////////////////////////////////////////

//todo max rings & sections

		public Vector2 Center {
			get {
				float width = this.layout.width;
				float height = this.layout.height;

				return new Vector2(width/2, height/2);
			}
		}

		public float Radius {
			get {
				float width = this.contentRect.width;
				float height = this.contentRect.height;

				return Mathf.Min(width, height) /2;
			}
		}

		public int Rings {
			get => _rings;
			set {
				if ( value >= 1 ) {
					_rings = value;
				}
			}
		}

		public int Sections {
			get => _sections;
			set {
				if ( value >= 3 ) {
					_sections = value;
				}
			}
		}
		
		public bool Filled { get; set; }

		public Color32 Color { get; set; } = new Color32(255, 255, 255, 255);

		public float RingWidth { get; set; } = 5;

///// Private Functions ////////////////////////////////////////////////////////////////////////////
		
		private void DrawCircle(MeshGenerationContext mgc) {
			// Debug.Log($"{Radius}, {Sections}, {Rings}");

			// var mc = mgc.Allocate(4, 6, null);
			//
			// //todo transform points
			//
			// var a = new Vertex { position = new Vector3(0, Radius), tint = new Color32(255,255,255,255)};
			// var b = new Vertex { position = new Vector3(Radius, 0), tint = new Color32(255,255,255,255)};
			// var c = new Vertex { position = new Vector3(2*Radius, Radius), tint = new Color32(255,255,255,255)};
			// var d = new Vertex { position = new Vector3(Radius, 2*Radius), tint = new Color32(255,255,255,255)};
			//
			// mc.SetAllVertices(new []{ a,b,c,d});
			// mc.SetAllIndices(new ushort[]{ 0,1,2, 2,3,0 });

			mgc.CreateCircle(Sections, Rings, Radius, Center, Color, Filled, RingWidth);
		}
		
///// Callbacks ////////////////////////////////////////////////////////////////////////////////////

		private void OnGenerateVisualContent(MeshGenerationContext mgc) {
			//use MeshCircle to generate a circle
			Profiler.BeginSample("DrawCircle");
			DrawCircle(mgc);
			Profiler.EndSample();
		}
		
		private void OnCustomStyleResolved(CustomStyleResolvedEvent evt) {
			if (evt.customStyle.TryGetValue(s_ColorProperty, out var colorValue))
				Color = colorValue;
			
			MarkDirtyRepaint();
		}
		
///// Constructors /////////////////////////////////////////////////////////////////////////////////

		public new class UxmlFactory : UxmlFactory<CircleUIT, UxmlTraits> { }

		public new class UxmlTraits : VisualElement.UxmlTraits {
			
			private readonly UxmlIntAttributeDescription sectionsAttribute =
				new UxmlIntAttributeDescription {
					name = "Sections",
					defaultValue = 10
				};
			
			private readonly UxmlIntAttributeDescription ringsAttribute =
				new UxmlIntAttributeDescription {
					name = "Rings",
					defaultValue = 1
				};
			
			private readonly UxmlFloatAttributeDescription ringWidthAttribute =
				new UxmlFloatAttributeDescription {
					name = "RingWidth",
					defaultValue = 10
				};
			
			private readonly UxmlBoolAttributeDescription
				filledAttribute = new UxmlBoolAttributeDescription {
					name = "Filled",
					defaultValue = true
				};

			public override IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription {
				get { yield break; }
			}

			public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc) {
				base.Init(ve, bag, cc);

				if ( ve is CircleUIT element ) {
					element.Clear();

					element.Sections = sectionsAttribute.GetValueFromBag(bag, cc);
					element.Rings = ringsAttribute.GetValueFromBag(bag, cc);
					element.RingWidth = ringWidthAttribute.GetValueFromBag(bag, cc);
					element.Filled = filledAttribute.GetValueFromBag(bag, cc);
				}
			}
		}

		public CircleUIT() {
			pickingMode = PickingMode.Ignore;
			generateVisualContent += OnGenerateVisualContent;
			
			RegisterCallback<CustomStyleResolvedEvent>(OnCustomStyleResolved);
		}
	}
}