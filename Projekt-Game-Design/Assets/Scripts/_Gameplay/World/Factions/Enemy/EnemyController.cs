using Characters;
using Events.ScriptableObjects.GameState;
using System.Collections.Generic;
using System.Linq;
using Characters.Types;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using UnityEngine;

/**
 * Class that moderates enemy turn
 * defines when enemy turn is over (enemy wants to end turn)
 * defines enemy character's order in which they act
 */
public class EnemyController : MonoBehaviour {
	[Header("Sending Events On: ")] [SerializeField]
	private EFactionEventChannelSO endTurnEC;

	[Header("Receiving Events On")] [SerializeField]
	private EFactionEventChannelSO startTurnEC;

	[SerializeField] private bool isOnTurn;

	private List<EnemyCharacterSC>
		enemyOrder; // saves each enemy in order in which they are supposed to act

	private int currentlyActingEnemy = 0;

	private List<EnemyCharacterSC>
		enemiesToDestroy = new List<EnemyCharacterSC>(); // deletes these enemies after one round

	private CharacterManager CharacterManager => GameplayProvider.Current.CharacterManager;

	// Start is called before the first frame update
	void Start() {
		isOnTurn = false;
		enemiesToDestroy = new List<EnemyCharacterSC>();
	}

	private void Awake() {
		startTurnEC.OnEventRaised += StartNewTurn;
	}

	private void OnDestroy() {
		startTurnEC.OnEventRaised -= StartNewTurn;
	}

	private void Update() {
		EvaluateEnemyTurn();
	}

	public void EvaluateEnemyTurn() {
		if ( isOnTurn ) {
			// find next enemy character that isn't done
			while ( currentlyActingEnemy < enemyOrder.Count &&
			        ( enemyOrder[currentlyActingEnemy].isDone ||
			          enemyOrder[currentlyActingEnemy].IsDead ) )
				currentlyActingEnemy++;

			// if there is no enemy character that isn't done (= all enemy characters are done),
			// end turn
			// elsewise tell enemy character to make their turn
			if ( currentlyActingEnemy >= enemyOrder.Count )
				EndTurn();
			else {
				enemyOrder[currentlyActingEnemy].isNextToAct = true;
			}
		}
	}

	private void StartNewTurn(Faction faction) {
		if ( faction.Equals(Faction.Enemy) ) {
			isOnTurn = true;
			SetUpAllEnemies();
			DestroyDeadEnemies();
		}
	}

	// sets each enemy to not done and not on turn
	// defines/initialises the (turn)order in which the enemy characters act
	private void SetUpAllEnemies() {
		// turn order is order within enemy container, may be changes later on, but not necessary
		enemyOrder = new List<EnemyCharacterSC>();

		GameplayProvider.Current.CharacterManager.GetEnemyCahracters().ForEach(
			enemy => {
				enemy.isDone = false;
				enemy.isNextToAct = false;
				enemyOrder.Add(enemy);
			});

		currentlyActingEnemy = 0;
	}

	private void EndTurn() {
		Debug.Log("Enemy Controller ending turn. ");
		isOnTurn = false;
		endTurnEC.RaiseEvent(Faction.Enemy);
	}

	private void DestroyDeadEnemies() {
		enemiesToDestroy.ForEach(enemy => enemy.Remove());

		List<EnemyCharacterSC> enemiesToremove = CharacterManager.GetEnemyCahracters()
			.Where(enemy => enemy.ShouldBeRemoved).ToList();

		enemiesToDestroy.AddRange(enemiesToremove);
	}
}