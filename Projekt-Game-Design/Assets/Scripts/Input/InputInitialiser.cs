using System;
using Input;
using UnityEngine;

namespace GDP01.Input.Input {
	[Serializable][Flags]
	public enum InputMaps {
		Nothing     = 0,
		Gameplay    = 1,
		Camera      = 2,
		Menu        = 4,
		LevelEditor = 8,
		Inventory   = 16,
		// add all the others
	}
	
	public class InputInitialiser : MonoBehaviour {
		[SerializeField] private InputMaps enableInput;
		[SerializeField] private InputReader inputReader;

		private void Start() {

			if ( enableInput.HasFlag(InputMaps.Camera) ) {
				
			}
			foreach (InputMaps value in Enum.GetValues(typeof(InputMaps)))
			{
				if ((enableInput & value) == value)
				{
					switch (value)
					{
						case InputMaps.Camera:
						case InputMaps.Gameplay:
							inputReader.EnableGameplayInput();
							break;
						case InputMaps.Inventory:
							inputReader.EnableInventoryInput();
							break;
						case InputMaps.Menu:
							inputReader.EnableMenuInput();
							break;
						case InputMaps.LevelEditor:
							inputReader.EnableLevelEditorInput();
							break;
					}
				}
			}
		}
	}
}