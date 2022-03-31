using Events.ScriptableObjects;
using GDP01;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_AnnouncePhase_OnEnter", menuName = "State Machines/Actions/GameState/Announce Phase On Enter")]
public class G_AnnouncePhase_OnEnterSO : StateActionSO
{
		[Header("Sending Events On")]
		[SerializeField] private GamePhaseEventChannelSO gamePhaseAnnouncementEC;

		[SerializeField] private GamePhase phase;

		public override StateAction CreateAction() => new G_AnnouncePhase_OnEnter(gamePhaseAnnouncementEC, phase);
}

public class G_AnnouncePhase_OnEnter : StateAction
{
		protected new G_RaiseTurnStartEvent_OnEnterSO OriginSO => ( G_RaiseTurnStartEvent_OnEnterSO )base.OriginSO;

		private readonly GamePhaseEventChannelSO gamePhaseAnnouncementEC;
		private readonly GamePhase phase;

		public G_AnnouncePhase_OnEnter(GamePhaseEventChannelSO gamePhaseAnnouncementEC, GamePhase phase)
		{
				this.gamePhaseAnnouncementEC = gamePhaseAnnouncementEC;
				this.phase = phase;
		}

		public override void Awake(StateMachine stateMachine) { }

		public override void OnUpdate() { }

		public override void OnStateEnter()
		{
				gamePhaseAnnouncementEC.RaiseEvent(phase);
		}

		public override void OnStateExit() { }
}