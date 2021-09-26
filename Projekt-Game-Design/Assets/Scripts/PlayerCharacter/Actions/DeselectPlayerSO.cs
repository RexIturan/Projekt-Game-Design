using Events.ScriptableObjects;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

// Sets Variable in GameObject to false and raises unselected Event
//
[CreateAssetMenu(fileName = "DeselectPlayer", menuName = "State Machines/Actions/Player/DeselectPlayer")]
public class DeselectPlayerSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private GameObjEventChannelSO deselectEvent;

    protected override StateAction CreateAction() => new DeselectPlayer(deselectEvent);
}

public class DeselectPlayer : StateAction
{
    private StateMachine stateMachine;
    private GameObjEventChannelSO deselectEvent;

    public DeselectPlayer(GameObjEventChannelSO deselectEvent)
    {
        this.deselectEvent = deselectEvent;
    }

    public override void OnUpdate()
    {
    }

    public override void Awake(StateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
    }

    public override void OnStateExit()
    {
        stateMachine.gameObject.GetComponent<PlayerCharacterSC>().IsSelected = false;
        deselectEvent.RaiseEvent(stateMachine.gameObject);
    }
}
