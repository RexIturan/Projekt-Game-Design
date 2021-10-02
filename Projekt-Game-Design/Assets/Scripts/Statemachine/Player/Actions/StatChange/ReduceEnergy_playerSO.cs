using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions {
	[CreateAssetMenu(fileName = "ReduceEnergy_player", menuName = "State Machines/Actions/Reduce Energy_player")]
	public class ReduceEnergy_playerSO : StateActionSO
	{
		[SerializeField] private AbilityContainerSO abilityContainer;
		public override StateAction CreateAction() => new ReduceEnergy_player(abilityContainer);
	}

	public class ReduceEnergy_player : StateAction
	{
		protected new ReduceEnergy_playerSO OriginSO => (ReduceEnergy_playerSO)base.OriginSO;
		private PlayerCharacterSC playerCharacterSC;
		private readonly AbilityContainerSO abilityContainer;

		public ReduceEnergy_player(AbilityContainerSO abilityContainer) {
			this.abilityContainer = abilityContainer;
		}
	
		public override void Awake(UOP1.StateMachine.StateMachine stateMachine) {
			playerCharacterSC = stateMachine.gameObject.GetComponent<PlayerCharacterSC>();
		}
	
		public override void OnUpdate()
		{
		}
	
		public override void OnStateEnter() {
			var currentAbility = abilityContainer.abilities[playerCharacterSC.AbilityID];
		
			var energyReduction = currentAbility.costs + (currentAbility.moveToTarget ? playerCharacterSC.GetEnergyUseUpFromMovement() : 0);  
			playerCharacterSC.energy -= energyReduction;
		}
	
		public override void OnStateExit()
		{
		}
	}
}