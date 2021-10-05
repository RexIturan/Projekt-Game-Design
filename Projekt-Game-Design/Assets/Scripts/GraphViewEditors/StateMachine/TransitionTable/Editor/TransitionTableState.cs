using GraphViewEditors.StateMachine.TransitionTable.Editor.UI.Commands;
using UnityEditor.GraphToolsFoundation.Overdrive;
using UnityEngine;
using UnityEngine.GraphToolsFoundation.CommandStateObserver;

namespace GraphViewEditors.StateMachine.TransitionTable.Editor {
    public class TransitionTableState : GraphToolState{
        /// <inheritdoc />
        public TransitionTableState(Hash128 graphViewEditorWindowGUID, Preferences preferences)
            : base(graphViewEditorWindowGUID, preferences) {
            this.SetInitialSearcherSize(SearcherService.Usage.k_CreateNode, new Vector2(375, 300), 2.0f);
        }
        
        /// <inheritdoc />
        public override void RegisterCommandHandlers(Dispatcher dispatcher)
        {
            base.RegisterCommandHandlers(dispatcher);

            if (!(dispatcher is CommandDispatcher commandDispatcher))
                return;

            commandDispatcher.RegisterCommandHandler<SetNumberCommand>(SetNumberCommand.DefaultHandler);
            // commandDispatcher.RegisterCommandHandler<AddPortCommand>(AddPortCommand.DefaultHandler);
            // commandDispatcher.RegisterCommandHandler<RemovePortCommand>(RemovePortCommand.DefaultHandler);
            //
            // commandDispatcher.RegisterCommandHandler<SetTemperatureCommand>(SetTemperatureCommand.DefaultHandler);
            // commandDispatcher.RegisterCommandHandler<SetDurationCommand>(SetDurationCommand.DefaultHandler);
        }
    }
}