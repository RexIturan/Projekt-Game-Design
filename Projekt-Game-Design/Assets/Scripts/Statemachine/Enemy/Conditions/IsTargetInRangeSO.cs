using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_IsTargetInRange", menuName = "State Machines/Conditions/Enemy/Is Target In Range")]
public class IsTargetInRangeSO : StateConditionSO
{
	protected override Condition CreateCondition() => new IsTargetInRange();
}

public class IsTargetInRange : Condition
{
	protected new IsTargetInRangeSO OriginSO => (IsTargetInRangeSO)base.OriginSO;

	private EnemyCharacterSC enemyCharacterSC;

	public override void Awake(StateMachine stateMachine) {
		enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement() {
		var inRangeTiles = enemyCharacterSC.tileInRangeOfTarget;
		var pos = enemyCharacterSC.gridPosition;
		
		if (inRangeTiles is null) return false;

		foreach (var tilePos in inRangeTiles) {
			if (pos == tilePos) return true;
		}
		
		return false;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit()
	{
	}
}
