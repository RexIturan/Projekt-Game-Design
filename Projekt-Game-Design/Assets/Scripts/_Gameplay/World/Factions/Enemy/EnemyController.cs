using Characters;
using Events.ScriptableObjects.GameState;
using System.Collections.Generic;
using Characters.Types;
using UnityEngine;

/**
 * Class that moderates enemy turn
 * defines when enemy turn is over (enemy wants to end turn)
 * defines enemy character's order in which they act
 */
public class EnemyController : MonoBehaviour
{
		[Header("Sending Events On: ")]
		[SerializeField] private EFactionEventChannelSO endTurnEC;

		[Header("Receiving Events On")]
		[SerializeField] private EFactionEventChannelSO startTurnEC;

		[SerializeField] private bool isOnTurn;
		private CharacterList characterList;
		private List<EnemyCharacterSC> enemyOrder; // saves each enemy in order in which they are supposed to act
		private int currentlyActingEnemy = 0;

		private List<GameObject> enemiesToDestroy; // deletes these enemies after one round

    // Start is called before the first frame update
    void Start()
    {
				isOnTurn = false;
				enemiesToDestroy = new List<GameObject>();
    }

		private void Awake()
		{
				startTurnEC.OnEventRaised += StartNewTurn;
				FindCharacterListIfNotSet();
		}

		private void OnDestroy()
		{
				startTurnEC.OnEventRaised -= StartNewTurn;
		}

		private void Update()
		{
				EvaluateEnemyTurn();
		}

		private void FindCharacterListIfNotSet()
		{
				if ( !characterList )
				{
						GameObject characterListGameObject = GameObject.Find("Characters");
						if ( characterListGameObject )
								characterList = characterListGameObject.GetComponent<CharacterList>();
				}
		}

		public void EvaluateEnemyTurn()
		{
				if ( isOnTurn )
				{
						FindCharacterListIfNotSet();

						// find next enemy character that isn't done
						while ( currentlyActingEnemy < enemyOrder.Count && 
								(enemyOrder[currentlyActingEnemy].isDone || enemyOrder[currentlyActingEnemy].isDead) )
								currentlyActingEnemy++;

						// if there is no enemy character that isn't done (= all enemy characters are done),
						// end turn
						// elsewise tell enemy character to make their turn
						if ( currentlyActingEnemy >= enemyOrder.Count )
								EndTurn();
						else
						{
								enemyOrder[currentlyActingEnemy].isNextToAct = true;
						}
				}
		}

		private void StartNewTurn(Faction faction)
		{
				if ( faction.Equals(Faction.Enemy) )
				{
						isOnTurn = true;
						SetUpAllEnemies();
						DestroyDeadEnemies();
				}
		}

		// sets each enemy to not done and not on turn
		// defines/initialises the (turn)order in which the enemy characters act
		private void SetUpAllEnemies()
		{
				FindCharacterListIfNotSet();

				// turn order is order within enemy container, may be changes later on, but not necessary
				enemyOrder = new List<EnemyCharacterSC>();

				foreach(GameObject enemyObj in characterList.enemyContainer)
				{
						EnemyCharacterSC enemy = enemyObj.GetComponent<EnemyCharacterSC>();
						enemy.isDone = false;
						enemy.isNextToAct = false;

						enemyOrder.Add(enemy);
				}

				currentlyActingEnemy = 0;
		}

		private void EndTurn()
		{
				isOnTurn = false;
				endTurnEC.RaiseEvent(Faction.Enemy);
		}

		private void DestroyDeadEnemies()
		{
				foreach(GameObject enemy in enemiesToDestroy)
				{
						characterList.deadEnemies.Remove(enemy);
						GameObject.Destroy(enemy);
				}

				// put new dead enemies on list
				foreach(GameObject enemy in characterList.deadEnemies)
				{
						enemiesToDestroy.Add(enemy);
				}
		}
}
