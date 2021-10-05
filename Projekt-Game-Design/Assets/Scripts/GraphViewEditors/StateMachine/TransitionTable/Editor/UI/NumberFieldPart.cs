using GraphViewEditors.StateMachine.TransitionTable.Editor.Nodes;
using GraphViewEditors.StateMachine.TransitionTable.Editor.UI.Commands;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.UIElements;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor.UI {
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
        EditableLabel numberFieldLabel { get; set; }
        
        public override VisualElement Root => NumberFieldContainer;
        
        public NumberFieldPart(string name, IGraphElementModel model, IModelUI ownerElement, string parentClassName)
            : base(name, model, ownerElement, parentClassName) { }
        
        protected override void BuildPartUI(VisualElement parent) {
            NumberFieldContainer = new VisualElement {name = PartName};
            NumberFieldContainer.AddToClassList(ussClassName);
            NumberFieldContainer.AddToClassList(m_ParentClassName.WithUssElement(PartName));

            numberFieldLabel = new EditableLabel {name = numberLabelName};
            numberFieldLabel.RegisterCallback<ChangeEvent<string>>(OnChangeNumber);
            numberFieldLabel.AddToClassList(ussClassName.WithUssElement("number"));
            numberFieldLabel.AddToClassList(m_ParentClassName.WithUssElement("number"));
            NumberFieldContainer.Add(numberFieldLabel);
            
            parent.Add(NumberFieldContainer);
        }

        void OnChangeNumber(ChangeEvent<string> evt) {
            if (!(m_Model is TransitionNodeModel transitionNodeModel))
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
            if (!(m_Model is TransitionNodeModel transitionNodeModel))
                return;

            numberFieldLabel.SetValueWithoutNotify($"{transitionNodeModel.number} mal");
        }
    }
}