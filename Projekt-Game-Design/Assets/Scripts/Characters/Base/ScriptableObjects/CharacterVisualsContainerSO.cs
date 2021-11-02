using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct StringMeshElement {
	public string name;
	public Mesh mesh;
}

[CreateAssetMenu(fileName = "newCharacterVisualsContainerSO", menuName = "Art/Character Visuals Container", order = 0)]
public class CharacterVisualsContainerSO : ScriptableObject
{
    //character mesh
    // heads
    public List<StringMeshElement> headsReference = new List<StringMeshElement>();

    // bodys
    public List<StringMeshElement> bodysReference = new List<StringMeshElement>();
    
    // material
    public List<Material> charMaterial = new List<Material>();
}
