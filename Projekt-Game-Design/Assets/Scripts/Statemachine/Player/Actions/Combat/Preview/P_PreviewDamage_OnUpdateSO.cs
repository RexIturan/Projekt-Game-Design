using Characters;
using Combat;
using Input;
using System;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using GDP01.Characters.Component;
using GDP01.World.Components;
using Visual.Healthbar;

[CreateAssetMenu(fileName = "p_PreviewDamage_OnUpdate", menuName = "State Machines/Actions/Player/Preview Damage On Update")]
public class P_PreviewDamage_OnUpdateSO : StateActionSO
{
		[Header("Sending and receiving events on ")]
		[SerializeField] private VoidEventChannelSO clearPreviewEvent;

		[SerializeField] private InputCache inputCache;

		public override StateAction CreateAction() => new P_PreviewDamage_OnUpdate(clearPreviewEvent, inputCache);
}

public class P_PreviewDamage_OnUpdate : StateAction
{
		private VoidEventChannelSO _clearPreviewEvent;
		private InputCache _inputCache;

		private Attacker _attacker;
		private Statistics _statistics;
		private AbilityController _abilityController;

		private Vector3Int? lastTargetPos;

		public P_PreviewDamage_OnUpdate(VoidEventChannelSO clearPreviewEvent, InputCache inputCache)
		{
				_clearPreviewEvent = clearPreviewEvent;
				_inputCache = inputCache;
		}

		/// <summary>
		/// Sets the last target pos vector to null, 
		/// so it is known when a preview is shown and when it isn't. 
		/// </summary>
		private void ClearLastTargetPos()
		{
				lastTargetPos = null;
		}

		public override void Awake(StateMachine stateMachine)
		{
				_attacker = stateMachine.gameObject.GetComponent<Attacker>();
				_statistics = stateMachine.gameObject.GetComponent<Statistics>();
				_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();

				_clearPreviewEvent.OnEventRaised += ClearLastTargetPos;
		}

		public override void OnUpdate() {
				AbilitySO ability = _abilityController.GetSelectedAbility();

				Vector3Int mousePos = _inputCache.cursor.abovePos.gridPos;
				Vector3Int targetPos = _abilityController.singleTarget ? _abilityController.singleTargetPos : mousePos;
				
				// only draw new preview if the already drawn preview doesn't have the same grid position 
				if(!targetPos.Equals(lastTargetPos)) {
						_clearPreviewEvent.RaiseEvent();
						lastTargetPos = targetPos;

						bool isInRange = false;

						for ( int i = 0; !isInRange && i < _attacker.tilesInRange.Count; i++ ) {
								if ( _attacker.tilesInRange[i].pos.Equals(targetPos) ) {
										isInRange = true;
								}
						}

						// only draw the preview if the target position is in range and the ability has effects
						if ( isInRange && ability != null && ability.targetedEffects != null ) {
								// get cumulated damage and display preview
								foreach ( Tuple<Targetable, int> targetDamagePair in CombatUtils.GetCumulatedDamage(targetPos, ability, _attacker)) {
										HealthbarController healthbar = targetDamagePair.Item1.GetComponentInChildren<HealthbarController>();
										if ( healthbar ) {
												healthbar.SetPreviewValue(-targetDamagePair.Item2);
										}
										else {
												Debug.Log("Could not find healthbar of object. ");
										}
								}
						}
				}
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
}