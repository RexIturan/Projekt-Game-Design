using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour {

    [SerializeField] private UnitGridCombat[] unitGridCombat;
// todo remove its scuffed
    public GridDrawer gridContainer;


    private int currentUnitIndex = 0;

    private void NextActiveUnit() {
        if (currentUnitIndex + 1 == unitGridCombat.Length) {
            currentUnitIndex = 0;
        }
        else {
            currentUnitIndex += 1;
        }
    }
    
    private void Update() {
    
        // gridContainer.showPathsFrom((int) unitGridCombat[currentUnitIndex].transform.position.x, (int) unitGridCombat[currentUnitIndex].transform.position.z, unitGridCombat[currentUnitIndex].speed);
        //
        // if (Input.GetMouseButtonDown(0)) {
        //     // todo refactor
        //     Plane plane = new Plane(Vector3.up, 0);
        //     Vector3 worldPosition = new Vector3();
        //     float distance;
        //     Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //     if (plane.Raycast(ray, out distance))
        //     {
        //         worldPosition = ray.GetPoint(distance);
        //     }
        //     Debug.Log(Mathf.Floor(worldPosition.x) + " " + Mathf.Floor(worldPosition.z));
        //             
        //     int x = (int) Mathf.Floor(worldPosition.x);
        //     int y = (int) Mathf.Floor(worldPosition.z);
        //     
        //     unitGridCombat[currentUnitIndex].MoveTo(x, y);
        //     
        //     NextActiveUnit();
        // }
    }
}
