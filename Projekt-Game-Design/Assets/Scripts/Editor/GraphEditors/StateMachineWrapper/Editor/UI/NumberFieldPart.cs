using Editor.GraphEditors.StateMachineWrapper.Editor.Nodes;
using Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace Editor.GraphEditors.StateMachineWrapper.Editor.UI {
    public class NumberFieldPart : BaseModelUIPart {

        public static readonly string ussClassName = "";
        public static readonly string numberLabelName = "number";

        public static NumberFieldPart Create (
            string name, IGraphElementModel model, IModelUI modelUI, string parentClassName) {
            
            if (model is INodeModel)
            {
                return new NumberFieldPart(name, model, modelUI, parentClassName);
            }

            return null;
        } 
        
        VisualElement NumberFieldContainer { get; set; }
        EditableLabel NumberFieldLabel { get; set; }
        
        public override VisualElement Root => NumberFieldContainer;
        
        public NumberFieldPart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }
        
        protected override void BuildPartUI(VisualElement parent) {
            NumberFieldContainer = new VisualElement {name = PartName};
            NumberFieldContainer.AddToClassList(ussClassName);
            NumberFieldContainer.AddToClassList(m_ParentClassName.WithUssElement(PartName));

            NumberFieldLabel = new EditableLabel {name = numberLabelName};
            NumberFieldLabel.RegisterCallback<ChangeEvent<string>>(OnChangeNumber);
            NumberFieldLabel.AddToClassList(ussClassName.WithUssElement("number"));
            NumberFieldLabel.AddToClassList(m_ParentClassName.WithUssElement("number"));
            NumberFieldContainer.Add(NumberFieldLabel);
            
            parent.Add(NumberFieldContainer);
        }

        void OnChangeNumber(ChangeEvent<string> evt) {
            if (!(m_Model is Transition_NodeModel transitionNodeModel))
                return;

            if (int.TryParse(evt.newValue, out var v))
                m_OwnerElement.CommandDispatcher.Dispatch(new SetNumberCommand(v, transitionNodeModel));
        }
        
        protected override void PostBuildPartUI() {
            base.PostBuildPartUI();

            //todo write own stylesheet
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(
                "Assets/Scripts/GraphViewEditors/StateMachine/TransitionTable/Editor/UI/TransitionNodePart.uss");
            
            if (stylesheet != null) {
                NumberFieldContainer.styleSheets.Add(stylesheet);
            }
        }
        
        protected override void UpdatePartFromModel() {
            if (!(m_Model is Transition_NodeModel transitionNodeModel))
                return;

            NumberFieldLabel.SetValueWithoutNotify($"{transitionNodeModel.number} mal");
        }
    }
}
