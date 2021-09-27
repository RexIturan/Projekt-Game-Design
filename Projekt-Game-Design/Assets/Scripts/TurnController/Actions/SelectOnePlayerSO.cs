using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SelectOnePlayer", menuName = "State Machines/Actions/Select One Player")]
public class SelectOnePlayerSO : StateActionSO
{
	protected override StateAction CreateAction() => new SelectOnePlayer();
}

public class SelectOnePlayer : StateAction
{
	protected new SelectOnePlayerSO OriginSO => (SelectOnePlayerSO)base.OriginSO;
	private List<GameObject> list;
	private TurnContainerCO turnController;

	public override void Awake(StateMachine stateMachine)
	{
		list = stateMachine.GetComponent<TurnContainerCO>().PlayersSelected;
		turnController = stateMachine.GetComponent<TurnContainerCO>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
	{
		MeshRenderer render;
		for (int i = 0; i < list.Count-1; i++)
		{
			render = list[i].GetComponent<MeshRenderer>();
			render.material.color = Color.green;
			turnController.PlayersSelected.Remove(list[i]);
			Debug.Log("Ein Spieler wurde deselected");
			list[i].GetComponent<PlayerCharacterSC>().isSelected = false;

		}
		render = list[list.Count-1].GetComponent<MeshRenderer>();
		render.material.color = Color.magenta;
	}
	
	public override void OnStateExit()
	{
	}
}
