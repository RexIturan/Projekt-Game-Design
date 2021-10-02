using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_MoveToTarget_OnEnter", menuName = "State Machines/Actions/Enemy/e_MoveToTarget_OnEnter")]
public class e_MoveToTarget_OnEnterSO : StateActionSO
{
    public override StateAction CreateAction() => new e_MoveToTarget_OnEnter();
}

public class e_MoveToTarget_OnEnter : StateAction
{
    protected new e_MoveToTarget_OnEnterSO OriginSO => (e_MoveToTarget_OnEnterSO)base.OriginSO;

    private EnemyCharacterSC enemyCharacterSC;

    public override void Awake(StateMachine stateMachine)
    {
        enemyCharacterSC = stateMachine.gameObject.GetComponent<EnemyCharacterSC>();
    }

    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        enemyCharacterSC.position = new Vector3Int(enemyCharacterSC.movementTarget.x,
                                                       1,
                                                       enemyCharacterSC.movementTarget.y);
        enemyCharacterSC.transformToPosition();
    }
}
