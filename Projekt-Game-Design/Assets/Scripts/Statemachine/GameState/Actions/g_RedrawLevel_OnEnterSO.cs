using Characters.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_RedrawLevel_OnEnter", menuName = "State Machines/Actions/GameState/RedrawLevel On Enter")]
public class g_RedrawLevel_OnEnterSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO levelLoaded;

    public override StateAction CreateAction() => new g_RedrawLevel_OnEnter(levelLoaded);
}

public class g_RedrawLevel_OnEnter : StateAction
{
    protected new g_RedrawLevel_OnEnterSO OriginSO => (g_RedrawLevel_OnEnterSO)base.OriginSO;

    private VoidEventChannelSO levelLoaded;

    public g_RedrawLevel_OnEnter(VoidEventChannelSO levelLoaded)
    {
        this.levelLoaded = levelLoaded;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter()
    {
        levelLoaded.RaiseEvent();
    }

    public override void OnStateExit() { }
}