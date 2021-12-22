using Ability.ScriptableObjects;
using Characters.Ability;
using Combat;
using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_DrawPattern_OnUpdate", menuName = "State Machines/Actions/Player/Draw Pattern On Update")]
public class P_DrawPattern_OnUpdateSO : StateActionSO {
	[Header("Sending Events On")]
	[SerializeField] private DrawPatternEventChannelSO drawPatternEC;
	[SerializeField] private VoidEventChannelSO clearPatternEC;

	[SerializeField] private InputCache inputCache;
	[SerializeField] private AbilityContainerSO abilityContainer;

	public override StateAction CreateAction() => new P_DrawPattern_OnUpdate(drawPatternEC, clearPatternEC, inputCache, abilityContainer);
}

public class P_DrawPattern_OnUpdate : StateAction {
	private readonly DrawPatternEventChannelSO _drawPatternEC;
	private readonly VoidEventChannelSO _clearPatternEC;
	private InputCache _inputCache;
	private AbilityContainerSO _abilityContainer;

	private Attacker _attacker;
	private AbilityController _abilityController;
  private bool _isDrawn;
	private Vector3Int _lastDrawnGridPos;
		
	public P_DrawPattern_OnUpdate(DrawPatternEventChannelSO drawPatternEC, VoidEventChannelSO clearPatternEC,
			InputCache inputCache, AbilityContainerSO abilityContainer) {
		_drawPatternEC = drawPatternEC;
		_clearPatternEC = clearPatternEC;
		_inputCache = inputCache;
		_abilityContainer = abilityContainer;
	}

	public override void Awake(StateMachine stateMachine) {
		_attacker = stateMachine.gameObject.GetComponent<Attacker>();
		_abilityController = stateMachine.gameObject.GetComponent<AbilityController>();
		_isDrawn = false;
	}

	public override void OnUpdate() {
		Vector3Int mousePos = _inputCache.cursor.abovePos.gridPos;
		if(!_isDrawn || !_lastDrawnGridPos.Equals(mousePos)) { 
			bool isInRange = false;
			
			for(int i = 0; !isInRange && i < _attacker.tilesInRange.Count; i++) { 
		    if(_attacker.tilesInRange[i].pos.Equals(mousePos)) {
					isInRange = true;

					AbilitySO ability = _abilityContainer.abilities[_abilityController.SelectedAbilityID];

					int rotations = _attacker.GetRotationsToTarget(mousePos);

					_drawPatternEC.RaiseEvent(mousePos, 
							ability.targetedEffects[0].area.GetPattern(rotations), 
							ability.targetedEffects[0].area.GetAnchor(rotations));

					_isDrawn = true;
					_lastDrawnGridPos = mousePos;
				}
      }

			if(!isInRange) {
				_clearPatternEC.RaiseEvent();
				_isDrawn = false;
			}
    }
	}

	public override void OnStateEnter() { }

	public override void OnStateExit() { }
}