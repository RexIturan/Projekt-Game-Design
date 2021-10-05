using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace GraphViewEditors.StateMachine.TransitionTable {
    public class TransitionTableGraphEditor : EditorWindow
    {
        [MenuItem("Tools/GraphEditor/Transition Table Graph Editor")]
        public static void OpenWindow()
        {
            TransitionTableGraphEditor wnd = GetWindow<TransitionTableGraphEditor>();
            wnd.titleContent = new GUIContent("TransitionTableGraphEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/Scripts/GraphView/StateMachine/TransitionTable/TransitionTableGraphEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/Scripts/GraphView/StateMachine/TransitionTable/TransitionTableGraphEditor.uss");
            root.styleSheets.Add(styleSheet);
        }
    }
}