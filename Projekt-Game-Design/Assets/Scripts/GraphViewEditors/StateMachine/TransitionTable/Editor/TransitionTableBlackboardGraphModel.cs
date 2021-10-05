using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public class TransitionTableBlackboardGraphModel : BlackboardGraphModel {
        
        internal static readonly string[] k_Sections = { "TransitionIn", "TransitionOut" };
        
        /// <inheritdoc />        
        public TransitionTableBlackboardGraphModel(IGraphAssetModel graphAssetModel) 
            : base(graphAssetModel) { }
        
        public override string GetBlackboardTitle()
        {
            return AssetModel?.FriendlyScriptName == null ? "Blackboard" : AssetModel?.FriendlyScriptName + " Blackboard";
        }
        
        public override string GetBlackboardSubTitle()
        {
            return "The Blackboard Subtitle";
        }
        
        public override IEnumerable<string> SectionNames =>
            GraphModel == null ? Enumerable.Empty<string>() : k_Sections;

        public override IEnumerable<IVariableDeclarationModel> GetSectionRows(string sectionName)
        {
            if (sectionName == k_Sections[0])
            {
                return GraphModel?.VariableDeclarations?.Where(v => v.DataType == TransitionTableStencil.TransitionIn) ??
                       Enumerable.Empty<IVariableDeclarationModel>();
            }

            if (sectionName == k_Sections[1])
            {
                return GraphModel?.VariableDeclarations?.Where(v => v.DataType == TransitionTableStencil.TransitionOut) ??
                       Enumerable.Empty<IVariableDeclarationModel>();
            }

            return Enumerable.Empty<IVariableDeclarationModel>();
        }
    }
}