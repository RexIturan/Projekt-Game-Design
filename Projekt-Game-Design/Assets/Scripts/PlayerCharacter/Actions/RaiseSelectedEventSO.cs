using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "RaiseSelectedEvent", menuName = "State Machines/Actions/Player/RaiseSelectedEvent")]
public class RaiseSelectedEventSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] public GameObjActionEventChannelSO selectNewPlayer;
    public override StateAction CreateAction() => new RaiseSelectedEvent(selectNewPlayer);
}

public class RaiseSelectedEvent : StateAction
{
    private GameObject gameObject;
    private GameObjActionEventChannelSO selectNewPlayer;

    public RaiseSelectedEvent(GameObjActionEventChannelSO gameObjEventChannel)
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
    
    //TODO: Muss geändert werden
    public void irgendwas(int zahl)
    {
        Debug.Log("Es wurde die Action mit der ID: " + zahl + " gedrückt.");
    }

    public override void OnStateEnter()
    {
        Debug.Log("Ich bin selected");
        selectNewPlayer.RaiseEvent(gameObject,irgendwas);
    }
}
