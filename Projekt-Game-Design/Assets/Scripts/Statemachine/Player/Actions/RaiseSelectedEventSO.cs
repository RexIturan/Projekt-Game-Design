using Events.ScriptableObjects;
using UnityEngine;
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
    private GameObject _gameObject;
    private PlayerCharacterSC _playerCharacterSc;
    private GameObjActionEventChannelSO _selectNewPlayer;

    public RaiseSelectedEvent(GameObjActionEventChannelSO gameObjEventChannel)
    {
        _selectNewPlayer = gameObjEventChannel;
    }

    public override void OnUpdate()
    {

    }

    public override void Awake(StateMachine stateMachine)
    {
        _gameObject = stateMachine.gameObject;
        Debug.Log($"Awake raiseSelectedEvent {this}");
        _playerCharacterSc = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
    }
    
    //TODO: Muss geändert werden
    private void AbilityCallback(int value) {
        Debug.Log("Es wurde die Ability mit der ID: " + value + " gedrückt.");
        if (!_playerCharacterSc.abilitySelected)
        {
            _playerCharacterSc.abilitySelected = true;
            _playerCharacterSc.AbilityID = value;
        }
        else
        {
            _playerCharacterSc.abilitySelected = false;
            _playerCharacterSc.AbilityID = -1;
        }
    }

    public override void OnStateEnter()
    {
        Debug.Log("Ich bin selected");
        _selectNewPlayer.RaiseEvent(_gameObject, AbilityCallback);
    }
}
