using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

// Sets Variable in GameObject to false and raises unselected Event
//
[CreateAssetMenu(fileName = "DeselectOnEnter_player", menuName = "State Machines/Actions/Player/DeselectOnEnter")]
public class DeselectOnEnter_playerSO : StateActionSO {
    [Header("Sending Events On")] [SerializeField]
    private GameObjEventChannelSO deselectEvent;

    public override StateAction CreateAction() => new DeselectOnEnter_player(deselectEvent);
}

public class DeselectOnEnter_player : StateAction {
    private GameObjEventChannelSO deselectEvent;
    private StateMachine stateMachine;

    public DeselectOnEnter_player(GameObjEventChannelSO deselectEvent) {
        this.deselectEvent = deselectEvent;
    }

    public override void Awake(StateMachine stateMachine) {
        this.stateMachine = stateMachine;
    }
    
    public override void OnUpdate() { }

    public override void OnStateEnter() {
        stateMachine.gameObject.GetComponent<PlayerCharacterSC>().isSelected = false;
        deselectEvent.RaiseEvent(stateMachine.gameObject);
    }
}