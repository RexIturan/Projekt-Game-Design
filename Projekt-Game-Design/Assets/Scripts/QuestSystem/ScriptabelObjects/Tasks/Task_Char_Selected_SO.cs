using Characters;
using GDP01.Player.Player;
using Player;

namespace QuestSystem.ScriptabelObjects {
	public class Task_Char_Selected_SO : TaskSO {
///// Private Variable
		
		private PlayerController _playerController;

///// Private Variable
		
///// TaskSO			
		
		public override TaskType Type { get; } = TaskType.Char_Selected;
		public override string BaseName { get; } = "CharSelected";
		
		public override bool IsDone() {

			if ( active ) {
				done = false;
			
				if ( _playerController.HasSelected ) {
					var selected = _playerController.Selected;
					var pcComponent = selected.GetComponent<PlayerCharacterSC>();
					var stats = selected.GetComponent<Statistics>();

					if ( pcComponent is { } && stats is { } ) {
						done = true;
					}				
				}	
			}
			
			return done;
		}

		public override void StartTask() {
			base.StartTask();
			_playerController = PlayerController.FindInstance();
		}
	}
}