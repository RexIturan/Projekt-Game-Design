using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Player/TriggerAnimation")]
public class P_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private string animation;

    public override StateAction CreateAction() => new P_TriggerAnimation_OnEnter(animation);
}

public class P_TriggerAnimation_OnEnter : StateAction {
    private Animator _animator;
    private string _animation;

    public P_TriggerAnimation_OnEnter(string animation) {
        this._animation = animation;
    }

    public override void OnUpdate() {
    }

    public override void Awake(StateMachine stateMachine) {
        _animator = stateMachine.gameObject.GetComponentInChildren<Animator>();
    }

    public override void OnStateEnter() {
        if (_animator) {
            Debug.Log("Try to play animation: " + _animation);
            _animator.SetTrigger(_animation);
        }
        else
            Debug.LogWarning("Animator in gameobject not found. ");
    }
}
