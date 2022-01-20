using System;
using Events.ScriptableObjects.Core;
using UnityEngine;

namespace Events.ScriptableObjects
{
		/// <summary>
		/// makes projectile spawner spawn a projectile
		/// prefab should have projectile monobehaviour script
		/// </summary>
		// TODO rename
		[CreateAssetMenu(menuName = "Events/Visual/CreateProjectileEventChannel")]
		public class CreateProjectileEventChannelSO : EventChannelBaseSO {
			
			//todo prat as spawn data
				public event Action<Vector3, Vector3, float, GameObject> OnEventRaised;

				public void RaiseEvent(Vector3 start, Vector3 end, float time, GameObject projectilePrefab)
				{
						if ( OnEventRaised != null )
								OnEventRaised.Invoke(start, end, time, projectilePrefab);
				}
		}
}
