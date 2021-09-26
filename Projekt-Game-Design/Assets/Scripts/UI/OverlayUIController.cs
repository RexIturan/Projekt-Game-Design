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

    [Header("Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityGameOverlayEventChannel;
    [SerializeField] private BoolEventChannelSO VisibilityInventoryEventChannel;

    [Header("Sending Events On")]
    [SerializeField] private VoidEventChannelSO enableGamplayInput;
    
    [Header("Sending and Receiving Events On")]
    [SerializeField] private BoolEventChannelSO VisibilityMenuEventChannel;

    // Start is called before the first frame update
    void Start()
    {
        // Holen des UXML Trees, zum getten der einzelnen Komponenten
        var root = GetComponent<UIDocument>().rootVisualElement;
        overlayContainer = root.Q<VisualElement>("OverlayContainer");
        overlayContainer.Q<Button>("IngameMenuButton").clicked += ShowMenu;
    }

    private void Awake()
    {
        VisibilityMenuEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityInventoryEventChannel.OnEventRaised += HandleOtherScreensOpened;
        VisibilityGameOverlayEventChannel.OnEventRaised  += HandleGameOverlay;
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

    void HandleOtherScreensOpened(bool value)
    {
        HandleGameOverlay(false);
    }

    void ShowMenu()
    {
        //OverlayManager(screenOverlay.INGAME_MENU);
        //enableGamplayInput.RaiseEvent();
        //overlayContainer.style.display = DisplayStyle.Flex;
        VisibilityMenuEventChannel.RaiseEvent(true);
    }
}