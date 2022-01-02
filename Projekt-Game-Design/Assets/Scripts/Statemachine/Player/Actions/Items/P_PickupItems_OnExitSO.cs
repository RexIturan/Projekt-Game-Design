using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Characters;
using Grid;
using System.Collections.Generic;
using Events.ScriptableObjects;

[CreateAssetMenu(fileName = "p_PickupItems_OnExit",
	menuName = "State Machines/Actions/Player/Pickup Items On Exit")]
public class P_PickupItems_OnExitSO : StateActionSO {
	[SerializeField] private IntEventChannelSO itemPickupEvent;

	public override StateAction CreateAction() => new P_PickupItems_OnExit(itemPickupEvent);
}

public class P_PickupItems_OnExit : StateAction {
	private IntEventChannelSO _itemPickupEvent;

	private GridController _gridController;
	private GridTransform _gridTransform;

	public P_PickupItems_OnExit(IntEventChannelSO itemPickupEvent) {
		_itemPickupEvent = itemPickupEvent;
	}

	public override void Awake(StateMachine stateMachine) {
		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
		FindGridController();
	}

	private void FindGridController() {
		GameObject gridControllerGameObject = GameObject.Find("GridController");
		if ( gridControllerGameObject )
			_gridController = gridControllerGameObject.GetComponent<GridController>();
	}

	public override void OnStateExit() {
		if(!_gridController)
			FindGridController();

		if(!_gridController)
			Debug.LogError("Could not find Grid Controller. ");
		else { 
			// Debug.Log("Searching for items at: " + _gridTransform.gridPosition.x + ", " + _gridTransform.gridPosition.z);
			List<int> items = _gridController.GetItemsAtGridPos(_gridTransform.gridPosition);

			if(items.Count > 0) {
				// Debug.Log("Item found ");
				foreach(int itemId in items) 
					_itemPickupEvent.RaiseEvent(itemId);

				_gridController.RemoveAllItemsAtGridPos(_gridTransform.gridPosition);
			}
		}
	}

		public override void OnUpdate() { }
}