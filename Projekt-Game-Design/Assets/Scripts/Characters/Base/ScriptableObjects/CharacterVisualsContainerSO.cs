using System;
using System.Collections.Generic;
using Characters;
using UnityEngine;

[Serializable]
public struct StringMeshElement {
	public string name;
	public Mesh mesh;
}

[Serializable]
public struct FactionMaterialMapping {
	public Faction faction;
	public Material material;
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
    
    //
    public List<FactionMaterialMapping> factionMaterial = new List<FactionMaterialMapping>();
}
