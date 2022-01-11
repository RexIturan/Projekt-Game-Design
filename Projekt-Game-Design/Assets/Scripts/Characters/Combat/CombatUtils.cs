using Ability;
using Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WorldObjects;

namespace Combat
{
		public class CombatUtils : MonoBehaviour
		{
				[SerializeField] private CharacterList characters;

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

				/**
				 * Finds all characters/world objects, that are appliable to the ability target type 
				 * and that are within the pattern at given position
				 * 
				 * NOTE: Doesn't work on world objects yet!!!
				 * NOTE: Y coordinates don't matter here (which makes sense since patterns are 2D)
				 */
				public static HashSet<Targetable> FindAllTargets(Vector3Int groundTargetGridPos, 
						bool[][] pattern, Vector2Int patternAnchor, 
						Attacker attacker, AbilityTarget targetTypes)
				{
						HashSet<Targetable> targets = new HashSet<Targetable>();

						HashSet<Targetable> possibleTargets = new HashSet<Targetable>();

						// characters
						//
						CharacterList characterList = CharacterList.FindInstant();
						if ( characterList )
						{
								Faction attackerFaction = attacker.gameObject.GetComponent<Statistics>().GetFaction();

								// cases in which player characters are targeted
								if(attackerFaction.Equals(Faction.Player) && targetTypes.HasFlag(AbilityTarget.Ally) || 
										attackerFaction.Equals(Faction.Enemy) && targetTypes.HasFlag(AbilityTarget.Enemy))
								{
										foreach(GameObject player in characterList.playerContainer)
										{
												if(!player.Equals(attacker.gameObject))
												{
														possibleTargets.Add(player.GetComponent<Targetable>());
												}
										}
								}

								// cases in which enemy characters are targeted
								if ( attackerFaction.Equals(Faction.Player) && targetTypes.HasFlag(AbilityTarget.Enemy) ||
										attackerFaction.Equals(Faction.Enemy) && targetTypes.HasFlag(AbilityTarget.Ally) )
								{
										foreach ( GameObject enemy in characterList.enemyContainer )
										{
												if ( !enemy.Equals(attacker.gameObject) )
												{
														possibleTargets.Add(enemy.GetComponent<Targetable>());
												}
										}
								}
						}

						// add self
						if ( targetTypes.HasFlag(AbilityTarget.Self) )
						{
								possibleTargets.Add(attacker.gameObject.GetComponent<Targetable>());
						}

						// world objects
						//
						WorldObjectList worldObjectList = WorldObjectList.FindInstant();
						if(worldObjectList && targetTypes.HasFlag(AbilityTarget.Neutral))
						{
								foreach(GameObject doorObj in worldObjectList.doors)
								{
										Targetable doorTarget = doorObj.GetComponent<Targetable>();
										if ( doorTarget )
												possibleTargets.Add(doorTarget);
								}

								foreach ( GameObject junkObj in worldObjectList.junks )
								{
										Targetable junkTarget = junkObj.GetComponent<Targetable>();
										if ( junkTarget )
												possibleTargets.Add(junkTarget);
								}
						}

						// add each target that is in the pattern
						foreach (Targetable target in possibleTargets)
						{
								// (sorry about bad comment) The Anchor point should correspond to the ground target 
								// The difference between the position of the ground target and the position of the character 
								// is the same as the difference between the anchor point and the character within the pattern
								// GroundTargetPos - CharTargetPos = AnchorPos - PatternCoords
								// Therefore: PatternCoords = AnchorPos + CharTargetPos - GroundTargetPos
								Vector3Int targetPos = target.gameObject.GetComponent<GridTransform>().gridPosition;
								int patternX = patternAnchor.x + (targetPos.x - groundTargetGridPos.x);
								int patternY = patternAnchor.y + (targetPos.z - groundTargetGridPos.z);

								if ( patternX >= 0 && patternY >= 0 &&
									 patternX < pattern.Length && patternY < pattern[patternX].Length &&
									 pattern[patternX][patternY] )
										targets.Add(target);
						}

						return targets;
				}
		}
}