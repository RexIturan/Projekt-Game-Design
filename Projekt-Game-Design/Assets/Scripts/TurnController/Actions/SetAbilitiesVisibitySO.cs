using System.Collections.Generic;
using UnityEngine;
using Events.ScriptableObjects;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "SetAbilitiesVisibity", menuName = "State Machines/Actions/TurnState/SetAbilitiesVisibity")]
public class SetAbilitiesVisibitySO : StateActionSO
{
    [Header("Sending Events On")]
    [SerializeField] private BoolEventChannelSO setActionMenuVisibility;
    [SerializeField] private bool visible;

    public override StateAction CreateAction() => new SetAbilitiesVisibity(setActionMenuVisibility, visible);
}

public class SetAbilitiesVisibity : StateAction
{
    private BoolEventChannelSO setActionMenuVisibility;
    private bool show;
    // private TurnContainerCO turnController;

    public SetAbilitiesVisibity(BoolEventChannelSO ActionMenuVisibilityEvent, bool visibility)
    {
        setActionMenuVisibility = ActionMenuVisibilityEvent;
        show = visibility;
    }

    public override void Awake(StateMachine stateMachine)
	{
		// turnController = stateMachine.GetComponent<TurnContainerCO>();
	}
	
	public override void OnUpdate()
	{
	}
	
	public override void OnStateEnter()
    {
        setActionMenuVisibility.RaiseEvent(show);
	}
	
	public override void OnStateExit()
	{

	}
}
