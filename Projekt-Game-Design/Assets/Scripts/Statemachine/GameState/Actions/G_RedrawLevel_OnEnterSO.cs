using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_RedrawLevel_OnEnter", menuName = "State Machines/Actions/GameState/RedrawLevel On Enter")]
public class G_RedrawLevel_OnEnterSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO levelLoaded;

    public override StateAction CreateAction() => new G_RedrawLevel_OnEnter(levelLoaded);
}

public class G_RedrawLevel_OnEnter : StateAction {
	
    protected new G_RedrawLevel_OnEnterSO OriginSO => (G_RedrawLevel_OnEnterSO)base.OriginSO;

    private readonly VoidEventChannelSO _levelLoaded;

    public G_RedrawLevel_OnEnter(VoidEventChannelSO levelLoaded)
    {
        this._levelLoaded = levelLoaded;
    }

    public override void Awake(StateMachine stateMachine) { }

    public override void OnUpdate() { }

    public override void OnStateEnter() {
        _levelLoaded.RaiseEvent();
    }

    public override void OnStateExit() { }
}