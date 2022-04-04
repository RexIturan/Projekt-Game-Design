using Events.ScriptableObjects;
using GameManager;
using GDP01._Gameplay.Provider;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_PanCameraToFirstPlayer_OnExit", menuName = "State Machines/Actions/GameState/Pan Camera To First Player_On Exit")]
public class G_PanCameraToFirstPlayer_OnExitSO : StateActionSO
{
		[SerializeField] private Vector3EventChannelSO panCameraEC;

		public override StateAction CreateAction() => new G_PanCameraToFirstPlayer_OnExit(panCameraEC);
}

public class G_PanCameraToFirstPlayer_OnExit : StateAction
{
		private Vector3EventChannelSO panCameraEC;

		public G_PanCameraToFirstPlayer_OnExit(Vector3EventChannelSO panCameraEC) {
				this.panCameraEC = panCameraEC;
		}

		public override void OnUpdate() { }

		public override void OnStateEnter() { }

		public override void OnStateExit() {
				panCameraEC.RaiseEvent(GameplayProvider.Current.CharacterManager.GetFirstPlayerCharacter().transform.position);
		}
}