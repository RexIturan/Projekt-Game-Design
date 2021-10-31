using Editor.GraphEditors.StateMachineWrapper.Editor.UI.Commands;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace Editor.GraphEditors.StateMachineWrapper.Editor {
		public class TransitionTable_State : GraphToolState{
			/// <inheritdoc />
			public TransitionTable_State(Hash128 graphViewEditorWindowGuid, Preferences preferences)
				: base(graphViewEditorWindowGuid, preferences) {
				this.SetInitialSearcherSize(SearcherService.Usage.k_CreateNode, new Vector2(375, 300), 2.0f);
			}
        
			/// <inheritdoc />
			public override void RegisterCommandHandlers(Dispatcher dispatcher)
			{
				base.RegisterCommandHandlers(dispatcher);

				if (!(dispatcher is CommandDispatcher commandDispatcher))
					return;
				
				commandDispatcher.RegisterCommandHandler<SetStateReferenceCommand>(SetStateReferenceCommand.DefaultHandler);
			}
		}
	}