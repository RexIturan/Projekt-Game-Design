using Events.ScriptableObjects;
using UnityEditorInternal;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "IsSelected", menuName = "State Machines/Actions/IsSelected")]
public class IsSelectedSO : StateActionSO
{
	[Header("Sending Events On")]
	[SerializeField] public BoolEventChannelSO setActionMenuVisibility;
	[SerializeField] public GameObjEventChannelSO addPlayerToSelection;
	public override StateAction CreateAction() => new IsSelected(setActionMenuVisibility,addPlayerToSelection);
}

public class IsSelected : StateAction
{
	protected new IsSelectedSO OriginSO => (IsSelectedSO)base.OriginSO;

	private MeshRenderer render;
	
	private BoolEventChannelSO setActionMenuVisibility;
	private GameObjEventChannelSO addPlayerToSelection;
	private GameObject gameObject;
	public IsSelected(BoolEventChannelSO evChannel, GameObjEventChannelSO gameObjEventChannel)
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
		Debug.Log("Ich bin selected");
		render.material.color = Color.magenta;
		setActionMenuVisibility.RaiseEvent(true);
		addPlayerToSelection.RaiseEvent(gameObject);
	}
}
