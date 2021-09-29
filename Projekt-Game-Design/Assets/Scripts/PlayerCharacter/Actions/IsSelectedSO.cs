using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsSelected", menuName = "State Machines/Actions/IsSelected")]
public class IsSelectedSO : StateActionSO {
    [Header("Sending Events On")] 
    [SerializeField] private BoolEventChannelSO setActionMenuVisibility;
    [SerializeField] private GameObjEventChannelSO addPlayerToSelection;

    public override StateAction CreateAction() => new IsSelected(setActionMenuVisibility, addPlayerToSelection);
}

public class IsSelected : StateAction {
    protected new IsSelectedSO OriginSO => (IsSelectedSO) base.OriginSO;
    
    private GameObjEventChannelSO addPlayerToSelection;
    private BoolEventChannelSO setActionMenuVisibility;

    private GameObject gameObject;
    private MeshRenderer render;

    public IsSelected(BoolEventChannelSO evChannel, GameObjEventChannelSO gameObjEventChannel) {
        setActionMenuVisibility = evChannel;
        addPlayerToSelection = gameObjEventChannel;
    }

    public override void OnUpdate() { }

    public override void Awake(StateMachine stateMachine) {
        render = stateMachine.gameObject.GetComponent<MeshRenderer>();
        gameObject = stateMachine.gameObject;
    }

    public override void OnStateEnter() {
        Debug.Log("Ich bin selected");
        render.material.color = Color.magenta;
        setActionMenuVisibility.RaiseEvent(true);
        addPlayerToSelection.RaiseEvent(gameObject);
    }
}