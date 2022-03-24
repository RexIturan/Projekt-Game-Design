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
				/// <param name="itemType">item which's properties are displayed in the detail view </param>
				public ItemDetailPanel(ItemTypeSO itemType) : this(itemType, ICON_ON_DEFAULT) { }

				/// <summary>
				/// Creates arbitrary detail view of an item. 
				/// </summary>
				/// <param name="itemType">item which's properties are displayed in the detail view </param>
				/// <param name="iconOn">decides whether or not the detail view displays the item's icon </param>
				public ItemDetailPanel(ItemTypeSO itemType, bool iconOn)
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
								icon.image = itemType.icon.texture;
								header.Add(icon);
						}

						TextElement headerName = new TextElement();
						headerName.text = itemType.name;
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
				/// <param name="weaponType">weapon which's properties are displayed in the detail view </param>
				/// <param name="iconOn">decides whether or not the detail view displays the item's icon </param>
				public ItemDetailPanel(WeaponTypeSO weaponType, bool iconOn) : this(( ItemTypeSO )weaponType, iconOn)
				{
						// adding stats
						VisualElement stats = new VisualElement();

						StatBulletPoint damage = new StatBulletPoint(StatType.DAMAGE, calculateWeaponDamage(weaponType));
						stats.Add(damage);
						StatBulletPoint range = new StatBulletPoint(StatType.RANGE, calculateWeaponRange(weaponType));
						stats.Add(range);

						Add(stats);

						// adding abilities
						if ( weaponType.abilities.Length > 0 )
						{
								TextElement abilityHeader = new TextElement();
								abilityHeader.text = "Abilities: ";
								Add(abilityHeader);

								VisualElement abilities = new VisualElement();

								foreach(AbilitySO ability in weaponType.abilities)
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
				/// <param name="armorType">armor which's properties are displayed in the detail view </param>
				public ItemDetailPanel(ArmorTypeSO armorType) : this(armorType, ICON_ON_DEFAULT) { }

				/// <summary>
				/// Creates detail view of an armor piece. 
				/// </summary>
				/// <param name="armorType">armor which's properties are displayed in the detail view </param>
				/// <param name="iconOn">decides whether or not the detail view displays the item's icon </param>
				public ItemDetailPanel(ArmorTypeSO armorType, bool iconOn) : this((ItemTypeSO) armorType, iconOn)
				{
						// adding stats
						VisualElement stats = new VisualElement();

						StatBulletPoint defense = new StatBulletPoint(StatType.DEFENSE, armorType.armor);
						stats.Add(defense);
				}

				#region Doesn't belong here (it is Item properties). 

				/// <summary>
				/// Decides the displayed damage of a weapon. (Here, it's the maximum base damage of its abilities.) 
				/// TODO: Should be placed in WeaponSO (not done yet due to merging issues). 
				/// </summary>
				/// <param name="weaponType">Weapon which's damage is calculated</param>
				/// <returns>Damage of the weapon</returns>
				private int calculateWeaponDamage(WeaponTypeSO weaponType)
				{
						int damage = 0;

						List<TargetedEffect> targetedEffects = new List<TargetedEffect>();

						foreach(AbilitySO ability in weaponType.abilities)
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
				/// <param name="weaponType">Weapon which's range is calculated</param>
				/// <returns>Range of the weapon</returns>
				private int calculateWeaponRange(WeaponTypeSO weaponType)
				{
						int range = 0;
						
						foreach ( AbilitySO ability in weaponType.abilities )
						{
								if ( range < ability.range )
										range = ability.range;
						}

						return range;
				}

				#endregion
		}
}