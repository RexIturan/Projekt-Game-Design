using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_RaiseAbilitySelectedEvent_OnEnter", menuName = "State Machines/Actions/Player/Raise Ability Selected Event On Enter")]
public class P_RaiseAbilitySelectedEvent_OnEnterSO : StateActionSO
{
		[Header("Sending Events on: ")]
		[SerializeField] private VoidEventChannelSO abilitySelectedEvent;

		public override StateAction CreateAction() => new P_RaiseAbilitySelectedEvent_OnEnter(abilitySelectedEvent);
}

public class P_RaiseAbilitySelectedEvent_OnEnter : StateAction
{
		private VoidEventChannelSO abilitySelectedEvent;

		public P_RaiseAbilitySelectedEvent_OnEnter(VoidEventChannelSO abilitySelectedEvent)
		{
				this.abilitySelectedEvent = abilitySelectedEvent;
		}

		
		public override void OnStateEnter() {
				abilitySelectedEvent.RaiseEvent();
		}

		public override void OnUpdate() { }
}