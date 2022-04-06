using Events.ScriptableObjects;
using UnityEngine;

namespace Visual
{
		public class ProjectileSpawner : MonoBehaviour
		{
				[SerializeField] private CreateProjectileEventChannelSO createProjectileEvent;

				private void Awake()
				{
						createProjectileEvent.OnEventRaised += SpawnProjectile;
				}

				private void OnDestroy()
				{
						createProjectileEvent.OnEventRaised -= SpawnProjectile;
				}

				private void SpawnProjectile(Vector3 start, Vector3 end, float time, GameObject projectilePrefab)
				{
						GameObject newProjectile = Instantiate(projectilePrefab, start, Quaternion.identity);

						Projectile projectile = newProjectile.GetComponent<Projectile>();

						if ( projectile != null ) {
								projectile.start = start;
								projectile.end = end;
								projectile.timeEnd = time;
						}
						else { 
								BeamProjectile beamProjectile = newProjectile.GetComponent<BeamProjectile>();

								if(beamProjectile) {
										beamProjectile.start = start;
										beamProjectile.end = end;
										beamProjectile.timeEnd = time;
								}
								else {
										Debug.LogError("No projectile script in Projectile GameObject. ");
										Destroy(newProjectile);
								}
						}
				}
		}
}