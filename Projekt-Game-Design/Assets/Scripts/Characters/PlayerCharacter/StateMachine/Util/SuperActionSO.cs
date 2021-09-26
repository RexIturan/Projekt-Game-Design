using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SuperAction", menuName = "State Machines/Actions/Super Action")]
public class SuperActionSO : StateActionSO {
	[SerializeField] private AbilityPhase abilityPhase;
	[SerializeField] private AbilityContainerSO abilityContainer;
	protected override StateAction CreateAction() => new SuperAction(abilityPhase, abilityContainer);
}

public class SuperAction : StateAction
{
	protected new SuperActionSO OriginSO => (SuperActionSO)base.OriginSO;

	// input for each
	private AbilityContainerSO abilityContainer;
	private AbilityPhase phase;
	
	// 
	private StateMachine stateMachine;
	private List<StateAction> subActions;
	private int abilityID;
	private PlayerCharacterStateContainer playerCharacterStateContainer;

	public SuperAction(AbilityPhase phase, AbilityContainerSO abilityContainer) {
		this.phase = phase;
		this.abilityContainer = abilityContainer;
	}
	
	public override void Awake(StateMachine stateMachine) {
		this.stateMachine = stateMachine;
		playerCharacterStateContainer = stateMachine.GetComponent<PlayerCharacterStateContainer>();

	}
	
	public override void OnStateEnter() {
		// get Actions from Ability
		abilityID = playerCharacterStateContainer.abilityID;
		var abiliy = abilityContainer.abilities[abilityID];

		if (phase == AbilityPhase.selected) {
			subActions = ability.selectedActions;
		}
		else {
			subActions = ability.executingActions;
		}

		foreach (var action in subActions) {
			action.Awake(stateMachine);
			action.OnStateEnter();
		}
	}
	
	public override void OnUpdate()
	{
		foreach (var action in subActions) {
			action.OnUpdate();
		}
	}
	
	public override void OnStateExit()
	{
		foreach (var action in subActions) {
			action.OnStateExit();
		}
	}

	// enum AbilityPhase {
	// 	selected,
	// 	executing
	// }
}

public enum AbilityPhase {
	selected,
	executing
}