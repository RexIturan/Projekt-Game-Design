using System;
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
															 ( int )( effect.strengthBonus * stats.Strength.Value ) +
															 ( int )( effect.dexterityBonus * stats.Dexterity.Value ) +
															 ( int )( effect.intelligenceBonus * stats.Intelligence.Value );

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

				/// <summary>
				/// Calculates the cumulated damage for every target of an ability. 
				/// First component of tuples are the targetables, and the second component is the corresponding damage 
				/// (sum of all targeted effects). 
				/// </summary>
				/// <param name="targetPos">The position the ability is casted </param>
				/// <param name="ability">Ability that is casted </param>
				/// <param name="attacker">Attacker component of character casting the ability </param>
				/// <returns></returns>
				public static List<Tuple<Targetable, int>> GetCumulatedDamage(Vector3Int targetPos, AbilitySO ability, Attacker attacker) {
					Statistics statistics = attacker.GetComponent<Statistics>();
					
					List<Tuple<Targetable, int>> targetDamagePairs = new List<Tuple<Targetable, int>>();
					List<Tuple<Targetable, int>> cumulatedDamagePairs = new List<Tuple<Targetable, int>>();
					HashSet<Targetable> allTargets = new HashSet<Targetable>();

					// rotations of pattern depending on the angle the attacker is facing
					int rotations = attacker.GetRotationsToTarget(targetPos);
					
					// calculate damage for each target for each targeted effects
					foreach ( TargetedEffect targetedEffect in ability.targetedEffects ) {
						int damage = CalculateDamage(targetedEffect.effect, statistics.StatusValues);

						List<Targetable> targets = FindAllTargets(
								targetedEffect.area.GetTargetedTiles(targetPos, rotations),
								attacker, targetedEffect.targets);

						foreach ( Targetable target in targets ) {
							targetDamagePairs.Add(new Tuple<Targetable, int>(target, damage));
							allTargets.Add(target);
						}
					}

					// cumulate damage for targetables
					foreach ( Targetable target in allTargets ) {
						int cumulatedDamage = 0;

						targetDamagePairs.ForEach(pair => {
							if ( pair.Item1.Equals(target) )
								cumulatedDamage += pair.Item2;
						});
						
						cumulatedDamagePairs.Add(new Tuple<Targetable, int>(target, cumulatedDamage));
					}

					return cumulatedDamagePairs;
				}
		}
}