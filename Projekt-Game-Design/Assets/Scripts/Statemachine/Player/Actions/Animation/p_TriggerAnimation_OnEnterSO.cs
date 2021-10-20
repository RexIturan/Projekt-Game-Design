using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "p_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Player/TriggerAnimation")]
public class p_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private string animation;

    public override StateAction CreateAction() => new p_TriggerAnimation_OnEnter(animation);
}

public class p_TriggerAnimation_OnEnter : StateAction {
    private Animator animator;
    private string animation;

    public p_TriggerAnimation_OnEnter(string animation) {
        this.animation = animation;
    }

    public override void OnUpdate() {
    }

    public override void Awake(StateMachine stateMachine) {
        animator = stateMachine.gameObject.GetComponentInChildren<Animator>();
    }

    public override void OnStateEnter() {
        if (animator) {
            Debug.Log("Try to play animation: " + animation);
            animator.SetTrigger(animation);
        }
        else
            Debug.LogWarning("Animator in gameobject not found. ");
    }
}
