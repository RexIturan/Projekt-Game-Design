using UnityEngine;

namespace SceneManagement.ScriptableObjects {
    /// <summary>
    /// This class contains Settings specific to PersistentManagers scenes only
    /// </summary>
//CreateAssetMenu commented since we don't want to create more than one Initialisation GameSceneSO
    [CreateAssetMenu(fileName = "PersistentManagers", menuName = "Scene Data/PersistentManagers")]
    public class PersistentManagersSO : GameSceneSO { }
}
