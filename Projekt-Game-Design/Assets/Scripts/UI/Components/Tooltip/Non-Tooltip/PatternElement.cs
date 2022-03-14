using Ability;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Ability
{
		/// <summary>
		/// Representation of a pattern as a VisualElement. 
		/// </summary>
		public class PatternElement : VisualElement
		{
				private static readonly string defaultStyleSheet = "patternElement";
				private static readonly string className = "patternElement";
				private static readonly string rowClassName = "patternElementRow";

				private readonly Sprite targetImage = Resources.Load<Sprite>("Sprites/tileTarget");
				private readonly Sprite nonTargetImage = Resources.Load<Sprite>("Sprites/tileNonTarget");
				private readonly Sprite anchorImage = Resources.Load<Sprite>("Sprites/anchorTarget");
				private readonly Sprite anchorNonTargetImage = Resources.Load<Sprite>("Sprites/anchorNonTarget");

				public PatternElement(TargetPattern pattern)
				{
						bool[][] boolPattern = pattern.GetPattern();
						for(int row = 0; row < boolPattern[0].Length; row++ )
						{
								// Setting up style
								styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
								AddToClassList(className);

								VisualElement rowElement = new VisualElement();
								rowElement.AddToClassList(rowClassName);

								for (int col = 0; col < boolPattern.Length; col++ )
								{
										Image tile = new Image();

										if ( pattern.GetAnchor().Equals(new Vector2Int(col, row)) )
										{
												if ( boolPattern[col][row] )
												{
														tile.image = anchorImage.texture;
												}
												else
												{
														tile.image = anchorNonTargetImage.texture;
												}
										}
										else
										{
												if ( boolPattern[col][row] )
												{
														tile.image = targetImage.texture;
												}
												else
												{
														tile.image = nonTargetImage.texture;
												}
										}

										rowElement.Add(tile);
								}

								Add(rowElement);
						}
				}
		}
}