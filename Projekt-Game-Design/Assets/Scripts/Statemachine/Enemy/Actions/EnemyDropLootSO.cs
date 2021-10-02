using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DropLoot", menuName = "State Machines/Actions/Drop Loot")]
public class DropLootSO : StateActionSO
{
	public override StateAction CreateAction() => new DropLoot();
}

public class DropLoot : StateAction
{
	protected new DropLootSO OriginSO => (DropLootSO)base.OriginSO;

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
