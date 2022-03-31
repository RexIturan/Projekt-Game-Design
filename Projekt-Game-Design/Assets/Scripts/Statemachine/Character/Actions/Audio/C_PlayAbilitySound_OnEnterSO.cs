using Audio;
using Events.ScriptableObjects;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_PlayAbilitySound_OnEnter", menuName = "State Machines/Actions/Character/Play Ability Sound On Enter")]
public class C_PlayAbilitySound_OnEnterSO : StateActionSO {
		[Header("Sending Events On:")]
		[SerializeField] private SoundEventChannelSO playSoundEC;

		public override StateAction CreateAction() => new C_PlayAbilitySound_OnEnter(playSoundEC);
}

public class C_PlayAbilitySound_OnEnter : StateAction {
		private SoundEventChannelSO playSoundEC;

		private AbilityController _abilityController;

		public C_PlayAbilitySound_OnEnter(SoundEventChannelSO playSoundEC) {
				this.playSoundEC = playSoundEC;
		}

		public override void Awake(StateMachine stateMachine) {
			_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		}

    public override void OnUpdate() { }

    public override void OnStateEnter() {
				playSoundEC.RaiseEvent(_abilityController.GetSelectedAbility().activationSound);
    }

    public override void OnStateExit() { }
}
