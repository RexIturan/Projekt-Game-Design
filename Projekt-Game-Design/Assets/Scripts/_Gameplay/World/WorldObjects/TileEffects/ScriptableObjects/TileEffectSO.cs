using UnityEngine;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Defines an action a Tile Effect has. 
		/// Is called by the corresponding TileEffectController. 
		/// </summary>
    public abstract class TileEffectSO : ScriptableObject
		{
				public bool actionOnEvaluate;
				public bool actionOnEnter;
				public bool actionOnExit;

				abstract public void OnAction(TileEffectController tileEffectController);
    }
}
