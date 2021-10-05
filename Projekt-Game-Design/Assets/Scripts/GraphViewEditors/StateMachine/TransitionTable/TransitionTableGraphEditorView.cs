using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.UIElements;

namespace GraphViewEditors.StateMachine.TransitionTable {
    public class TransitionTableGraphEditorView : GraphView {
        #region UXML
        [Preserve]
        public new class UxmlFactory : UxmlFactory<TransitionTableGraphEditorView, GraphView.UxmlTraits> { }
        #endregion

        public TransitionTableGraphEditorView() {
            Insert(0, new GridBackground());
            // var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/GraphView/StateMachine/TransitionTable/TransitionTableGraphEditor.uss");
            var ussGUID = AssetDatabase.FindAssets("TransitionTableGraphEditor t:StyleSheet");
            var ussPath = AssetDatabase.GUIDToAssetPath(ussGUID.Length > 0 ? ussGUID[0] : "");
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ussPath);
            Debug.Log(styleSheet);
            // if (styleSheet != null) {
            //     styleSheets.Add(styleSheet);
            // }
            styleSheets.Add(styleSheet);
        }
    }
}
