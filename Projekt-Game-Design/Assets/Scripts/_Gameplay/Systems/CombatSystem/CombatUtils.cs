using System.Collections.Generic;
using System.Linq;
using Ability;
using Characters;
using Characters.Types;
using GDP01.World.Components;
using UnityEngine;

namespace Combat
{
		public class CombatUtils : MonoBehaviour
		{
				/**
				 * calculates the damage/healing inflicted
				 */
				public static int CalculateDamage(Effect effect, StatusValues stats)
				{
						int effectDamage = effect.baseDamage +
															 ( int )( effect.strengthBonus * stats.Strength.value ) +
															 ( int )( effect.dexterityBonus * stats.Dexterity.value ) +
															 ( int )( effect.intelligenceBonus * stats.Intelligence.value );

						if ( effect.type == DamageType.Healing )
								effectDamage *= -1;

						return effectDamage;
				}

				public static List<Targetable> FindAllTargets(
					List<Vector3Int> targetPositons,
					Attacker attacker, TargetRelationship targetRelationshipTypes) {
					List<Targetable> targets = new List<Targetable>();
					
					// characters

					Faction attackerFaction = attacker.gameObject.GetComponent<Statistics>().GetFaction();

					List<Faction> factions = targetRelationshipTypes.GetTargetedFactions(attackerFaction);

					targets.AddRange(GetTargets(factions, targetPositons));
					
					// add self
					if ( targetRelationshipTypes.HasFlag(TargetRelationship.Self)) {
						var selfTarget = attacker.gameObject.GetComponent<Targetable>();
						if (! targets.Contains(selfTarget)) {
							targets.Add(selfTarget);	
						}
					}

					targets = Targetable.GetTargetsWithPositions(targets, targetPositons);
					
					return targets;
				}

				private static List<Targetable> GetTargets(List<Faction> factions, List<Vector3Int> positions) {

					List<Targetable> targets = GetAllTargetables().ToList();
					targets = GetTargetsWithFaction(targets, factions);
					targets = Targetable.GetTargetsWithPositions(targets, positions);

					return targets;
				}
				
				private static Targetable[] GetAllTargetables() {
					return Targetable.GetAllInstances();
				}
				
				private static List<Targetable> GetTargetsWithFaction(
					List<Targetable> targetables, List<Faction> targetedFactions) {
					
					return targetables.FindAll(target => {
						var stats = target.GetComponent<Statistics>();
						if(stats is null) return false;

						return targetedFactions.Contains(stats.Faction);
					});
				}
		}
}