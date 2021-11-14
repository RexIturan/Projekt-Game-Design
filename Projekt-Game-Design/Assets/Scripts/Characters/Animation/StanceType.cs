using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// possible character stances
[System.Serializable, System.Flags]
public enum StanceType
{
	// note: RIGHT_UP and RIGHT_OVER_SHOULDER are mutually exclusive 
	// if both flags are set, it will be treated as RIGHT_OVER_SHOULDER
	RIGHT_UP = 1,
	RIGHT_OVER_SHOULDER = 2,
	LEFT_UP = 4
}
