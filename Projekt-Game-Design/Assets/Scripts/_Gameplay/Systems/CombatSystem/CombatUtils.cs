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
		public static class CombatUtils
		{
				/// <summary>
				/// Calculates the inflicted damage by an attacker towards a target. 
				/// </summary>
				/// <param name="effect"></param>
				/// <param name="statsAttacker"></param>
				/// <returns></returns>
				public static int CalculateDamage(Effect effect, Attacker attacker, Targetable target)
				{
						Statistics targetStatistics = target.GetComponent<Statistics>();
						Statistics attackerStatistics = attacker.GetComponent<Statistics>();

						StatusValues statsAttacker = attackerStatistics.StatusValues;

						float effectDamage = effect.baseDamage +
																	( effect.strengthBonus * statsAttacker.Strength.Value ) +
																	( effect.dexterityBonus * statsAttacker.Dexterity.Value ) +
																	( effect.intelligenceBonus * statsAttacker.Intelligence.Value );

						if ( effect.type == DamageType.Healing )
								effectDamage *= -1;

						// type multiplication
						effectDamage = effectDamage * DamageTable.GetFactorForDamageAndArmor(effect.type, targetStatistics.ArmorType);

						return Mathf.FloorToInt(effectDamage);
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

					// Remove dead from targets
					targets.RemoveAll(target => target.IsDead);

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
						List<Targetable> targets = FindAllTargets(
								targetedEffect.area.GetTargetedTiles(targetPos, rotations),
								attacker, targetedEffect.targets);

						foreach ( Targetable target in targets ) {
							int damage = CalculateDamage(targetedEffect.effect, attacker, target);

							// TODO: is it ok to say dead people receive no damage? 
							if(!target.IsDead) { 
								targetDamagePairs.Add(new Tuple<Targetable, int>(target, damage));
								allTargets.Add(target);
							}
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

				/// <summary>
				/// Calculates the total damage an ability would deal upon a given faction. 
				/// Range of health is not considered, so 30 points of damage against a character with 1 point of health 
				/// will still be count as 30 points of damage. 
				/// </summary>
				public static int GetCumulatedDamageOnFaction(Vector3Int targetPos, AbilitySO ability, Attacker attacker, Faction faction) {
						return GetCumulatedDamageOnFaction(targetPos, ability, attacker, faction, false);
				}

				/// <summary>
				/// Calculates the total damage an ability would deal upon a given faction. 
				/// </summary>
				/// <param name="actual">Defines whether range of health should be considered, 
				/// i.e. if flag is set, 30 points of damage against a character with 1 point of health will be count as 1 point of damage, 
				/// and if the flag is not set, it will be count as 30 points of damage </param>
				public static int GetCumulatedDamageOnFaction(Vector3Int targetPos, AbilitySO ability, Attacker attacker, Faction faction, bool actual) {
						List<Tuple<Targetable, int>> targetDamagePairs = GetCumulatedDamage(targetPos, ability, attacker);
						int totalDamage = 0;
						foreach(Tuple<Targetable, int> targetDamagePair in targetDamagePairs) {
								Statistics targetableStats = targetDamagePair.Item1.GetComponent<Statistics>();

								if ( targetableStats && targetableStats.Faction.Equals(faction) ) { 
										if(!actual)
												totalDamage += targetDamagePair.Item2;
										else {
												int actualDamage = 0;
												if ( targetDamagePair.Item2 > 0 ) { 
														actualDamage = Mathf.Min(targetableStats.StatusValues.HitPoints.Value - targetableStats.StatusValues.HitPoints.Min, 
																targetDamagePair.Item2);
												}
												else if ( targetDamagePair.Item2 < 0 ) {
														actualDamage = Mathf.Max(targetableStats.StatusValues.HitPoints.Value - targetableStats.StatusValues.HitPoints.Max,
																targetDamagePair.Item2);
												}

												totalDamage += actualDamage;
										}
								}
						}
						return totalDamage;
				}
		}
}