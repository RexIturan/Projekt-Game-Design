using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "e_CheckedRange", menuName = "State Machines/Conditions/Enemy/Checked Range")]
public class CheckedRangeSO : StateConditionSO
{
	protected override Condition CreateCondition() => new CheckedRange();
}

public class CheckedRange : Condition
{
	protected new CheckedRangeSO OriginSO => (CheckedRangeSO)base.OriginSO;
	private EnemyCharacterSC enemyCharacterSC;
	
	public override void Awake(StateMachine stateMachine) {
		enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
	}
	
	protected override bool Statement()
	{
		return enemyCharacterSC.rangeChecked;
	}
	
	public override void OnStateEnter()
	{
	}
	
	public override void OnStateExit() {
		// enemyCharacterSC.rangeChecked = false;
	}
}
