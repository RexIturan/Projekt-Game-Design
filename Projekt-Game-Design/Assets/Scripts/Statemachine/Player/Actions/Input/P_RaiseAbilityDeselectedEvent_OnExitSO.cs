using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_RaiseAbilityDeselectedEvent_OnExit", menuName = "State Machines/Actions/Player/Raise Ability Deselected Event On Exit")]
public class P_RaiseAbilityDeselectedEvent_OnExitSO : StateActionSO
{
		[Header("Sending Events on: ")]
		[SerializeField] private VoidEventChannelSO abilityDeselectedEvent;

		public override StateAction CreateAction() => new P_RaiseAbilityDeselectedEvent_OnExit(abilityDeselectedEvent);
}

public class P_RaiseAbilityDeselectedEvent_OnExit : StateAction
{
		private VoidEventChannelSO abilityDeselectedEvent;

		public P_RaiseAbilityDeselectedEvent_OnExit(VoidEventChannelSO abilityDeselectedEvent)
		{
				this.abilityDeselectedEvent = abilityDeselectedEvent;
		}

		
		public override void OnStateExit() {
				abilityDeselectedEvent.RaiseEvent();
		}

		public override void OnUpdate() { }
}