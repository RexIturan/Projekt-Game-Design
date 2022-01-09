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
				[SerializeField] private SwitchContainerSO switchContainer;

				public void Initialise(List<Door_Save> door_Saves, List<Switch_Save> switch_Saves)
				{
						WorldObjectList worldObjects = WorldObjectList.FindWorldObjectList();

						// doors
						//
						Transform parent = GameObject.Find("WorldObjects/doors").transform;

						worldObjects.doors.Clear();

						foreach(Door_Save door in door_Saves)
						{
								DoorTypeSO type = doorContainer.doors[door.doorTypeId];
								GameObject doorObj = Instantiate(type.prefab, parent, true);
								doorObj.GetComponent<Door>().Initialise(door, type);
								worldObjects.doors.Add(doorObj);
						}

						parent = GameObject.Find("WorldObjects/switches").transform;

						worldObjects.switches.Clear();

						foreach(Switch_Save switchSave in switch_Saves)
						{
								SwitchTypeSO type = switchContainer.switches[switchSave.switchTypeId];
								GameObject switchObj = Instantiate(type.prefab, parent, true);
								switchObj.GetComponent<SwitchComponent>().Initialise(switchSave, type);
								worldObjects.switches.Add(switchObj);
						}
				}
		}
}