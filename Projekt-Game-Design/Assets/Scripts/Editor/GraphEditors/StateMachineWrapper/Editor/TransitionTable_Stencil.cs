﻿using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
	public class TransitionTable_Stencil : Stencil {
		public static readonly string toolName = "State Machine Wrapper GraphEditor";
		public override string ToolName => toolName;

		public static readonly string graphName = "State Machine Wrapper";
		public static TypeHandle TransitionIn { get; } = TypeHandleHelpers.GenerateCustomTypeHandle("TransitionIn");
		public static TypeHandle TransitionOut { get; } = TypeHandleHelpers.GenerateCustomTypeHandle("TransitionOut");
        
		/// <inheritdoc />
		public override IBlackboardGraphModel CreateBlackboardGraphModel(IGraphAssetModel graphAssetModel) {
			return new TransitionTable_BlackboardGraphModel(graphAssetModel);
		}
        
		/// <inheritdoc />
		public override void PopulateBlackboardCreateMenu(string sectionName, GenericMenu menu, CommandDispatcher commandDispatcher)
		{
			if (sectionName == TransitionTable_BlackboardGraphModel.Sections[0])
			{
				menu.AddItem(new GUIContent("Add"), false, () =>
				{
					CreateVariableDeclaration(TransitionIn.Identification, TransitionIn);
				});
			}
			else if (sectionName == TransitionTable_BlackboardGraphModel.Sections[1])
			{
				menu.AddItem(new GUIContent("Add"), false, () =>
				{
					CreateVariableDeclaration(TransitionOut.Identification, TransitionOut);
				});
			}

			void CreateVariableDeclaration(string name, TypeHandle type)
			{
				var finalName = name;
				var i = 0;

				// ReSharper disable once AccessToModifiedClosure
				while (commandDispatcher.State.WindowState.GraphModel.VariableDeclarations.Any(v => v.Title == finalName))
					finalName = name + i++;

				commandDispatcher.Dispatch(new CreateGraphVariableDeclarationCommand(finalName, true, type));
			}
		}
	}
}