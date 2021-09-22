using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DebugLogOnExit", menuName = "State Machines/Actions/Debug Log On Exit")]
public class DebugLogOnExitSO : StateActionSO
{
	protected override StateAction CreateAction() => new DebugLogOnExit();
}

public class DebugLogOnExit : StateAction
{
	protected new DebugLogOnExitSO OriginSO => (DebugLogOnExitSO)base.OriginSO;

	private MeshRenderer render;
	public override void OnUpdate()
	{
	}

	public override void Awake(StateMachine stateMachine)
	{
		render = stateMachine.gameObject.GetComponent<MeshRenderer>();
	}

	public override void OnStateEnter()
	{
		Debug.Log("Ich befinde mich nun im OnEnterStatus");
		render.material.color = Color.green;
	}
}
