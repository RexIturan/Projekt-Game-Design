using System;
using UnityEngine;
using WorldObjects;

namespace WorldObjects {
	/// <summary>
	/// Attached to a game object of an item saves reference to its scriptable Object
	/// </summary>
	public partial class ItemComponent : WorldObject.Factory<ItemComponent, ItemComponent.ItemData> {
		
		[Header("Item ComponentPart")]
		[SerializeField] private Transform modelTransform;

		private MeshRenderer _meshRenderer;
		private MeshFilter _meshFilter;

		//todo make available from unity
		[SerializeField] private Vector3 _armorRotationOffset = new Vector3(-90, 0, 0);
		[SerializeField] private Vector3 _armorPositionOffset = new Vector3(0, 0, 0);
		[SerializeField] private Vector3 _defaultRotationOffset = new Vector3(0, 0, 0);
		[SerializeField] private Vector3 _defaultPositionOffset = new Vector3(0, 0.25f, 0);

		// [SerializeField] protected new ItemSO _type;
		public new ItemSO Type {
			get => ( ItemSO )_type;
			set => _type = value;
		}
		
		[SerializeField] private ItemData _itemData;
		
		private void Awake() {
			_meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
			_meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
		}

		public void InitItem(ItemSO itemSO) {
			_meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
			_meshFilter = gameObject.GetComponentInChildren<MeshFilter>();

			Type = itemSO;

			if ( Type is BodyArmorSO ) {
				modelTransform.localRotation = Quaternion.Euler(_armorRotationOffset);
				modelTransform.localPosition = _armorPositionOffset;
				// Debug.Log("armor/head");
			}
			else if ( Type is HeadArmorSO ) {
				modelTransform.localRotation = Quaternion.Euler(_armorRotationOffset);
				modelTransform.localPosition = new Vector3(0, -0.5f, 0);
			}
			else {
				modelTransform.localRotation = Quaternion.Euler(_defaultRotationOffset);
				modelTransform.localPosition = _defaultPositionOffset;
				// Debug.Log("other item");
			}

			_meshRenderer.material = itemSO.material;
			_meshFilter.mesh = itemSO.mesh;
		}

		public void Reset() {
			InitItem(Type);
		}

		private void OnValidate() {
			InitItem(Type);
		}
	}
}