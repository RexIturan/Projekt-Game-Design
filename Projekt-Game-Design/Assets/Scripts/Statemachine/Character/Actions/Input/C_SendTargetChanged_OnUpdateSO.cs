using Events.ScriptableObjects;
using Input;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "C_SendTargetChanged_OnUpdate", menuName = "State Machines/Actions/Character/Input/Send TargetChanged OnUpdate")]
public class C_SendTargetChanged_OnUpdateSO : StateActionSO {
	[SerializeField] private Vector3IntEventChannelSO targetChangedEC;
	[SerializeField] private InputCache inputCache;
	public override StateAction CreateAction() => new C_SendTargetChanged_OnUpdate(targetChangedEC, inputCache);
}

public class C_SendTargetChanged_OnUpdate : StateAction
{
	protected new C_SendTargetChanged_OnUpdateSO OriginSO => (C_SendTargetChanged_OnUpdateSO)base.OriginSO;
	
	private readonly Vector3IntEventChannelSO _targetChangedEC;
	private readonly InputCache _inputCache;
	private Vector3Int lastTargetPos;
	private bool vaildValue;

	private void CachePositionAndInvokeEvent(Vector3Int newPos) {
		lastTargetPos = newPos;
		_targetChangedEC.RaiseEvent(lastTargetPos);
	}
	
	public C_SendTargetChanged_OnUpdate(Vector3IntEventChannelSO targetChangedEC, InputCache inputCache) {
		_targetChangedEC = targetChangedEC;
		_inputCache = inputCache;
	}
	
	public override void Awake(StateMachine stateMachine) {}
	
	public override void OnUpdate() {
		var currentPos = _inputCache.cursor.abovePos.gridPos;
		if ( !vaildValue ) {
			vaildValue = true;
			CachePositionAndInvokeEvent(currentPos);
		}
		else {
			if ( !lastTargetPos.Equals(currentPos) ) {
				CachePositionAndInvokeEvent(currentPos);
			}
		}
	}
	
	public override void OnStateEnter() {
		vaildValue = false;
	}
	
	public override void OnStateExit() {
	}
}
