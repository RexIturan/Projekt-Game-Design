using Ability.ScriptableObjects;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_InitialiseAbilities_OnEnter", menuName = "State Machines/Actions/GameState/Initialise Abilities On Enter")]
public class G_InitialiseAbilities_OnEnterSO : StateActionSO {
		[SerializeField] private AbilityContainerSO abilityContainer;
    public override StateAction CreateAction() => new G_InitialiseAbilities_OnEnter(abilityContainer);
}

public class G_InitialiseAbilities_OnEnter : StateAction {
    private AbilityContainerSO _abilityContainer;
    
    public G_InitialiseAbilities_OnEnter(AbilityContainerSO abilityContainer) {
        this._abilityContainer = abilityContainer;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
				_abilityContainer.InitAbilities();
    }

    public override void OnStateExit() { }
}
