using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "EnemyMoveFinished", menuName = "State Machines/Conditions/Enemy Move Finished")]
public class EnemyMoveFinishedSO : StateConditionSO
{
	protected override Condition CreateCondition() => new EnemyMoveFinished();
}

public class EnemyMoveFinished : Condition
{
	private EnemyCharacterSC enemyCharacterSc;
	protected new EnemyMoveFinishedSO OriginSO => (EnemyMoveFinishedSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return this.enemyCharacterSc.moveFinished;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
