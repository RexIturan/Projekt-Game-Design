using Characters.Ability;
using Events.ScriptableObjects;
using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_UpdateWorldObjects_OnExit", menuName = "State Machines/Actions/Character/Update WorldObjects On Exit")]
public class C_UpdateWorldObjects_OnExitSO : StateActionSO
{
		[Header("Sending Events On")]
		[SerializeField] private VoidEventChannelSO updateWorldObjectEvent;

		public override StateAction CreateAction() => new C_UpdateWorldObjects_OnExit(updateWorldObjectEvent);
}

public class C_UpdateWorldObjects_OnExit : StateAction
{
		private readonly VoidEventChannelSO _updateWorldObjectsEvent;
		private StateMachine _stateMachine;

		public C_UpdateWorldObjects_OnExit(VoidEventChannelSO updateWorldObjectsEvent)
		{
				_updateWorldObjectsEvent = updateWorldObjectsEvent;
		}

		public override void Awake(StateMachine stateMachine)
		{
				_stateMachine = stateMachine;
		}

		public override void OnUpdate() { }

		public override void OnStateExit()
		{
				_updateWorldObjectsEvent.RaiseEvent();
		}
}