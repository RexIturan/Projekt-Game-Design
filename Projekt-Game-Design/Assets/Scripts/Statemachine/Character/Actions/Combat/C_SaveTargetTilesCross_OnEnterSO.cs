using Ability.ScriptableObjects;
using Characters;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;
using Events.ScriptableObjects.FieldOfView;
using Level.Grid;
using FieldOfView;
using GDP01.Characters.Component;
using GDP01.World.Components;

[CreateAssetMenu(fileName = "c_SaveTargetTilesCross_OnEnter", menuName = "State Machines/Actions/Character/Save Target Tiles Cross On Enter")]
public class C_SaveTargetTilesCross_OnEnterSO : StateActionSO
{
		[SerializeField] private FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEvent;
		[SerializeField] private AbilityContainerSO abilityContainer;

		public override StateAction CreateAction() => 
				new C_SaveTargetTilesCross_OnEnter(fieldOfViewCrossQueryEvent, abilityContainer);
}

public class C_SaveTargetTilesCross_OnEnter : StateAction
{
		private Attacker _attacker;
		private AbilityController _abilityController;
		private GridTransform _gridTransform;

		private readonly FOVCrossQueryEventChannelSO _fieldOfViewCrossQueryEvent;
		private readonly AbilityContainerSO _abilityContainer;

		public C_SaveTargetTilesCross_OnEnter(FOVCrossQueryEventChannelSO fieldOfViewCrossQueryEvent, AbilityContainerSO abilityContainer)
		{
				_fieldOfViewCrossQueryEvent = fieldOfViewCrossQueryEvent;
				_abilityContainer = abilityContainer;
		}

		public override void OnUpdate() { }

		public override void Awake(StateMachine stateMachine)
		{
				_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
				_attacker = stateMachine.gameObject.GetComponent<Attacker>();
				_gridTransform = stateMachine.gameObject.GetComponent<GridTransform>();
		}

		public override void OnStateEnter()
		{
				_fieldOfViewCrossQueryEvent.RaiseEvent(_gridTransform.gridPosition, 
						TileProperties.ShootTrough, SaveToStateContainer);
		}

		public void SaveToStateContainer(bool[,] tilesInRange)
		{
				_attacker.tilesInRange = FieldOfViewController.VisibleTilesToPathNodeList(tilesInRange);
		}
}