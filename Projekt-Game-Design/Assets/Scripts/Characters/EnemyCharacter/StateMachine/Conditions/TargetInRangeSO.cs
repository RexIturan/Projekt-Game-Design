using field_of_view;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "TargetInRange", menuName = "State Machines/Conditions/Target In Range")]
public class TargetInRangeSO : StateConditionSO
{
	protected override Condition CreateCondition() => new TargetInRange();
}

public class TargetInRange : Condition
{
	private EnemyCharacterSC enemyCharacterSc;
	protected new TargetInRangeSO OriginSO => (TargetInRangeSO)base.OriginSO;

	public override void Awake(StateMachine stateMachine)
	{
		this.enemyCharacterSc = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return enemyCharacterSc.targetInRange;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
