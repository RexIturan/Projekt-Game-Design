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

	private EnemyCharacterSC _enemyCharacterSC;

	public override void Awake(StateMachine stateMachine) {
		_enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement() {
		var inRangeTiles = _enemyCharacterSC.tileInRangeOfTarget;
		var pos = _enemyCharacterSC.gridPosition;
		
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
