using Audio;
using Events.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
		public class BeamProjectile : MonoBehaviour
		{
				public float timeEnd = 1; // time to live for the beam 
				public Vector3 start;
				public Vector3 end;

				void Start()
				{
						InitTransform();
						Destroy(gameObject, timeEnd);
				}

				private void InitTransform()
				{
						transform.position = start;
						transform.rotation = Quaternion.LookRotation(end - start);
						Vector3 oldScale = transform.localScale;
						transform.localScale = new Vector3(oldScale.x, oldScale.y, (end - start).magnitude);
				}
		}
}