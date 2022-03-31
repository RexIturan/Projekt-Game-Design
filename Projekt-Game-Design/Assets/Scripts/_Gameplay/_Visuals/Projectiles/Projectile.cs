using Audio;
using Events.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Visual
{
		public class Projectile : MonoBehaviour
		{
				[SerializeField] private SoundEventChannelSO playSoundEC;
				[SerializeField] private SoundSO impactSound;

				public float time;
				public float timeEnd = 1;
				public Vector3 start;
				public Vector3 end;
				[SerializeField] private bool onlyFall;
				[SerializeField] private float height;
				[SerializeField] private Vector3 lastPos;

				void Start()
				{
						time = 0;
						lastPos = start;
				}

				void Update() {
						time += Time.deltaTime;

						if ( time <= timeEnd ) {
								Vector3 newPos = CalculatePosition();
								gameObject.transform.position = newPos;
								gameObject.transform.rotation = Quaternion.LookRotation(newPos - lastPos);
								lastPos = newPos;
						}
						else { 
								playSoundEC.RaiseEvent(impactSound);
								Destroy(gameObject);
						}
				}

				private Vector3 CalculatePosition()
				{
						// portion of the distance already left behind
						float portion = time > 0 ? time / timeEnd : 0.0f;
						if( portion > 1.0f )
								portion = 1.0f;

						// linear position of the projectile right now
						Vector3 position = start + portion * ( end - start );

						if ( onlyFall )
						{
								// x = 0 => y = height
								// x = 1 => y = 0
								// => y = (1 - (x)2) * height
								position += Vector3.up * (1 - Mathf.Pow(portion, 2)) * height;
						}
						else {
								// y-position gain due to parable form:
								// y = (a(x - b)2 + 1) c | a: factor; b: offset on x-Axis (portion); c: height
								// x = 0 => y = 0 
								// x = 0.5 => y = c
								// x = 1 => y = 0
								// => y = (-4*(x - 0.5)2 + 1) c
								position += Vector3.up * ( -4 * Mathf.Pow(portion - 0.5f, 2) + 1 ) * height;
						}

						return position;
				}
		}
}