using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[System.Serializable]
public class InventorySlot : VisualElement
{
    public readonly Image icon;
    public readonly InventorySlotType slotType;
    public int inventoryItemID = -1; // ID within Inventory
		public ItemSO item;
    
    public InventorySlot() : this(InventorySlotType.EquipmentInventory){}
    
    public InventorySlot(InventorySlotType type) {
        //Create a new Image element and add it to the root
        icon = new Image();
        Add(icon);
        //Add USS style properties to the elements
        icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
        RegisterCallback<MouseDownEvent>(OnPointerDown);
        // clicked += () => Debug.Log("click");//InventoryUIController.StartDrag(Mouse.current.position.ReadValue(), this);
        slotType = type;
    }
    
    public void HoldItem(ItemSO item)
    {
				this.item = item;
        icon.image = item.icon.texture;
        inventoryItemID = item ? item.id : -1;
        // Debug.Log("Test in HoldItem");
    }

    public void DropItem()
    {
        inventoryItemID = -1;
        icon.image = null;
				item = null;
    }
    
    private void OnPointerDown(EventBase evt) {
	    Debug.Log("down");
	    
	    if ( evt is PointerDownEvent ptr ) {
		    //Not the left mouse button
		    if (inventoryItemID == -1 || ptr.button != 0)
		    {
			    return;
		    }
		    //Clear the image
		    icon.image = null;
		    //Start the drag
		    InventoryUIController.StartDrag(ptr.position, this);
	    }
	    if ( evt is MouseDownEvent mde ) {
		    //Not the left mouse button
		    if (inventoryItemID == -1 || mde.button != 0)
		    {
			    return;
		    }
		    //Clear the image
		    icon.image = null;
		    //Start the drag
		    InventoryUIController.StartDrag(mde.mousePosition, this);
	    }
    }

    public enum InventorySlotType
    {
        None,
        NormalInventory,
        EquipmentInventory,
        Trashcan
    }
    
    #region UXML
    [Preserve]
    public new class UxmlFactory : UxmlFactory<InventorySlot, UxmlTraits> { }
    [Preserve]
    public new class UxmlTraits : VisualElement.UxmlTraits { }
    #endregion
}


