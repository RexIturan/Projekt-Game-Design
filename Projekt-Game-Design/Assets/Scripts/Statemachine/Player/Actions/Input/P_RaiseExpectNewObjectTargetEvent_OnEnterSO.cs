using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Grid;
using Ability.ScriptableObjects;

[CreateAssetMenu(fileName = "p_RaiseExpectNewObjectTargetEvent_OnEnterSO",
	menuName = "State Machines/Actions/Player/Raise Expect New Object Target Event On Enter")]
public class P_RaiseExpectNewObjectTargetEvent_OnEnterSO : StateActionSO
{
		[Header("Sending Events on: ")]
		[SerializeField] private VoidEventChannelSO expectObjectTargetEvent;

		public override StateAction CreateAction() => new P_RaiseExpectNewObjectTargetEvent_OnEnter(expectObjectTargetEvent);
}

public class P_RaiseExpectNewObjectTargetEvent_OnEnter : StateAction
{
		private VoidEventChannelSO expectObjectTargetEvent;

		public P_RaiseExpectNewObjectTargetEvent_OnEnter(VoidEventChannelSO expectObjectTargetEvent)
		{
				this.expectObjectTargetEvent = expectObjectTargetEvent;
		}

		
		public override void OnStateEnter() {
				expectObjectTargetEvent.RaiseEvent();
		}

		public override void OnUpdate() { }
}