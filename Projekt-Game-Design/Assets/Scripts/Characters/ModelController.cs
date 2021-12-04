using UnityEngine;

namespace Characters {
	public class ModelController : MonoBehaviour {

		[SerializeField] private GameObject model;
		
		//old init
		public PlayerTypeSO playerType;
		
		//todo get equiped items and show them 
		//todo wrap in scriptable object
		public GameObject prefab;
		public Mesh mesh;
		public Material material;
		public Sprite image;

		// create model
		private void InitModel() {
			// create model
			model = Instantiate(playerType.model, transform);

			// save animation controller
			// animationController = model.GetComponent<CharacterAnimationController>();
		}
	}
}