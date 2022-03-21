using Ability;
using System.Collections.Generic;
using UI.Components.Ability;
using UI.Components.Stats;
using UI.Components.Tooltip;
using UnityEngine;
using UnityEngine.UIElements;

namespace UI.Components.Item
{
		/// <summary>
		/// VisualElement containing a detailed overview of an item's properties. 
		/// </summary>
		public class ItemDetailPanel : VisualElement
		{
				private static readonly bool ICON_ON_DEFAULT = true;

				private static readonly string defaultStyleSheet = "itemPanel";
				private static readonly string className = "itemPanel";
				private static readonly string headerClassName = "itemPanelHeader";
				private static readonly string abilitiesClassName = "itemPanelAbilities";

				/// <summary>
				/// Creates arbitrary detail view of an item. 
				/// </summary>
				/// <param name="item">item which's properties are displayed in the detail view </param>
				public ItemDetailPanel(ItemSO item) : this(item, ICON_ON_DEFAULT) { }

				/// <summary>
				/// Creates arbitrary detail view of an item. 
				/// </summary>
				/// <param name="item">item which's properties are displayed in the detail view </param>
				/// <param name="iconOn">decides whether or not the detail view displays the item's icon </param>
				public ItemDetailPanel(ItemSO item, bool iconOn)
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
								icon.image = item.icon.texture;
								header.Add(icon);
						}

						TextElement headerName = new TextElement();
						headerName.text = item.name;
						header.Add(headerName);

						Add(header);

						// description
						TextElement description = new TextElement();
						description.text = "item.description";

						Add(description);
				}

				/// <summary>
				/// Creates detail view of a weapon. 
				/// </summary>
				/// <param name="weapon">weapon which's properties are displayed in the detail view </param>
				/// <param name="iconOn">decides whether or not the detail view displays the item's icon </param>
				public ItemDetailPanel(WeaponSO weapon, bool iconOn) : this(( ItemSO )weapon, iconOn)
				{
						// adding stats
						VisualElement stats = new VisualElement();

						StatBulletPoint damage = new StatBulletPoint(StatType.DAMAGE, calculateWeaponDamage(weapon));
						stats.Add(damage);
						StatBulletPoint range = new StatBulletPoint(StatType.RANGE, calculateWeaponRange(weapon));
						stats.Add(range);

						Add(stats);

						// adding abilities
						if ( weapon.abilities.Length > 0 )
						{
								TextElement abilityHeader = new TextElement();
								abilityHeader.text = "Abilities: ";
								Add(abilityHeader);

								VisualElement abilities = new VisualElement();

								foreach(AbilitySO ability in weapon.abilities)
								{
										Image abilityIcon = new Image();
										abilityIcon.image = ability.icon.texture;
										abilities.Add(abilityIcon);

										TooltipElement abilityTooltip = new TooltipElement(abilityIcon);
										abilityTooltip.pickingMode = PickingMode.Ignore;
										AbilityDetailPanel abilityPanel = new AbilityDetailPanel(ability, true);
										abilityTooltip.Add(abilityPanel);
								}

								abilities.AddToClassList(abilitiesClassName);
								Add(abilities);
						}
				}

				/// <summary>
				/// Creates detail view of an armor piece. 
				/// </summary>
				/// <param name="armor">armor which's properties are displayed in the detail view </param>
				public ItemDetailPanel(ArmorSO armor) : this(armor, ICON_ON_DEFAULT) { }

				/// <summary>
				/// Creates detail view of an armor piece. 
				/// </summary>
				/// <param name="armor">armor which's properties are displayed in the detail view </param>
				/// <param name="iconOn">decides whether or not the detail view displays the item's icon </param>
				public ItemDetailPanel(ArmorSO armor, bool iconOn) : this((ItemSO) armor, iconOn)
				{
						// adding stats
						VisualElement stats = new VisualElement();

						StatBulletPoint defense = new StatBulletPoint(StatType.DEFENSE, armor.armor);
						stats.Add(defense);
				}

				#region Doesn't belong here (it is Item properties). 

				/// <summary>
				/// Decides the displayed damage of a weapon. (Here, it's the maximum base damage of its abilities.) 
				/// TODO: Should be placed in WeaponSO (not done yet due to merging issues). 
				/// </summary>
				/// <param name="weapon">Weapon which's damage is calculated</param>
				/// <returns>Damage of the weapon</returns>
				private int calculateWeaponDamage(WeaponSO weapon)
				{
						int damage = 0;

						List<TargetedEffect> targetedEffects = new List<TargetedEffect>();

						foreach(AbilitySO ability in weapon.abilities)
						{
								targetedEffects.AddRange(ability.targetedEffects);
						}

						foreach(TargetedEffect effect in targetedEffects)
						{
								if ( damage < effect.effect.baseDamage )
										damage = effect.effect.baseDamage;
						}

						return damage;
				}

				/// <summary>
				/// Decides the displayed range of a weapon. (Here, it's the maximum range of its abilities.) 
				/// TODO: Should be placed in WeaponSO (not done yet due to merging issues). 
				/// </summary>
				/// <param name="weapon">Weapon which's range is calculated</param>
				/// <returns>Range of the weapon</returns>
				private int calculateWeaponRange(WeaponSO weapon)
				{
						int range = 0;
						
						foreach ( AbilitySO ability in weapon.abilities )
						{
								if ( range < ability.range )
										range = ability.range;
						}

						return range;
				}

				#endregion
		}
}