using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterStanceController : MonoBehaviour {
	private TwoBoneIKConstraint rightUp = null;
	private TwoBoneIKConstraint rightOverShoulder = null;
	private TwoBoneIKConstraint leftUp = null;

	void Start() {
		// find Components
		foreach ( TwoBoneIKConstraint constraint in gameObject
			.GetComponentsInChildren<TwoBoneIKConstraint>() ) {
			if ( constraint.tag == "Stance/RightUp" )
				rightUp = constraint;
			else if ( constraint.tag == "Stance/RightOverShoulder" )
				rightOverShoulder = constraint;
			else if ( constraint.tag == "Stance/LeftUp" )
				leftUp = constraint;
		}
	}

	public void TakeStance(StanceType stanceType) {
		// right arm
		//
		if ( ( stanceType & StanceType.RIGHT_OVER_SHOULDER ) == StanceType.RIGHT_OVER_SHOULDER ) {
			rightOverShoulder.weight = 1.0f;
			rightUp.weight = 0f;
		}
		else if ( ( stanceType & StanceType.RIGHT_UP ) == StanceType.RIGHT_UP ) {
			rightUp.weight = 1.0f;
			rightOverShoulder.weight = 0f;
		}
		else {
			rightUp.weight = 0f;
			rightOverShoulder.weight = 0f;
		}

		// left arm
		//
		if ( ( stanceType & StanceType.LEFT_UP ) == StanceType.LEFT_UP ) {
			leftUp.weight = 1.0f;
		}
		else
			leftUp.weight = 0f;
	}
}