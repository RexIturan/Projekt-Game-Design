using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Field", menuName = "Field Data", order = 51)]
public class FieldData : ScriptableObject
{
        public int rows = 10;
    
        public int cols = 10;
    
        public int scale = 1;

        public Vector3 leftBottomLocation = new Vector3(0, 0, 0);

        public int numberOfRockyFields = 3;
        
        public int numberOfForestFields = 6;
        public int numberOfWaterFields = 10;
        
        public Material forest;
        public Material rocks;
        public Material water;
        public Material defaultMaterial;
}
