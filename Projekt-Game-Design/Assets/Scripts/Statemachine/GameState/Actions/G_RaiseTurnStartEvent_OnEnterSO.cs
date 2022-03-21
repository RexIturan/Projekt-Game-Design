using Characters.Types;
using Events.ScriptableObjects.GameState;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_RaiseTurnStartEvent_OnEnterSO", menuName = "State Machines/Actions/GameState/Raise Turn Start Event On Enter")]
public class G_RaiseTurnStartEvent_OnEnterSO : StateActionSO
{
		[Header("Sending Events On")]
		[SerializeField] private EFactionEventChannelSO startTurnEvent;

		[SerializeField] private Faction faction;

		public override StateAction CreateAction() => new G_RaiseTurnStartEvent_OnEnter(startTurnEvent, faction);
}

public class G_RaiseTurnStartEvent_OnEnter : StateAction
{
		protected new G_RaiseTurnStartEvent_OnEnterSO OriginSO => ( G_RaiseTurnStartEvent_OnEnterSO )base.OriginSO;

		private readonly EFactionEventChannelSO startTurnEvent;
		private Faction faction;

		public G_RaiseTurnStartEvent_OnEnter(EFactionEventChannelSO startTurnEvent, Faction faction)
		{
				this.startTurnEvent = startTurnEvent;
				this.faction = faction;
		}

		public override void Awake(StateMachine stateMachine) { }

		public override void OnUpdate() { }

		public override void OnStateEnter()
		{
				startTurnEvent.RaiseEvent(faction);
		}

		public override void OnStateExit() { }
}