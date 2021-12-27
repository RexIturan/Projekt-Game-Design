using Characters;
using Events.ScriptableObjects.GameState;
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
		private CharacterList characterList;

		public G_TriggerDefeatOnPlayerDeath_OnUpdate(VoidEventChannelSO triggerDefeatEC)
		{
				this.triggerDefeatEC = triggerDefeatEC;
		}

		public override void Awake(StateMachine stateMachine) { }

		public override void OnUpdate()
		{
				if ( !characterList )
				{
						GameObject characterListGameObject = GameObject.Find("Characters");
						if ( characterListGameObject )
								characterList = characterListGameObject.GetComponent<CharacterList>();
				}
				if( characterList )
				{
						bool allDead = true;

						foreach(GameObject player in characterList.playerContainer)
						{
								Statistics playerStats = player.GetComponent<Statistics>();
								if ( playerStats && !playerStats.StatusValues.HitPoints.IsMin() )
										allDead = false;
						}

						if ( allDead )
								triggerDefeatEC.RaiseEvent();
				}
		}

		public override void OnStateEnter() { }

		public override void OnStateExit() { }
}