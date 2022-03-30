using GDP01._Gameplay.Provider;
using UnityEngine;
using WorldObjects;

namespace QuestSystem.ScriptabelObjects {
	public class Task_SwitchActive_SO : TaskSO {

		[SerializeField] private int switchId; 
		private WorldObjectList _worldObjectList; 
		
		public override TaskType Type { get; } = TaskType.Switch_Active;
		public override string BaseName { get; } = "SwitchActive";
		
		public override bool IsDone() {
			if ( active ) {
				done = false;


				var foundSwitch = GameplayProvider.Current.WorldObjectManager
					.GetSwitchWhere(switchComp => switchComp.IsActivated && switchComp.Id == switchId);

				if ( foundSwitch is { Count: >0 } ) {
					done = true;
				}
					
				// if (_worldObjectList is {}) {
				// 	foreach ( var switchObj in _worldObjectList.switches ) {
				// 		var component = switchObj.GetComponent<SwitchComponent>();
				// 		if ( component is {} ) {
				// 			if ( component.IsActivated && component.Id == switchId ) {
				// 				done = true;
				// 			}
				// 		}
				// 	}	
				// }
			}

			return done;
		}

		public override void StartTask() {
			base.StartTask();
			_worldObjectList = WorldObjectList.FindInstant();
		}
	}
}