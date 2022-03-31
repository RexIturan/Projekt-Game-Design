using Ability.ScriptableObjects;
using Audio;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_StopAllSounds_OnEnter", menuName = "State Machines/Actions/GameState/Stop All Sounds On Enter")]
public class G_StopAllSounds_OnEnterSO : StateActionSO {
		[Header("Sending Events On:")]
		[SerializeField] private VoidEventChannelSO stopAllSoundsEC;

    public override StateAction CreateAction() => new G_StopAllSounds_OnEnter(stopAllSoundsEC);
}

public class G_StopAllSounds_OnEnter : StateAction {
		private VoidEventChannelSO stopAllSoundsEC;

		public G_StopAllSounds_OnEnter(VoidEventChannelSO stopAllSoundsEC) {
				this.stopAllSoundsEC = stopAllSoundsEC;
		}

		public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
				stopAllSoundsEC.RaiseEvent();
    }

    public override void OnStateExit() { }
}
