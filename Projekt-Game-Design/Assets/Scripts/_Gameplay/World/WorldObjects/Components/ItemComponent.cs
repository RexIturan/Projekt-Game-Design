using System;
using System.Collections.Generic;
using Characters;
using Characters.Types;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using Grid;
using UnityEngine;

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
		[SerializeField] private PositionGameObjectEventChannelSO onTileEnterEC;
		[SerializeField] private IntEventChannelSO pickupEC;
		[SerializeField] private VoidEventChannelSO redrawLevelEC;
		
		// [SerializeField] protected new ItemSO _type;
		public new ItemSO Type {
			get => ( ItemSO )_type;
			set => _type = value;
		}
		
		[SerializeField] private ItemData _itemData;
		
		private GridController _gridController;

		private void Awake() {
			_meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
			_meshFilter = gameObject.GetComponentInChildren<MeshFilter>();

			//todo remove temporary
			// GridPosition = Vector3Int.FloorToInt(transform.position);
		}

		public void InitItem(ItemSO itemSO, Vector3Int gridPosition) {
			
			_meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
			_meshFilter = gameObject.GetComponentInChildren<MeshFilter>();

			GridPosition = gridPosition;
			_gridTransform.MoveToGridPosition();
			
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

		private void HandleOnTileEnter(Vector3Int position, GameObject characterObject) {
			if ( _gridTransform.gridPosition == position && 
			     characterObject.GetComponent<Statistics>().Faction == Faction.Player ) {
				
				if(!_gridController)
					_gridController = GridController.FindGridController();

				if(!_gridController)
					Debug.LogError("Could not find Grid Controller. ");
				else { 
					// Debug.Log("Searching for items at: " + _gridTransform.gridPosition.x + ", " + _gridTransform.gridPosition.z);
					List<int> items = _gridController.GetItemsAtGridPos(GridPosition);

					if(items.Count > 0) {
						// Debug.Log("Item found ");
						foreach(int itemId in items) 
							pickupEC.RaiseEvent(itemId);

						_gridController.RemoveAllItemsAtGridPos(_gridTransform.gridPosition);
						redrawLevelEC.RaiseEvent();
					}
				}
			}
		}
		
///// Unity Functions //////////////////////////////////////////////////////////////////////////////

		private void OnEnable() {
			onTileEnterEC.OnEventRaised += HandleOnTileEnter;
		}

		private void OnDisable() {
			onTileEnterEC.OnEventRaised -= HandleOnTileEnter;
		}

		public void Reset() {
			InitItem(Type, GridPosition);
		}

		private void OnValidate() {
			InitItem(Type, GridPosition);
		}
	}
}