using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "p_ClearPreviewDamage_OnExit", menuName = "State Machines/Actions/Player/Clear Preview Damage On Exit")]
public class P_ClearPreviewDamage_OnExitSO : StateActionSO
{
		[Header("Sending events on ")]
		[SerializeField] private VoidEventChannelSO clearPreviewEvent;

		public override StateAction CreateAction() => new P_ClearPreviewDamage_OnExit(clearPreviewEvent);
}

public class P_ClearPreviewDamage_OnExit : StateAction
{
		private VoidEventChannelSO _clearPreviewEvent;
		
		public P_ClearPreviewDamage_OnExit(VoidEventChannelSO clearPreviewEvent)
		{
				_clearPreviewEvent = clearPreviewEvent;
		}

		public override void OnUpdate() { }

		public override void OnStateEnter() { }

		public override void OnStateExit() {
				_clearPreviewEvent.RaiseEvent();
		}
}