﻿using System;
using Grid;
using UnityEngine;

namespace Events.ScriptableObjects.FieldOfView
{
    [CreateAssetMenu(fileName = "FieldOfViewQueryEC", menuName = "Events/FOV/Field Of View Query EventChannel", order = 0)]
    public class FieldOfViewQueryEventChannelSO : EventChannelBaseSO
    {
        // grid pos, range, blocking, callback
        public event Action<Vector3Int, int, ETileFlags, Action<bool[,]>> OnEventRaised;

        public void RaiseEvent(Vector3Int startNode, int range, ETileFlags blocking, Action<bool[,]> callback) {
            OnEventRaised?.Invoke(startNode, range, blocking, callback);
        }

    }
}