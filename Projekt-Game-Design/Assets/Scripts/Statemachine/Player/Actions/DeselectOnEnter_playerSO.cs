using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

// Sets Variable in GameObject to false and raises unselected Event
//
[CreateAssetMenu(fileName = "DeselectOnEnter_player", menuName = "State Machines/Actions/Player/DeselectOnEnter")]
public class DeselectOnEnter_PlayerSO : StateActionSO {
    [Header("Sending Events On")] [SerializeField]
    private GameObjEventChannelSO deselectEvent;

    public override StateAction CreateAction() => new DeselectOnEnter_Player(deselectEvent);
}

public class DeselectOnEnter_Player : StateAction {
    private GameObjEventChannelSO _deselectEvent;
    private StateMachine _stateMachine;

    public DeselectOnEnter_Player(GameObjEventChannelSO deselectEvent) {
        this._deselectEvent = deselectEvent;
    }

    public override void Awake(StateMachine stateMachine) {
        this._stateMachine = stateMachine;
    }
    
    public override void OnUpdate() { }

    public override void OnStateEnter() {
	    var obj = _stateMachine.gameObject;
	    var playerSC = obj.GetComponent<PlayerCharacterSC>(); 
        playerSC.isSelected = false;
        playerSC.abilityConfirmed = false;
        playerSC.abilitySelected = false;
        _deselectEvent.RaiseEvent(obj);
    }
}