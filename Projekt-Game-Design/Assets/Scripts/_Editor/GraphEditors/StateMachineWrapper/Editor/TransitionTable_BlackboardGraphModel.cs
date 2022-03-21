using System.Collections.Generic;
using System.Linq;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEditor.GraphToolsFoundation.Overdrive.BasicModel;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_BlackboardGraphModel : BlackboardGraphModel {
		internal static readonly string[] Sections = { "Blackboard List1", "Blackboard List2" };

		/// <inheritdoc />        
		public TransitionTable_BlackboardGraphModel(IGraphAssetModel graphAssetModel)
			: base(graphAssetModel) { }

		public override string GetBlackboardTitle() {
			return AssetModel?.FriendlyScriptName == null
				? "Blackboard"
				: AssetModel?.FriendlyScriptName + " Blackboard";
		}

		public override string GetBlackboardSubTitle() {
			return "The Blackboard Subtitle";
		}

		public override IEnumerable<string> SectionNames =>
			GraphModel == null ? Enumerable.Empty<string>() : Sections;

		// public override IEnumerable<IVariableDeclarationModel> GetSectionRows(string sectionName) {
		// 	if ( sectionName == Sections[0] ) {
		// 		return GraphModel?.VariableDeclarations?.Where(v =>
		// 			       v.DataType == TransitionTable_Stencil.TransitionIn) ??
		// 		       Enumerable.Empty<IVariableDeclarationModel>();
		// 	}
		//
		// 	if ( sectionName == Sections[1] ) {
		// 		return GraphModel?.VariableDeclarations?.Where(v =>
		// 			       v.DataType == TransitionTable_Stencil.TransitionOut) ??
		// 		       Enumerable.Empty<IVariableDeclarationModel>();
		// 	}
		//
		// 	return Enumerable.Empty<IVariableDeclarationModel>();
		// }
	}
}