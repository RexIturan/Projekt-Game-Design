using System;
using UnityEngine;

/// <summary>
/// Attached to a game object of an item saves reference to its scriptable Object
/// </summary>
public class Item : MonoBehaviour {
    [SerializeField] public ItemSO itemSO;

    private MeshRenderer _meshRenderer;
    private MeshFilter _meshFilter;

    private void Awake() {
	    _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
	    _meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
    }

    public void InitItem(ItemSO itemData) {
	    _meshRenderer = gameObject.GetComponentInChildren<MeshRenderer>();
	    _meshFilter = gameObject.GetComponentInChildren<MeshFilter>();
	    
	    this.itemSO = itemData;
	    _meshRenderer.material = itemData.material;
	    _meshFilter.mesh = itemData.mesh;
    }

    public void Reset() {
	    InitItem(itemSO);
    }
}
