using System;
using UnityEngine;

/// <summary>
/// Attached to a game object of an item saves reference to its scriptable Object
/// </summary>
public class Item : MonoBehaviour {
    [SerializeField] public ItemSO itemSO;
    [SerializeField] private Transform modelTransform;
    
    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;
    private Vector3 _armorRotationOffset = new Vector3(-90, 0, 0);
    private Vector3 _armorPositionOffset = new Vector3(0, 0, 0);
    private Vector3 _defaultRotationOffset = new Vector3(0, 0, -90);
    private Vector3 _defaultPositionOffset = new Vector3(0, 0.25f, 0);
    
    private void Awake() {
	    _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
	    _meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
    }

    public void InitItem(ItemSO itemData) {
	    _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
	    _meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
	    
	    this.itemSO = itemData;
	    
	    var pos = modelTransform.position; 
	    var localPos = modelTransform.localPosition;
	    
	    if ( itemSO is BodyArmorSO) {
		    modelTransform.localRotation = Quaternion.Euler(_armorRotationOffset);
		    modelTransform.localPosition = _armorPositionOffset;
		    // Debug.Log("armor/head");
	    } else if (itemSO is HeadArmorSO) {
		    modelTransform.localRotation = Quaternion.Euler(_armorRotationOffset);
		    modelTransform.localPosition = new Vector3(0, -0.5f, 0);
	    }
	    else {
		    modelTransform.localRotation = Quaternion.Euler(_defaultRotationOffset);
		    modelTransform.localPosition = _defaultPositionOffset;
		    // Debug.Log("other item");
	    }
	    
	    _meshRenderer.material = itemData.material;
	    _meshFilter.mesh = itemData.mesh;
    }

    public void Reset() {
	    InitItem(itemSO);
    }
}
