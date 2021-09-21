using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryUIController : MonoBehaviour
{
    public List<InventorySlot> InventoryItems = new List<InventorySlot>();

    private VisualElement m_SlotContainer;
    // Für das Inventar
    private VisualElement inventoryContainer;

    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    
    [Header("Sending and Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityGameOverlayEventChannel;
    
    [Header("Sending Events On")]
    // Inputchannel für das Inventar
    [SerializeField] private VoidEventChannelSO enableInventoryInput;
    
    //Für das Inventar
    private enum inventoryTab
    {
        NONE,
        ITEMS,
        ARMORY,
        WEAPONS
    }
    
    
    private void Start()
    {
        
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        //Store the root from the UI Document component
        root = GetComponent<UIDocument>().rootVisualElement;
        
        inventoryContainer = root.Q<VisualElement>("InventoryOverlay");

        //Search the root for the SlotContainer Visual Element
        m_SlotContainer = root.Q<VisualElement>("InventoryContent");

        //Create InventorySlots and add them as children to the SlotContainer
        for (int i = 0; i < 28; i++)
        {
            InventorySlot item = new InventorySlot();

            InventoryItems.Add(item);

            m_SlotContainer.Add(item);
        }
        
        inventoryContainer.Q<Button>("Tab1").clicked += HandleItemTabPressed;
        inventoryContainer.Q<Button>("Tab2").clicked += HandleArmoryTabPressed;
        inventoryContainer.Q<Button>("Tab3").clicked += HandleWeaponsTabPressed;
    }
    
    
    private void Awake()
    {
        
        //VisibilityMenuEventChannel.OnEventRaised += SetMenuVisibility;
        //VisibilityInventoryEventChannel.OnEventRaised += SetInventoryVisibility;
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleInventoryOverlay;
        VisibilityGameOverlayEventChannel.OnEventRaised  += HandleOtherScreensOpened;
    }
    
    void resetAllTabs()
    {
        inventoryContainer.Q<Button>("Tab1").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab2").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab3").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab4").RemoveFromClassList("ClickedTab");
        inventoryContainer.Q<Button>("Tab5").RemoveFromClassList("ClickedTab");


        inventoryContainer.Q<Button>("Tab1").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab2").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab3").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab4").AddToClassList("UnclickedTab");
        inventoryContainer.Q<Button>("Tab5").AddToClassList("UnclickedTab");
    }

    void InventoryManager(inventoryTab tab, Button button)
    {
        resetAllTabs();
        button.RemoveFromClassList("UnclickedTab");
        button.AddToClassList("ClickedTab");

        switch (tab)
        {
            case inventoryTab.ITEMS:
                break;
            case inventoryTab.ARMORY:
                break;
            case inventoryTab.WEAPONS:
                break;
        }
    }
    
    
    
    void HandleItemTabPressed()
    {
        InventoryManager(inventoryTab.ITEMS, inventoryContainer.Q<Button>("Tab1"));
    }

    void HandleArmoryTabPressed()
    {
        InventoryManager(inventoryTab.ARMORY, inventoryContainer.Q<Button>("Tab2"));
    }

    void HandleWeaponsTabPressed()
    {
        InventoryManager(inventoryTab.WEAPONS, inventoryContainer.Q<Button>("Tab3"));
    }

    void HandleInventoryOverlay(bool value)
    {
        Debug.Log(value);

        if (value)
        {
            enableInventoryInput.RaiseEvent();
            inventoryContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            inventoryContainer.style.display = DisplayStyle.None;
            //VisibilityInventoryEventChannel.RaiseEvent(true);
        }
    }
    
    void HandleOtherScreensOpened(bool value)
    {
        HandleInventoryOverlay(false);
    }
}
