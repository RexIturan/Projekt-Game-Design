using Ability.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

namespace Characters.PlayerCharacter.StateMachine.Actions {
	[CreateAssetMenu(fileName = "p_ReduceEnergy_OnEnter", menuName = "State Machines/Actions/Player/Reduce Energy")]
	public class P_ReduceEnergy_OnEnterSO : StateActionSO
	{
		[SerializeField] private AbilityContainerSO abilityContainer;
		public override StateAction CreateAction() => new P_ReduceEnergy_OnEnter(abilityContainer);
	}

	public class P_ReduceEnergy_OnEnter : StateAction
	{
		protected new P_ReduceEnergy_OnEnterSO OriginSO => ( P_ReduceEnergy_OnEnterSO )base.OriginSO;
		private PlayerCharacterSC _playerCharacterSC;
		private readonly AbilityContainerSO _abilityContainer;

		public P_ReduceEnergy_OnEnter(AbilityContainerSO abilityContainer) {
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