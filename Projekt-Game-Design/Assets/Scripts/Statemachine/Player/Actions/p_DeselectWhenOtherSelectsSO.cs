using Events.ScriptableObjects;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_DeselectWhenOtherSelects", menuName = "State Machines/Actions/Player/Deselect When Other Selects")]
public class p_DeselectWhenOtherSelectsSO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] public GameObjActionEventChannelSO selectNewPlayer;

    public override StateAction CreateAction() => new p_DeselectWhenOtherSelects(selectNewPlayer);
}

public class p_DeselectWhenOtherSelects : StateAction
{
    private GameObject gameObject;
    private PlayerCharacterSC playerCharacterSc;
    private GameObjActionEventChannelSO selectNewPlayer;

    public p_DeselectWhenOtherSelects(GameObjActionEventChannelSO gameObjEventChannel)
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
        selectNewPlayer.OnEventRaised += DeselectSelf;
    }

    private void DeselectSelf(GameObject selectedPlayer, Action<int> callback)
    {
        if(!selectedPlayer.Equals(gameObject))
        {
            gameObject.GetComponent<PlayerCharacterSC>().isSelected = false;
        }
    }
}
