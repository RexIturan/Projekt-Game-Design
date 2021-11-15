using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * script attached to the model of a character. 
 * the model of a character will be stored by a PlayerType as a prefab
 * and this component will be instantiated and called by PlayerCharacterSC 
 * to play character animations
 */
public class CharacterAnimationController : MonoBehaviour
{
	Animator animator;

	private const int BASE_LAYER_INDEX = 0;
	private float clipLength; // in seconds
	private float timeSinceStart; // in seconds
	private bool newAnimation;

	// for stance
	private CharacterStanceController stanceController;

	// Start is called before the first frame update
	void Start()
    {
		animator = gameObject.GetComponentInChildren<Animator>();
		newAnimation = false;
		timeSinceStart = 0;
		clipLength = animator.GetCurrentAnimatorStateInfo(BASE_LAYER_INDEX).length;

		stanceController = gameObject.GetComponentInChildren<CharacterStanceController>();
    }

    // Update is called once per frame
    void Update()
	{
		// always refresh the animation clip length in case the animation wasn't already updated
		clipLength = animator.GetCurrentAnimatorStateInfo(BASE_LAYER_INDEX).length;

		// new state?
		if ( newAnimation )
		{
			newAnimation = false;
			timeSinceStart = 0;
		}
		else
		{
			// um Überlauf vorzubeugen
			if (timeSinceStart < clipLength)
				timeSinceStart += Time.deltaTime;
		}
	}

	public void PlayAnimation(AnimationType animation)
	{
		if ( animator )
		{
			Debug.Log("Spiele Animation ab: " + animation.ToString());
			switch ( animation )
			{
				case AnimationType.IDLE:
					animator.SetTrigger("idle");
					break;
				case AnimationType.DEATH_A:
					animator.SetTrigger("death_A");
					break;
				case AnimationType.DEATH_B:
					animator.SetTrigger("death_B");
					break;
				case AnimationType.TAKE_DAMAGE:
					animator.SetTrigger("take_damage");
					break;
				case AnimationType.RUN:
					animator.SetTrigger("run");
					break;
				case AnimationType.WALK:
					animator.SetTrigger("walk");
					break;
				case AnimationType.SHOOT_BOW:
					animator.SetTrigger("shoot_bow");
					break;
				case AnimationType.SHOOT_CROSBOW:
					animator.SetTrigger("shoot_crosbow");
					break;
				case AnimationType.ATTACK_STING:
					animator.SetTrigger("attack_sting");
					break;
				case AnimationType.ATTACK_SINGLE_R:
					animator.SetTrigger("attack_single_R");
					break;
				case AnimationType.CAST_A:
					animator.SetTrigger("cast_A");
					break;
				case AnimationType.CAST_B:
					animator.SetTrigger("cast_B");
					break;
			}
			newAnimation = true;
		}
		else
			Debug.LogWarning("Kein Animator ");
	}

	public bool IsAnimationInProgress()
	{
		return timeSinceStart < clipLength;
	}

	public void TakeStance(StanceType stance)
	{
		// stanceController.TakeStance(stance);
	}
}