using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.UpdateHelper {
		/// <summary>
		/// UpdateHelper is a Singelton MonoBehaviour
		/// <para></para>
		/// Class to make the update method available for non-MonoBehaviour classes. 
		/// </summary>
		public class UpdateHelper : MonoBehaviour {

			#region Monobehaviour Singelton

			private static UpdateHelper instance;
			public static UpdateHelper Current => instance; 
			
			private void Awake() {
				if ( instance == null ) {
					instance = this;
				}
				else {
					Debug.LogWarning("There can only be one UpdateHelper!");
					Destroy(gameObject);
				}
			}

			private void OnDestroy() {
				instance = null;
			}

			#endregion
			

			private List<UpdatedClass> subscribedInstances;

				public UpdateHelper()
				{
						subscribedInstances = new List<UpdatedClass>();
				}

				void Update()
				{
						foreach(UpdatedClass updatedClass in subscribedInstances)
						{
								updatedClass.Update();
						}
				}

				public void Subscribe(UpdatedClass subscribedInstance)
				{
						subscribedInstances.Add(subscribedInstance);
				}

				public void Unsubscribe(UpdatedClass subscribedInstance)
				{
						subscribedInstances.Remove(subscribedInstance);
				}

				public static UpdateHelper FindInstance()
				{
						return FindObjectOfType<UpdateHelper>();
				}
		}
}