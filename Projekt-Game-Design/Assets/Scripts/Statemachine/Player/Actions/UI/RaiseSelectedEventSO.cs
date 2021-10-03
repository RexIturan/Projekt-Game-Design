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
    private PlayerCharacterSC playerCharacterSc;
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
        playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }
    
    //TODO: Muss geändert werden
    private void AbilityCallback(int value) {
        Debug.Log("Es wurde die Ability mit der ID: " + value + " gedrückt.");
        playerCharacterSc.abilitySelected = true;
        playerCharacterSc.AbilityID = value; 
    }

    public override void OnStateEnter()
    {
        Debug.Log("Ich bin selected");
        selectNewPlayer.RaiseEvent(gameObject, AbilityCallback);
    }
}
