using Ability.ScriptableObjects;
using Audio;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_StopAllSounds_OnEnter", menuName = "State Machines/Actions/GameState/Stop All Sounds On Enter")]
public class G_StopAllSounds_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new G_StopAllSounds_OnEnter();
}

public class G_StopAllSounds_OnEnter : StateAction {
		public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
				SoundManager.FindSoundManager().StopAllSounds();
    }

    public override void OnStateExit() { }
}
