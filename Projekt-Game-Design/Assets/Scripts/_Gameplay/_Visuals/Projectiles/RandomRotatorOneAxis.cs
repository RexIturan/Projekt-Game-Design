using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01
{
    public class RandomRotatorOneAxis : MonoBehaviour
    {
				[SerializeField] private Vector3 axis;
				[SerializeField] private float frequencyHZ;
				private float timer;

				private void Start() {
						timer = 0;
				}

				void Update()
        {
						timer += Time.deltaTime;
						if(timer >= 1f / frequencyHZ) {
								transform.Rotate(axis, 360 * Random.value, Space.Self);
								timer = 0;
						}
        }
    }
}
