using System;
using UnityEngine;

/**
 * script attached to the model of a character. 
 * the model of a character will be stored by a PlayerType as a prefab
 * and this component will be instantiated and called by PlayerCharacterSC 
 * to play character animations
 */
public class CharacterAnimationController : MonoBehaviour {
	Animator animator;

	private const int BASE_LAYER_INDEX = 0;
	private float clipLength; // in seconds
	private float timeSinceStart; // in seconds
	private bool newAnimation;

	// for stance
	private CharacterStanceController stanceController;

	// for equipment
	private CharacterEquipmentController equipmentController;

	// for armor
	private CharacterModelController characterModelController;

	// Start is called before the first frame update
	void Start() {
		animator = gameObject.GetComponentInChildren<Animator>();
		newAnimation = false;
		timeSinceStart = 0;
		clipLength = animator.GetCurrentAnimatorStateInfo(BASE_LAYER_INDEX).length;

		stanceController = gameObject.GetComponentInChildren<CharacterStanceController>();
		equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();
		characterModelController = gameObject.GetComponentInChildren<CharacterModelController>();
	}

	// Update is called once per frame
	void Update() {
		// always refresh the animation clip length in case the animation wasn't already updated
		clipLength = animator.GetCurrentAnimatorStateInfo(BASE_LAYER_INDEX).length;

		// new state?
		if ( newAnimation ) {
			newAnimation = false;
			timeSinceStart = 0;
		}
		else {
			// um Überlauf vorzubeugen
			if ( timeSinceStart < clipLength )
				timeSinceStart += Time.deltaTime;
		}
	}

	public void PlayAnimation(CharacterAnimation characterAnimation) {
		if ( animator ) {
			// Debug.Log("Spiele Animation ab: " + characterAnimation.ToString());
			switch ( characterAnimation ) {
				case CharacterAnimation.IDLE:
					animator.SetTrigger("idle");
					break;
				case CharacterAnimation.DEATH_A:
					animator.SetTrigger("death_A");
					break;
				case CharacterAnimation.DEATH_B:
					animator.SetTrigger("death_B");
					break;
				case CharacterAnimation.TAKE_DAMAGE:
					animator.SetTrigger("take_damage");
					break;
				case CharacterAnimation.RUN:
					animator.SetTrigger("run");
					break;
				case CharacterAnimation.WALK:
					animator.SetTrigger("walk");
					break;
				case CharacterAnimation.SHOOT_BOW:
					animator.SetTrigger("shoot_bow");
					break;
				case CharacterAnimation.SHOOT_CROSBOW:
					animator.SetTrigger("shoot_crosbow");
					break;
				case CharacterAnimation.ATTACK_STING:
					animator.SetTrigger("attack_sting");
					break;
				case CharacterAnimation.ATTACK_SINGLE_R:
					animator.SetTrigger("attack_single_R");
					break;
				case CharacterAnimation.CAST_A:
					animator.SetTrigger("cast_A");
					break;
				case CharacterAnimation.CAST_B:
					animator.SetTrigger("cast_B");
					break;
			}

			newAnimation = true;
		}
		else
			Debug.LogWarning("Kein Animator ");
	}

	public bool IsAnimationInProgress() {
		return timeSinceStart < clipLength;
	}

	public void TakeStance(StanceType stance) {
		stanceController.TakeStance(stance);
	}

	public void ChangeEquipment(EquipmentPosition position, Mesh newMesh, Material material) {
	  if(!equipmentController)
		  equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();

	  if(!characterModelController )
		  characterModelController = gameObject.GetComponentInChildren<CharacterModelController>();

	  switch ( position ) {
		  case EquipmentPosition.LEFT:
		  case EquipmentPosition.RIGHT:
		  case EquipmentPosition.SHIELD:
			  equipmentController.ChangeEquipment(position, newMesh, material);
			  break;
		  case EquipmentPosition.HEAD:
		  case EquipmentPosition.BODY:
			  characterModelController.ChangeEquipment(position, newMesh);
			  break;
		  default:
			  throw new ArgumentOutOfRangeException(nameof(position), position, null);
	  }
	}

	public void ChangeWeaponPosition(EquipmentPosition position, WeaponPositionType weaponPosition) {
	  if(!equipmentController)
		  equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();

		equipmentController.ChangeWeaponPosition(position, weaponPosition);
	}

	public void DisableEquipment(EquipmentPosition position, bool disable) {
	  if(!equipmentController)
		  equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();

		equipmentController.DisableEquipment(position, disable);
	}

	public void SetStandardHead(Mesh mesh) {
	  if(!characterModelController )
		  characterModelController = gameObject.GetComponentInChildren<CharacterModelController>();

		characterModelController.SetStandardHead(mesh);
	}

	public void SetStandardBody(Mesh mesh) {
	  if(!characterModelController )
		  characterModelController = gameObject.GetComponentInChildren<CharacterModelController>();

		characterModelController.SetStandardBody(mesh);
	}

	public static float TimeUntilHit(CharacterAnimation animation) {
		float time = 0;
				switch ( animation )
				{
					// todo move this data to animation Data SO ?
						case CharacterAnimation.ATTACK_STING:
								time = 0.15f;
								break;
						case CharacterAnimation.ATTACK_SINGLE_R:
								time = 0.2f;
								break;
						case CharacterAnimation.CAST_A:
								time = 0.3f;
								break;
						case CharacterAnimation.CAST_B:
								time = 0.5f;
								break;
				}
				return time;
		}
}