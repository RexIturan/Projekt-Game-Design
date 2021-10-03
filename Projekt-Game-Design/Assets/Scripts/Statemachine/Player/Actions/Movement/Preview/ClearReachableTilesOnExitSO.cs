using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Pathfinding;

[CreateAssetMenu(fileName = "ClearReachableTiles", menuName = "State Machines/Actions/Player/ClearReachableTiles")]
public class ClearReachableTilesOnExitSO : StateActionSO
{
    [SerializeField] private VoidEventChannelSO clearReachableTilesEC;
    public override StateAction CreateAction() => new ClearReachableTilesOnExit(clearReachableTilesEC);
}

public class ClearReachableTilesOnExit : StateAction
{
    private VoidEventChannelSO clearReachableTilesEC;
    
    public ClearReachableTilesOnExit(VoidEventChannelSO clearReachableTilesEC) {
        this.clearReachableTilesEC = clearReachableTilesEC;
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
        clearReachableTilesEC.RaiseEvent();
    }
}
