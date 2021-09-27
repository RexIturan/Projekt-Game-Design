using Events.ScriptableObjects;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "RaiseSelectedEvent", menuName = "State Machines/Actions/Player/RaiseSelectedEvent")]
public class RaiseSelectedEventSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] public GameObjEventChannelSO selectNewPlayer;
    public override StateAction CreateAction() => new RaiseSelectedEvent(selectNewPlayer);
}

public class RaiseSelectedEvent : StateAction
{
    private GameObject gameObject;
    private GameObjEventChannelSO selectNewPlayer;

    public RaiseSelectedEvent(GameObjEventChannelSO gameObjEventChannel)
    {
        selectNewPlayer = gameObjEventChannel;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        gameObject = stateMachine.gameObject;
    }

    public override void OnStateEnter()
    {
        Debug.Log("Ich bin selected");
        selectNewPlayer.RaiseEvent(gameObject);
    }
}
