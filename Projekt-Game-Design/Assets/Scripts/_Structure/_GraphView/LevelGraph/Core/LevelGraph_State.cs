using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace _Structure._GraphView.LevelGraph.Core {
		public class LevelGraph_State : GraphToolState{
			/// <inheritdoc />
			public LevelGraph_State(Hash128 graphViewEditorWindowGuid, Preferences preferences)
				: base(graphViewEditorWindowGuid, preferences) {
				this.SetInitialSearcherSize(SearcherService.Usage.k_CreateNode, new Vector2(375, 300), 2.0f);
			}
        
			/// <inheritdoc />
			public override void RegisterCommandHandlers(Dispatcher dispatcher)
			{
				base.RegisterCommandHandlers(dispatcher);

				if (!(dispatcher is CommandDispatcher commandDispatcher))
					return;
			}
		}
	}