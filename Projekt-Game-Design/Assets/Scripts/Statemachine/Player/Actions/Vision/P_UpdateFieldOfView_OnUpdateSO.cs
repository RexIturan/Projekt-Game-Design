using Events.ScriptableObjects;
using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Grid;
using Player;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using Util;
using StateMachine = UOP1.StateMachine.StateMachine;


namespace Statemachine.Player.Actions.Vision {
	[CreateAssetMenu(fileName = "p_UpdateFieldOfView_OnUpdate",
		menuName = "State Machines/Actions/Player/Update FieldOfView On Update")]
	public class P_UpdateFieldOfView_OnUpdateSO : StateActionSO {
		[SerializeField] private GridDataSO gridDataSO;
		[SerializeField] private VoidEventChannelSO fov_PlayerCharViewUpdateEC;

		public override StateAction CreateAction() => new C_UpdateFieldOfView_OnUpdate(fov_PlayerCharViewUpdateEC, gridDataSO);
	}

	public class C_UpdateFieldOfView_OnUpdate : StateAction {
		protected new P_UpdateFieldOfView_OnUpdateSO OriginSO => ( P_UpdateFieldOfView_OnUpdateSO )base.OriginSO;
    
			private readonly VoidEventChannelSO fov_PlayerCharViewUpdateEC;
    	private readonly GridDataSO _gridDataSO;
    	
    	// Game Object Components
    
    	private MovementController _movementController;
    	private GridTransform _gridTransform;
    	
    	// local variables
    	
    	private float TimePerStep;
    	private float _timeSinceLastStep;
      private bool _playerMove;
    
    	public C_UpdateFieldOfView_OnUpdate(VoidEventChannelSO fov_PlayerCharViewUpdateEC, GridDataSO gridDataSO) {
	      this.fov_PlayerCharViewUpdateEC = fov_PlayerCharViewUpdateEC;
    		_gridDataSO = gridDataSO;
    	}
    
    	public override void Awake(StateMachine stateMachine) {
	      var statistics = stateMachine.gameObject.GetComponent<Statistics>();
	      _playerMove = statistics.GetFaction() == Faction.Player;
	      _movementController = stateMachine.gameObject.GetComponent<MovementController>();
    		_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
    		TimePerStep = _gridDataSO.CellSize / _movementController.moveSpeed;
    	}
    
    	public override void OnUpdate() {
	      if ( _playerMove ) {
		      _timeSinceLastStep += Time.deltaTime;
    
		      if ( _timeSinceLastStep >= TimePerStep) {
			      _timeSinceLastStep -= TimePerStep;
            
			      fov_PlayerCharViewUpdateEC.RaiseEvent();
		      }
	      }
    	}
    
    	public override void OnStateEnter() {
    		_timeSinceLastStep = 0;
    	}
	}
}
