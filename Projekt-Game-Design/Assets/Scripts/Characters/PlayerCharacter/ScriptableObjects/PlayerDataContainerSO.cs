using System.Collections.Generic;
using UnityEngine;

namespace Characters.PlayerCharacter.ScriptableObjects {
    [CreateAssetMenu(fileName = "New PlayerDataContainer", menuName = "Character/PlayerDataContainer")]
    public class PlayerDataContainerSO : ScriptableObject {
        public List<PlayerTypeSO> playerTypes;
        public List<PlayerSpawnDataSO> playerpSpawnData;
    }
}