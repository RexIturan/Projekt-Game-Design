using System;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Events.ScriptableObjects;

[CreateAssetMenu(fileName = "NewPlayerSelected", menuName = "State Machines/Conditions/Player/NewPlayerSelected")]
public class NewPlayerSelectedSO : StateConditionSO
{
    [SerializeField] private GameObjActionEventChannelSO NewPlayerSelectedEvent;

    protected override Condition CreateCondition() => new NewPlayerSelected(NewPlayerSelectedEvent);
}

public class NewPlayerSelected : Condition
{
    private GameObject thisPlayer;
    private GameObjActionEventChannelSO selectEvent;

    private bool selectedPlayerIsDifferent;

    public NewPlayerSelected(GameObjActionEventChannelSO newPlayerSelectedEvent)
    {
        selectEvent = newPlayerSelectedEvent;
    }

    public override void Awake(StateMachine stateMachine)
    {
        thisPlayer = stateMachine.gameObject;

        selectEvent.OnEventRaised += OnNewPlayerSelectedEvent;
    }

    protected override bool Statement()
    {
        return selectedPlayerIsDifferent;
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    private void OnNewPlayerSelectedEvent(GameObject obj, Action<int> action)
    {
        selectedPlayerIsDifferent = !obj.Equals(thisPlayer);
    }
}
