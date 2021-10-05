using Events.ScriptableObjects;
// using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "MoveToTarget", menuName = "State Machines/Actions/Player/MoveToTarget")]
public class MoveToTargetSO : StateActionSO
{
    public override StateAction CreateAction() => new MoveToTarget();
}

public class MoveToTarget : StateAction
{
    protected new MoveToTargetSO OriginSO => (MoveToTargetSO)base.OriginSO;
    
    private PlayerCharacterSC playerCharacterSC;

    public override void Awake(StateMachine stateMachine)
    {
        playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }
    
    public override void OnUpdate()
    {
    }

    public override void OnStateEnter()
    {
        playerCharacterSC.gridPosition = new Vector3Int(playerCharacterSC.movementTarget.x,
                                                       1,
                                                       playerCharacterSC.movementTarget.y);
        playerCharacterSC.TransformToPosition();
    }
}
