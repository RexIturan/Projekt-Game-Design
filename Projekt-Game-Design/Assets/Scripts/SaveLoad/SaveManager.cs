using System.IO;
using Grid;
using UnityEngine;

namespace SaveLoad {
    public class SaveManager : MonoBehaviour {
        [SerializeField] private GridContainerSO gridContainer;

        [SerializeField] private string pathBase;
        [SerializeField] private string filename;
        [SerializeField] private string fileSuffix;
        
        //TODO return bool if successful
        public void SaveGridContainer() {
            string json = JsonUtility.ToJson(gridContainer);
            
            // TODO Debug Macro
            Debug.Log($"Save Test GridContainer to JSON \n{json}");

            var path = pathBase + filename + fileSuffix;

            using (FileStream fs = new FileStream(path, FileMode.Create)) {
                using (StreamWriter writer = new StreamWriter(fs)) {
                    writer.Write(json);
                }
            }
            
            UnityEditor.AssetDatabase.Refresh();
        }
    }
}