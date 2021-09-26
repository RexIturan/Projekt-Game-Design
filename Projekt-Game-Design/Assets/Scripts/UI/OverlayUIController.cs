using System;
using System.Collections;
using System.Collections.Generic;
using Events.ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class OverlayUIController : MonoBehaviour
{
    // Für die UI Elemente
    private VisualElement overlayContainer;
    
    //Action Container
    private VisualElement ActionContainer;

    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityGameOverlayEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityActionContainerEventChannel;

    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO enableGamplayInput;
    
    [Header("Sending and Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;

    // Start is called before the first frame update
    void Start()
    {
        
        // overlayContainer = root.Q<VisualElement>("OverlayContainer");
        // overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        
    }

    private void Awake()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        ActionContainer = root.Q<VisualElement>("ActionContainer");
        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityGameOverlayEventChannel.OnEventRaised  += HandleGameOverlay;
        VisibilityActionContainerEventChannel.OnEventRaised += HandleActionMenuVisibility;
    }

    void HandleGameOverlay(bool value)
    {
        if (value)
        {
            enableGamplayInput.RaiseEvent();
            overlayContainer.style.display = DisplayStyle.Flex;
        }
        else
        {
            overlayContainer.style.display = DisplayStyle.None;
        }
        
    }
    
    void HandleActionMenuVisibility(bool value)
    {
        if (value)
        {
            ShowActionMenu();
        }
        else
        {
            HideActionMenu();
        }
        
    }
    void HandleOtherScreensOpened(bool value)
    {
        HandleGameOverlay(false);
    }

    void ShowMenu()
    {
        VisibilityMenuEventChannel.RaiseEvent(true);
    }

    void HideActionMenu()
    {
        ActionContainer.style.display = DisplayStyle.None;
    }

    void ShowActionMenu()
    {
        ActionContainer.style.display = DisplayStyle.Flex;
    }
    
    
}