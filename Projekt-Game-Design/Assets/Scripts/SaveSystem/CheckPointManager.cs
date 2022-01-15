using Events.ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
		[SerializeField] private bool saveState;
		[SerializeField] private bool loadState;
		[SerializeField] private IntEventChannelSO saveGame;
		[SerializeField] private IntEventChannelSO loadGame;

		private void Update()
		{
				if(saveState)
				{
						saveState = false;

						saveGame.RaiseEvent(0);
				}

				if ( loadState )
				{
						loadState = false;

						loadGame.RaiseEvent(0);
				}
		}
}
