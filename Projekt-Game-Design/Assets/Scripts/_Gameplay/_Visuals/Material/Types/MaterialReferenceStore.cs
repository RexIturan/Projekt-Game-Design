using System;
using System.Collections.Generic;
using Characters;
using Characters.Types;
using UnityEngine;

public enum EMaterialColor {
	Black,
	Blue,
	DarkGreen,
	Green,
	Red,
	Tan,
	White,
	Yellow
}

[Serializable]
public struct CompositeMaterialMapping {
	public EMaterialColor matColor;
	public Material material;
}

[Serializable]
public struct StringMeshElement {
	public string name;
	public Mesh mesh;
}

[Serializable]
public struct FactionMaterialMapping {
	public Faction faction;
	public EMaterialColor matColor;
}

[CreateAssetMenu(fileName = "newCharacterVisualsContainerSO", menuName = "Art/Character Visuals Container", order = 0)]
public class MaterialReferenceStore : ScriptableObject {

	// material
  public List<CompositeMaterialMapping> compositeMaterials = new List<CompositeMaterialMapping>();
    
  //
  public List<FactionMaterialMapping> factionMaterials = new List<FactionMaterialMapping>();

///// private Variables

  private readonly Dictionary<Faction, Material> factionMaterialDict = new Dictionary<Faction, Material>();
  private readonly Dictionary<EMaterialColor, Material> matColorMaterialDict = new Dictionary<EMaterialColor, Material>();

///// Private Functions ////////////////////////////////////////////////////////////////////////////

///// Public Functions /////////////////////////////////////////////////////////////////////////////

	public Material GetMaterial(Faction faction) {
		return factionMaterialDict[faction];
	}
	
	public Material GetMaterial(EMaterialColor matColor) {
		return matColorMaterialDict[matColor];
	}
	
///// Unity Functions //////////////////////////////////////////////////////////////////////////////
  
  private void OnEnable() {
	  factionMaterialDict.Clear();
	  matColorMaterialDict.Clear();
	  
	  foreach ( var compositeMaterial in compositeMaterials ) {
			matColorMaterialDict.Add(compositeMaterial.matColor, compositeMaterial.material);  
	  }

	  foreach ( var factionMaterial in factionMaterials ) {
		  factionMaterialDict.Add(factionMaterial.faction, matColorMaterialDict[factionMaterial.matColor]);
	  }
  }
}
