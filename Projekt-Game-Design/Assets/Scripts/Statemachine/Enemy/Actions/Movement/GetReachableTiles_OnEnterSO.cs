using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "GetReachableTiles_OnEnter", menuName = "State Machines/Actions/Get Reachable Tiles_On Enter")]
public class GetReachableTiles_OnEnterSO : StateActionSO
{
	public override StateAction CreateAction() => new GetReachableTiles_OnEnter();
}

public class GetReachableTiles_OnEnter : StateAction
{
	protected new GetReachableTiles_OnEnterSO OriginSO => (GetReachableTiles_OnEnterSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
