using UI.Components.Tooltip;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

[System.Serializable]
public class InventorySlot : VisualElement
{
		private ItemTooltip itemTooltip;
    public readonly Image icon;
    public readonly InventorySlotType slotType;
    public int inventoryItemID = -1; // ID within Inventory
		public ItemTypeSO itemType;
		public int slotId;
    
    public InventorySlot()
    {
        //Create a new Image element and add it to the root
        icon = new Image();
        Add(icon);
        //Add USS style properties to the elements
        icon.AddToClassList("slotIcon");
        AddToClassList("slotContainer");
        RegisterCallback<PointerDownEvent>(OnPointerDown);
        slotType = InventorySlotType.EquipmentInventory;

				// initialize tooltip
				itemTooltip = new ItemTooltip(this);
				itemTooltip.Deactivate();
    }
    
    public InventorySlot(InventorySlotType type, int id) : this()
    {
        // clicked += () => Debug.Log("click");
        //InventoryUIController.StartDrag(Mouse.current.position.ReadValue(), this);
        slotId = id;
        slotType = type;
    }
    
    public void HoldItem(ItemTypeSO itemType) {
				this.itemType = itemType;
        icon.image = itemType?.icon.texture;
        inventoryItemID = itemType ? itemType.id : -1;
				// Debug.Log("Test in HoldItem");

				if ( itemTooltip == null )
						Debug.Log("itemTooltip == null");
				else if ( itemType == null )
						Debug.Log("item == null");
				else
				{
						itemTooltip.UpdateValues(itemType);
						itemTooltip.Activate();
				}
    }

    public void DropItem()
    {
        inventoryItemID = -1;
        icon.image = null;
				itemType = null;
				// this.slotId = -1;

			  itemTooltip.Deactivate();
    }
    
    private void OnPointerDown(PointerDownEvent evt)
    {
        //Not the left mouse button OR noitem Or no slot
        if (inventoryItemID == -1 || slotId == -1 || evt.button != 0)
          return;
        
        //Clear the image
        icon.image = null;
        //Start the drag
        InventoryUIController.StartDrag(evt.position, this);
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


