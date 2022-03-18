using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace GDP01.Util {
    public class SerializableScriptableObject : ScriptableObject {
	    
	    //SerializableScriptableObjectData
	    public class ReferenceData {
		    public string guid;
		    public string name;
		    public SerializableScriptableObject obj;
	    }
	    
	    
        private string _guid;
        public string Guid => _guid;

#if UNITY_EDITOR
        void OnValidate() {
            var path = AssetDatabase.GetAssetPath(this);
            _guid = AssetDatabase.AssetPathToGUID(path);
        }
#endif

	    /// <summary>
	    /// Wrapper to save the reference of an ScriptableObject
	    /// </summary>
	    /// <returns>Wrapper Object</returns>
	    public ReferenceData ToData() {
		    return new ReferenceData {
			    guid = Guid,
			    name = name,
			    obj = this
		    };
	    }
    }
}