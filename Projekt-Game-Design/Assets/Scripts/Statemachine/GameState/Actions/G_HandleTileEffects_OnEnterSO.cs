using GameManager;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_HandleTileEffects_OnEnter", menuName = "State Machines/Actions/GameState/Handle Tile Effects On Enter")]
public class G_HandleTileEffects_OnEnterSO : StateActionSO
{
		[SerializeField] private VoidEventChannelSO handleTileEffectsEC;

		public override StateAction CreateAction() => new G_HandleTileEffects_OnEnter(handleTileEffectsEC);
}

public class G_HandleTileEffects_OnEnter : StateAction
{
		private VoidEventChannelSO handleTileEffectsEC;

		public G_HandleTileEffects_OnEnter(VoidEventChannelSO handleTileEffectsEC) {
				this.handleTileEffectsEC = handleTileEffectsEC;
		}

		public override void OnUpdate() { }

		public override void OnStateEnter() {
				handleTileEffectsEC.RaiseEvent();
		}

		public override void OnStateExit() { }
}