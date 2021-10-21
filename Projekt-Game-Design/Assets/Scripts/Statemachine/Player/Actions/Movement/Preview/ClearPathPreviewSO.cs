using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "ClearPathPreview", menuName = "State Machines/Actions/Player/ClearPathPreview")]
public class ClearPathPreviewSO : StateActionSO
{
    [SerializeField] private VoidEventChannelSO clearPathPreviewEvent;
    public override StateAction CreateAction() => new ClearPathPreview(clearPathPreviewEvent);
}

public class ClearPathPreview : StateAction
{
    private readonly VoidEventChannelSO _clearPathPreviewEvent;

    public ClearPathPreview(VoidEventChannelSO clearPathPreviewEvent)
    {
        this._clearPathPreviewEvent = clearPathPreviewEvent;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {

    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateExit()
    {
        // Debug.Log("Clearing reachable tiles.");
        _clearPathPreviewEvent.RaiseEvent();
    }
}
