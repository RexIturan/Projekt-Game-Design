using System.Collections.Generic;
using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_SuperAction", menuName = "State Machines/Actions/Enemy/e_SuperAction")]
public class e_SuperActionSO : StateActionSO
{
    [SerializeField] private AbilityPhase abilityPhase;
    [SerializeField] private AbilityContainerSO abilityContainer;
    public override StateAction CreateAction() => new e_SuperAction(abilityPhase, abilityContainer);
}

public class e_SuperAction : StateAction
{
    protected new SuperActionSO OriginSO => (SuperActionSO)base.OriginSO;

    // input for each
    private AbilityContainerSO abilityContainer;
    private AbilityPhase phase;

    // 
    private StateMachine stateMachine;
    private List<StateAction> subActions;
    private int abilityID;
    private EnemyCharacterSC enemyCharacterSC;

    public e_SuperAction(AbilityPhase phase, AbilityContainerSO abilityContainer)
    {
        this.phase = phase;
        this.abilityContainer = abilityContainer;
    }

    public override void Awake(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        enemyCharacterSC = stateMachine.GetComponent<EnemyCharacterSC>();
    }

    public override void OnStateEnter()
    {
        subActions = new List<StateAction>();
        // get Actions from Ability
        abilityID = enemyCharacterSC.abilityID;
        var ability = abilityContainer.abilities[abilityID];
        StateActionSO[] actions;

        if (phase == AbilityPhase.selected)
        {
            actions = ability.selectedActions.ToArray();
        }
        else
        {
            actions = ability.executingActions.ToArray();
        }

        foreach (var actionSo in actions)
        {
            var action = actionSo.CreateAction();
            subActions.Add(action);
        }

        foreach (var action in subActions)
        {
            action.Awake(stateMachine);
            action.OnStateEnter();
        }
    }

    public override void OnUpdate()
    {
        if (subActions != null)
        {
            foreach (var action in subActions)
            {
                action.OnUpdate();
            }
        }
    }

    public override void OnStateExit()
    {
        foreach (var action in subActions)
        {
            action.OnStateExit();
        }
    }

    // enum AbilityPhase {
    // 	selected,
    // 	executing
    // }
}