using Ability.ScriptableObjects;
using Audio;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_PlayTrack_OnEnter", menuName = "State Machines/Actions/GameState/Play Track On Enter")]
public class G_PlayTrack_OnEnterSO : StateActionSO {
	//todo use SO instead 
		[SerializeField] private string soundName;

    public override StateAction CreateAction() => new G_PlayTrack_OnEnter(soundName);
}

public class G_PlayTrack_OnEnter : StateAction {
    private string soundName;

		public G_PlayTrack_OnEnter(string soundName) {
				this.soundName = soundName;
		}

		public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
	    //todo use event Else this will be a error source
				// AudioManager.FindSoundManager().PlaySound(soundName);
    }

    public override void OnStateExit() { }
}
