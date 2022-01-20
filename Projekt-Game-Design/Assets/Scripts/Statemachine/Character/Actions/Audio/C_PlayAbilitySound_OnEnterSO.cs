using Audio;
using GDP01.Characters.Component;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "c_PlayAbilitySound_OnEnter", menuName = "State Machines/Actions/Character/Play Ability Sound On Enter")]
public class C_PlayAbilitySound_OnEnterSO : StateActionSO {
    public override StateAction CreateAction() => new C_PlayAbilitySound_OnEnter();
}

public class C_PlayAbilitySound_OnEnter : StateAction {
		private AbilityController _abilityController;

		public override void Awake(StateMachine stateMachine) {
			_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		}

    public override void OnUpdate() { }

    public override void OnStateEnter() {
				AudioManager.FindSoundManager().PlaySound(_abilityController.GetSelectedAbility().activationSound);
    }

    public override void OnStateExit() { }
}
