using GDP01._Gameplay.World.Character;
using GDP01._Gameplay.World.Character.Data;
using UnityEngine;

/// <summary>
///player type containing constant initial data
/// for individual types of players (e.g. warrior, mage)
/// </summary> 
[CreateAssetMenu(fileName = "New PlayerType", menuName = "Character/PlayerType")]
public class PlayerTypeSO : CharacterTypeSO {
	//equipment
	// public int equipmentID;

	// public override T ToData<T>() {
	// 	return base.ToData<T>();
	// }

	public new PlayerCharacterData ToData() {
		PlayerCharacterData playerData = base.ToData<PlayerCharacterData>();
		
		// playerData.EquipmentId = equipmentID;
		playerData.Type = this;
		
		return playerData;
	}
}