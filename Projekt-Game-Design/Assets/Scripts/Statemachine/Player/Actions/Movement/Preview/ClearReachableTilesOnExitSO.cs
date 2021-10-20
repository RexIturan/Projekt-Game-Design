using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "ClearReachableTiles", menuName = "State Machines/Actions/Player/ClearReachableTiles")]
public class ClearReachableTilesOnExitSO : StateActionSO
{
    [SerializeField] private VoidEventChannelSO clearReachableTilesEC;
    public override StateAction CreateAction() => new ClearReachableTilesOnExit(clearReachableTilesEC);
}

public class ClearReachableTilesOnExit : StateAction
{
    private VoidEventChannelSO _clearReachableTilesEC;
    
    public ClearReachableTilesOnExit(VoidEventChannelSO clearReachableTilesEC) {
        this._clearReachableTilesEC = clearReachableTilesEC;
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
        _clearReachableTilesEC.RaiseEvent();
    }
}
