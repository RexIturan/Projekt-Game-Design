using Audio;
using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "b_PlaySound_OnEnter/Exit", menuName = "State Machines/Actions/Basic/Play Sound On Enter Or Exit")]
public class B_PlaySound_SO : StateActionSO {
		[Header("Sending Events On:")]
		[SerializeField] private SoundSO sound;
		[SerializeField] private SoundEventChannelSO playSoundEC;
		[SerializeField] private bool onEnter;
		[SerializeField] private bool onExit;

		public override StateAction CreateAction() => new B_PlaySound(sound, playSoundEC, onEnter, onExit);
}

public class B_PlaySound : StateAction {
		private SoundSO sound;
		private SoundEventChannelSO playSoundEC;
		private bool onEnter;
		private bool onExit;

		public B_PlaySound(SoundSO sound, SoundEventChannelSO playSoundEC, bool onEnter, bool onExit) {
				this.sound = sound;
				this.playSoundEC = playSoundEC;
				this.onEnter = onEnter;
				this.onExit = onExit;
		}

    public override void OnUpdate() { }

    public override void OnStateEnter() {
				if ( onEnter )
						playSoundEC.RaiseEvent(sound);
    }

    public override void OnStateExit() {
				if ( onExit )
						playSoundEC.RaiseEvent(sound);
		}
}
