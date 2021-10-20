using Events.ScriptableObjects;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;
using StateMachine = UOP1.StateMachine.StateMachine;

[CreateAssetMenu(fileName = "e_TriggerAnimation_OnEnter", menuName = "State Machines/Actions/Enemy/TriggerAnimation")]
public class e_TriggerAnimation_OnEnterSO : StateActionSO {
    [SerializeField] private string animation;

    public override StateAction CreateAction() => new e_TriggerAnimation_OnEnter(animation);
}

public class e_TriggerAnimation_OnEnter : StateAction {
    private Animator animator;
    private string animation;

    public e_TriggerAnimation_OnEnter(string animation) {
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
