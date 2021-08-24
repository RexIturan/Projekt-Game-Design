using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class MiniMap : MonoBehaviour
{
    public Transform player;

    private void Start()
    {
        GameEvents.current.onPlayerClicked += setTransform;
    }

  /*  void Update()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        float value = 0;
        value -= scroll * 20f * 100f * Time.deltaTime;
        Camera cam = Camera.main;
        cam.orthographicSize = value;
    }*/

    private void LateUpdate()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;
    }

    private void setTransform(GameObject obj)
    {
        player = obj.transform;
    }
}