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
	private CharacterBodyController bodyController;

	// Start is called before the first frame update
	void Start() {
		animator = gameObject.GetComponentInChildren<Animator>();
		newAnimation = false;
		timeSinceStart = 0;
		clipLength = animator.GetCurrentAnimatorStateInfo(BASE_LAYER_INDEX).length;

		stanceController = gameObject.GetComponentInChildren<CharacterStanceController>();
		equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();
		bodyController = gameObject.GetComponentInChildren<CharacterBodyController>();
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

	public void ChangeEquipment(EquipmentPosition position, Mesh newMesh) {
	  if(!equipmentController)
		  equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();

	  if(!bodyController )
		  bodyController = gameObject.GetComponentInChildren<CharacterBodyController>();

		if(position.Equals(EquipmentPosition.LEFT) || position.Equals(EquipmentPosition.RIGHT) || position.Equals(EquipmentPosition.SHIELD)) { 
			equipmentController.ChangeEquipment(position, newMesh);
		}
		else if (position.Equals(EquipmentPosition.HEAD) || position.Equals(EquipmentPosition.BODY)) { 
			bodyController.ChangeEquipment(position, newMesh);
		}
	}

	public void ChangeWeaponPosition(EquipmentPosition position, WeaponPositionType newPosition) {
	  if(!equipmentController)
		  equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();

		equipmentController.ChangeWeaponPosition(position, newPosition);
	}

	public void DisableEquipment(EquipmentPosition position, bool disable) {
	  if(!equipmentController)
		  equipmentController = gameObject.GetComponentInChildren<CharacterEquipmentController>();

		equipmentController.DisableEquipment(position, disable);
	}

	public void SetStandardHead(Mesh mesh) {
	  if(!bodyController )
		  bodyController = gameObject.GetComponentInChildren<CharacterBodyController>();

		bodyController.SetStandardHead(mesh);
	}

	public void SetStandardBody(Mesh mesh) {
	  if(!bodyController )
		  bodyController = gameObject.GetComponentInChildren<CharacterBodyController>();

		bodyController.SetStandardBody(mesh);
	}
}