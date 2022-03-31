using Characters.Types;
using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_HandleTileEffects_OnEnter", menuName = "State Machines/Actions/GameState/Handle Tile Effects On Enter")]
public class G_HandleTileEffects_OnEnterSO : StateActionSO
{
		[SerializeField] private Faction onFactionsTurn;
		[SerializeField] private VoidEventChannelSO handleTileEffectsEC;

		public override StateAction CreateAction() => new G_HandleTileEffects_OnEnter(handleTileEffectsEC, onFactionsTurn);
}

public class G_HandleTileEffects_OnEnter : StateAction
{
		private VoidEventChannelSO handleTileEffectsEC;
		private Faction onFactionsTurn;

		private GameSC _gameSC;

		public override void Awake(StateMachine stateMachine)
		{
				_gameSC = stateMachine.GetComponent<GameSC>();
		}

		public G_HandleTileEffects_OnEnter(VoidEventChannelSO handleTileEffectsEC, Faction onFactionsTurn)
		{
				this.handleTileEffectsEC = handleTileEffectsEC;
				this.onFactionsTurn = onFactionsTurn;
		}

		public override void OnUpdate() { }

		public override void OnStateEnter()
		{
				if ( _gameSC.CurrentPlayer.Equals(onFactionsTurn) )
						handleTileEffectsEC.RaiseEvent();
		}

		public override void OnStateExit() { }
}