using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

// Sets Variable in GameObject to false and raises unselected Event
//
[CreateAssetMenu(fileName = "DeselectPlayer", menuName = "State Machines/Actions/Player/DeselectPlayer")]
public class DeselectPlayerSO : StateActionSO {
    [Header("Sending Events On")] [SerializeField]
    private GameObjEventChannelSO deselectEvent;

    public override StateAction CreateAction() => new DeselectPlayer(deselectEvent);
}

public class DeselectPlayer : StateAction {
    private GameObjEventChannelSO deselectEvent;
    private StateMachine stateMachine;

    public DeselectPlayer(GameObjEventChannelSO deselectEvent) {
        this.deselectEvent = deselectEvent;
    }

    public override void OnUpdate() { }

    public override void Awake(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }

    public override void OnStateExit() {
        stateMachine.gameObject.GetComponent<PlayerCharacterSC>().isSelected = false;
        deselectEvent.RaiseEvent(stateMachine.gameObject);
    }
}