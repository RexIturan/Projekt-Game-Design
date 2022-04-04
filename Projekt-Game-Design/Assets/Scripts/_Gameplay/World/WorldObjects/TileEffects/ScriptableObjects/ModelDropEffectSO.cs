using Grid;
using UnityEngine;
using Visual;

namespace GDP01.TileEffects
{
		/// <summary>
		/// Effect that spawns a prefab to fall onto the groud 
		/// </summary>
		[System.Serializable]
		[CreateAssetMenu(fileName = "te_ModelDropEffect", menuName = "WorldObjects/TileEffects/Model Drop Effect")]
    public class ModelDropEffectSO : TileEffectSO
		{
				[SerializeField] private GameObject prefab;
				[SerializeField] private float fallingTime; // how long it takes the model to fall

				/// <summary>
				/// Spawns a model that drops on the ground and then disappears. 
				/// </summary>
				/// <param name="tileEffectController">TileEffect component that has this effect </param>
				override public void OnAction(TileEffectController tileEffectController) {
						GameObject currentlyFalling = Instantiate(prefab, tileEffectController.transform);
						Projectile projectile = currentlyFalling.GetComponent<Projectile>();

						projectile.start = currentlyFalling.transform.position;
						projectile.end = currentlyFalling.transform.position;
						projectile.timeEnd = fallingTime;
				}
    }
}
