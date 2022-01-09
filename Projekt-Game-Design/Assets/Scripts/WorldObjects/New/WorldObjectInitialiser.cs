using Combat;
using SaveSystem.SaveFormats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		public class WorldObjectInitialiser : MonoBehaviour
		{
				[SerializeField] private DoorContainerSO doorContainer;

				public void Initialise(List<Door_Save> door_Saves)
				{
						WorldObjectList worldObjects = WorldObjectList.FindWorldObjectList();

						// doors
						//
						Transform parent = GameObject.Find("WorldObjects/doors").transform;

						worldObjects.doors.Clear();

						foreach(Door_Save door in door_Saves)
						{
								DoorSO type = doorContainer.doors[door.doorTypeId];
								GameObject doorObj = Instantiate(type.prefab, parent, true);
								doorObj.GetComponent<Door>().Initialise(door, type);
								worldObjects.doors.Add(doorObj);
						}
				}
		}
}