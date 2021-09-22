using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "DebugLogOnEnter", menuName = "State Machines/Actions/Debug Log On Enter")]
public class DebugLogOnEnterSO : StateActionSO
{
	protected override StateAction CreateAction() => new DebugLogOnEnter();
}

public class DebugLogOnEnter : StateAction
{
	protected new DebugLogOnEnterSO OriginSO => (DebugLogOnEnterSO)base.OriginSO;

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
		Debug.Log("Ich befinde mich im OnEnterState");
		render.material.color = Color.magenta;
	}
}
