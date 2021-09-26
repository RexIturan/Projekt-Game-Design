using Events.ScriptableObjects;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

// Sets Variable in GameObject to false and raises unselected Event
//
[CreateAssetMenu(fileName = "UnselectPlayer", menuName = "State Machines/Actions/Player/UnselectPlayer")]
public class UnselectPlayerSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private GameObjEventChannelSO unselectEvent;

    protected override StateAction CreateAction() => new UnselectPlayer(unselectEvent);
}

public class UnselectPlayer : StateAction
{
    private StateMachine stateMachine;
    private GameObjEventChannelSO unselectEvent;

    public UnselectPlayer(GameObjEventChannelSO unselectEvent)
    {
        this.unselectEvent = unselectEvent;
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
        stateMachine.gameObject.GetComponent<PlayerCharacterCO>().isSelected = false;
        unselectEvent.RaiseEvent(stateMachine.gameObject);
    }
}
