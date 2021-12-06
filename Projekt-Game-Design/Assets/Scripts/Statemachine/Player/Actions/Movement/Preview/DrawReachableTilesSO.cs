using Characters.Movement;
using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "DrawReachableTiles",
	menuName = "State Machines/Actions/Player/DrawReachableTiles")]
public class DrawReachableTilesSO : StateActionSO {
	[Header("Sending Events On")] [SerializeField]
	private NodeListEventChannelSO drawReachableTileEC;

	public override StateAction CreateAction() => new DrawReachableTiles(drawReachableTileEC);
}

public class DrawReachableTiles : StateAction {
	private NodeListEventChannelSO _drawReachableTileEC;

	private MovementController _movementController;
	// private PlayerCharacterSC _playerStateContainer;

	public DrawReachableTiles(NodeListEventChannelSO drawReachableTileEC) {
		this._drawReachableTileEC = drawReachableTileEC;
	}

	public override void Awake(StateMachine stateMachine) {
		_movementController = stateMachine.gameObject.GetComponent<MovementController>();
	}

	public override void OnUpdate() {
		// pathfindingDrawer.DrawPreview(playerStateContainer.reachableTiles);
	}

	public override void OnStateEnter() {
		// pathfindingDrawer.ClearPreviewTilemap();
		// Debug.Log("Zeichne reachable tiles von Player aus");
		_drawReachableTileEC.RaiseEvent(_movementController.reachableTiles);
	}

	public override void OnStateExit() { }
}