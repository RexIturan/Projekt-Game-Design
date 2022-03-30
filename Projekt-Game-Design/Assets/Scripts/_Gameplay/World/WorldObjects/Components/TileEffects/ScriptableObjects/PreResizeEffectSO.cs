using UnityEngine;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Effect resizes the tile effect based on the time until activation. 
		/// </summary>
		[System.Serializable]
		[CreateAssetMenu(fileName = "te_ResizeEffect", menuName = "WorldObjects/TileEffects/Resize Effect")]
    public class PreResizeEffectSO : TileEffectSO
		{
				/// <summary>
				/// Resizes the tile effect game object according to the time until activation. 
				/// </summary>
				/// <param name="tileEffectController">TileEffect component that has this effect </param>
				override public void OnAction(TileEffectController tileEffectController) {
						float newScaleFactor = 1.0f / Mathf.Max(1, tileEffectController.GetTimeUntilActivation() + 1);
						tileEffectController.gameObject.transform.Find("model").localScale = Vector3.one * newScaleFactor;
				}
    }
}
