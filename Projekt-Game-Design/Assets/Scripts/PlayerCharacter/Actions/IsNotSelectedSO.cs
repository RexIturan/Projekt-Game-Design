using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "IsNotSelected", menuName = "State Machines/Actions/IsNotSelected")]
public class IsNotSelectedSO : StateActionSO
{
	
	[Header("Sending Events On")]
	[SerializeField] public BoolEventChannelSO setActionMenuVisibility;
	
	[SerializeField] public GameObjEventChannelSO addPlayerToSelection;
	protected override StateAction CreateAction() => new IsNotSelected(setActionMenuVisibility, addPlayerToSelection);
	

}

public class IsNotSelected : StateAction
{
	protected new IsNotSelectedSO OriginSO => (IsNotSelectedSO)base.OriginSO;

	private MeshRenderer render;


	private BoolEventChannelSO setActionMenuVisibility;

	private GameObjEventChannelSO addPlayerToSelection;

	private GameObject gameObject;

	public IsNotSelected(BoolEventChannelSO evChannel, GameObjEventChannelSO gameObjEventChannel)
	{
		setActionMenuVisibility = evChannel;
		addPlayerToSelection = gameObjEventChannel;
	}
	public override void OnUpdate()
	{
	}

	public override void Awake(StateMachine stateMachine)
	{
		render = stateMachine.gameObject.GetComponent<MeshRenderer>();
		gameObject = stateMachine.gameObject;
	}

	public override void OnStateEnter()
	{
		Debug.Log("Ich bin nicht selected");
		render.material.color = Color.green;
		setActionMenuVisibility.RaiseEvent(false);
		addPlayerToSelection.RaiseEvent(gameObject);
	}
}
