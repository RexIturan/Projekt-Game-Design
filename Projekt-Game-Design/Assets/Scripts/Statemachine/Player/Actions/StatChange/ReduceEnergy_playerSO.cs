using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions {
	[CreateAssetMenu(fileName = "ReduceEnergy_player", menuName = "State Machines/Actions/Reduce Energy_player")]
	public class ReduceEnergy_PlayerSO : StateActionSO
	{
		[SerializeField] private AbilityContainerSO abilityContainer;
		public override StateAction CreateAction() => new ReduceEnergy_Player(abilityContainer);
	}

	public class ReduceEnergy_Player : StateAction
	{
		protected new ReduceEnergy_PlayerSO OriginSO => (ReduceEnergy_PlayerSO)base.OriginSO;
		private PlayerCharacterSC _playerCharacterSC;
		private readonly AbilityContainerSO _abilityContainer;

		public ReduceEnergy_Player(AbilityContainerSO abilityContainer) {
			this._abilityContainer = abilityContainer;
		}
	
		public override void Awake(UOP1.StateMachine.StateMachine stateMachine) {
			_playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
		}
	
		public override void OnUpdate()
		{
		}
	
		public override void OnStateEnter() {
			var currentAbility = _abilityContainer.abilities[_playerCharacterSC.AbilityID];
		
			var energyReduction = currentAbility.costs + (currentAbility.moveToTarget ? _playerCharacterSC.GetEnergyUseUpFromMovement() : 0);  
			_playerCharacterSC.energy -= energyReduction;
		}
	
		public override void OnStateExit()
		{
		}
	}
}