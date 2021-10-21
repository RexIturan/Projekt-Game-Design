using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Enemy/TriggerAnimation")]
public class E_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private string animation;

    public override StateAction CreateAction() => new E_TriggerAnimation_OnEnter(animation);
}

public class E_TriggerAnimation_OnEnter : StateAction {
    private Animator _animator;
    private readonly string _animation;

    public E_TriggerAnimation_OnEnter(string animation) {
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
            Debug.LogWarning("Animator in GameObject not found. ");
    }
}
