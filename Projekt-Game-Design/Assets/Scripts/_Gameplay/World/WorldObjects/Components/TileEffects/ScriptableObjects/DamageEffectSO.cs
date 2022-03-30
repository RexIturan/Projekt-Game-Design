using Characters;
using Characters.Types;
using Combat;
using Events.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Target effect that deals damage to certain groups on action. 
		/// </summary>
		[System.Serializable]
		[CreateAssetMenu(fileName = "te_newDamageEffect", menuName = "WorldObjects/TileEffects/Damage Effect")]
    public class DamageEffectSO : TileEffectSO
		{
				[SerializeField] private int damage;
				[SerializeField] private List<Faction> targets;
				[SerializeField] private CreateFloatingTextEventChannelSO createTextEC;

				/// <summary>
				/// Deals damage to the targetable with the same grid transform as the tile effect. 
				/// Damage only targets certain factions. 
				/// </summary>
				/// <param name="tileEffectController">TileEffect component that has this effect </param>
				override public void OnAction(TileEffectController tileEffectController) {
						GridTransform tilePosition = tileEffectController.gameObject.GetComponent<GridTransform>();
						Targetable target = Targetable.GetTargetsWithPosition(tilePosition.gridPosition);
						Faction targetFaction = target ? target.GetComponent<Statistics>().Faction : Faction.None;

						if ( target && targets.Contains(targetFaction) && !target.IsDead ) { 
								target.ReceivesDamage(damage);
								createTextEC.RaiseEvent(damage.ToString(), target.gameObject.transform.position + Vector3.up, Color.red);
						}
				}
    }
}
