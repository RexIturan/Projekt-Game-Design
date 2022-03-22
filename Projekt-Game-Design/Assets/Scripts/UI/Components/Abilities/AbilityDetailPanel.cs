using Ability;
using System.Collections;
using System.Collections.Generic;
using UI.Components.Stats;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Ability
{
		/// <summary>
		/// VisualElement containing detailed properties of an ability. 
		/// </summary>
		public class AbilityDetailPanel : VisualElement
		{
				private static readonly bool ICON_ON_DEFAULT = true;

				private static readonly string defaultStyleSheet = "abilityPanel";
				private static readonly string className = "abilityPanel";
				private static readonly string headerClassName = "abilityPanelHeader";
				private static readonly string patternClassName = "abilityPattern";

				public AbilityDetailPanel(AbilitySO ability) : this(ability, ICON_ON_DEFAULT) { }

				public AbilityDetailPanel(AbilitySO ability, bool iconOn)
				{
						// Setting up style
						styleSheets.Add(Resources.Load<StyleSheet>(defaultStyleSheet));
						AddToClassList(className);

						// header
						VisualElement header = new VisualElement();
						header.AddToClassList(headerClassName);

						if ( iconOn )
						{
								Image icon = new Image();
								icon.image = ability.icon.texture;
								header.Add(icon);
						}

						TextElement headerName = new TextElement();
						headerName.text = ability.name;
						header.Add(headerName);

						Add(header);

						// description
						TextElement description = new TextElement();
						description.text = ability.description;

						Add(description);

						// stats
						VisualElement stats = new VisualElement();

						StatBulletPoint costs = new StatBulletPoint(StatType.COSTS, ability.costs);
						stats.Add(costs);
						StatBulletPoint range = new StatBulletPoint(StatType.RANGE, ability.range);
						stats.Add(range);

						Add(stats);

						// effects
						if ( ability.targetedEffects.Length > 0 )
						{
								VisualElement effects = new VisualElement();

								foreach(TargetedEffect targetedEffect in ability.targetedEffects)
								{
										VisualElement effect = new VisualElement();

										if(!targetedEffect.effect.type.Equals(DamageType.Healing))
										{
												StatBulletPoint damage = new StatBulletPoint(StatType.DAMAGE, targetedEffect.effect.baseDamage);
												effect.Add(damage);
										}
										else
										{
												StatBulletPoint healing = new StatBulletPoint(StatType.HEALING, targetedEffect.effect.baseDamage);
												effect.Add(healing);
										}

										if(targetedEffect.area.GetPattern().Length > 1 || targetedEffect.area.GetPattern().GetLength(1) > 1)
										{
												TextElement patternHeader = new TextElement();
												patternHeader.text = "Area of effect: ";
												effect.Add(patternHeader);

												PatternElement pattern = new PatternElement(targetedEffect.area);
												pattern.AddToClassList(patternClassName);
												effect.Add(pattern);
										}

										Add(effect);
								}
						}
				}
		}
}