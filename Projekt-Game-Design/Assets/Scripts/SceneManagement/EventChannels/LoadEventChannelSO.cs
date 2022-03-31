﻿using System;
using Events.ScriptableObjects.Core;
using SceneManagement.ScriptableObjects;
using UnityEngine;


    /// <summary>
    /// This class is a used for scene loading events.
    /// Takes an array of the scenes we want to load and a bool to specify if we want to show a loading screen.
    /// </summary>
    [CreateAssetMenu(menuName = "Events/Load Event Channel")]
    public class LoadEventChannelSO : EventChannelBaseSO {
	    
        public event Action<GameSceneSO[], bool, bool> OnLoadingRequested;

        public void RaiseEvent(GameSceneSO[] locationsToLoad, bool showLoadingScreen = false, bool closeLoadingScreenOnSceneReady = true)
        {
            if (OnLoadingRequested != null)
            {
                OnLoadingRequested.Invoke(locationsToLoad, showLoadingScreen, closeLoadingScreenOnSceneReady);
            }
            else
            {
                Debug.LogWarning("A Scene loading was requested, but nobody picked it up. " +
                                 "Check why there is no SceneLoader already present, " +
                                 "and make sure it's listening on this Load Event channel.");
            }
        }
    }
