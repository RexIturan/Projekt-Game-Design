﻿using System.Linq;
using UnityEditor;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.Overdrive;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public class TransitionTableStencil : Stencil {
        public static readonly string toolName = "TransitonTable GraphEditor";
        public override string ToolName => toolName;

        public static readonly string graphName = "TransitonTable";
        public static TypeHandle TransitionIn { get; } = TypeHandleHelpers.GenerateCustomTypeHandle("TransitionIn");
        public static TypeHandle TransitionOut { get; } = TypeHandleHelpers.GenerateCustomTypeHandle("TransitionOut");
        
        /// <inheritdoc />
        public override IBlackboardGraphModel CreateBlackboardGraphModel(IGraphAssetModel graphAssetModel) {
            return new TransitionTableBlackboardGraphModel(graphAssetModel);
        }
        
        /// <inheritdoc />
        public override void PopulateBlackboardCreateMenu(string sectionName, GenericMenu menu, CommandDispatcher commandDispatcher)
        {
            if (sectionName == TransitionTableBlackboardGraphModel.k_Sections[0])
            {
                menu.AddItem(new GUIContent("Add"), false, () =>
                {
                    CreateVariableDeclaration(TransitionIn.Identification, TransitionIn);
                });
            }
            else if (sectionName == TransitionTableBlackboardGraphModel.k_Sections[1])
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