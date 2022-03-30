using System.Collections.Generic;
using System.Linq;
using Characters;
using Events.ScriptableObjects.GameState;
using GDP01._Gameplay.Provider;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "g_TriggerDefeatOnPlayerDeath_OnUpdate", menuName = "State Machines/Actions/GameState/Trigger Defeat On Player Death On Update")]
public class G_TriggerDefeatOnPlayerDeath_OnUpdateSO : StateActionSO
{
		[Header("Sending Events On")]
		[SerializeField] private VoidEventChannelSO triggerDefeatEC;

		public override StateAction CreateAction() => new G_TriggerDefeatOnPlayerDeath_OnUpdate(triggerDefeatEC);
}

public class G_TriggerDefeatOnPlayerDeath_OnUpdate : StateAction
{
		private readonly VoidEventChannelSO triggerDefeatEC;

		public G_TriggerDefeatOnPlayerDeath_OnUpdate(VoidEventChannelSO triggerDefeatEC)
		{
				this.triggerDefeatEC = triggerDefeatEC;
		}

		public override void Awake(StateMachine stateMachine) { }

		public override void OnUpdate() {
				bool allDead = true;

				List<PlayerCharacterSC> playerCahractersAlive = GameplayProvider.Current.CharacterManager.GetPlayerCharactersWhere((player) => player.IsAlive).ToList();
				if ( playerCahractersAlive is { Count: > 0 } ) {
					allDead = false;
				}

				if ( allDead )
						triggerDefeatEC.RaiseEvent();
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
}