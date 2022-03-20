using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util.UpdateHelper {
		/// <summary>
		/// Class to make the update method available for non-MonoBehaviour classes. 
		/// </summary>
		public class UpdateHelper : MonoBehaviour
		{
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

				public static UpdateHelper FindInstance()
				{
						return FindObjectOfType<UpdateHelper>();
				}
		}
}