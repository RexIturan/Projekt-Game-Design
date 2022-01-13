using System;
using System.Collections.Generic;
using Events.ScriptableObjects.Core;
using UnityEngine;
using Util;

namespace Events.ScriptableObjects
{
		[CreateAssetMenu(menuName = "Events/Level/DrawTargetTilesEventChannelSO")]
		public class DrawTargetTilesEventChannelSO : EventChannelBaseSO
		{
				public event Action<List<PathNode>, List<PathNode>, List<PathNode>, bool> OnEventRaised;

				public void RaiseEvent(List<PathNode> allies, List<PathNode> neutrals, List<PathNode> enemies, bool damage)
				{
						if ( OnEventRaised != null )
								OnEventRaised.Invoke(allies, neutrals, enemies, damage);
				}
		}
}