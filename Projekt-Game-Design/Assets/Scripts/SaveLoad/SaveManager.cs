using System.IO;
using Grid;
using UnityEditor;
using UnityEngine;

namespace SaveLoad {
    public class SaveManager : MonoBehaviour {
        [SerializeField] private GridContainerSO gridContainer;

        [SerializeField] private string pathBase;
        [SerializeField] private string filename;
        [SerializeField] private string fileSuffix;

        //TODO return bool if successful
        public void SaveGridContainer() {
            var json = JsonUtility.ToJson(gridContainer);

            // TODO Debug Macro
            Debug.Log($"Save Test GridContainer to JSON \n{json}");

            var path = pathBase + filename + fileSuffix;

            using (var fs = new FileStream(path, FileMode.Create)) {
                using (var writer = new StreamWriter(fs)) {
                    writer.Write(json);
                }
            }

            // AssetDatabase.Refresh();
        }
    }
}