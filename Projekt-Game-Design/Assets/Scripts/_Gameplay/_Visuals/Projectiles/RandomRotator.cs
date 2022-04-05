using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GDP01
{
    public class RandomRotator : MonoBehaviour
    {
				[Header("Degrees per second: ")]
				[SerializeField] private float velocity;
				private Vector3 rotationDirection; // direction the model rotates into 
				private Vector3 changeDirection; // the direction the rotationDirection changes into (more or less equivalent to its derivative)
				[SerializeField] private float changingFactor = 0.01f;
				[SerializeField] private float changingChangingFactor = 0.1f;

        // Start is called before the first frame update
        void Start()
        {
						rotationDirection = new Vector3(Random.value, Random.value, Random.value).normalized;
						changeDirection = new Vector3(Random.value, Random.value, Random.value).normalized;
				}

        // Update is called once per frame
        void Update()
        {
						transform.Rotate(rotationDirection, velocity * Time.deltaTime);

						changeDirection += new Vector3(Random.value, Random.value, Random.value).normalized * changingChangingFactor;
						changeDirection = changeDirection.normalized;

						rotationDirection += (changeDirection * changingFactor).normalized;
        }
    }
}
