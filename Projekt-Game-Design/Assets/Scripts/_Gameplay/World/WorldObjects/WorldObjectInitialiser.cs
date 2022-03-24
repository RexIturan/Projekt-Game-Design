using SaveSystem.SaveFormats;
using System.Collections.Generic;
using UnityEngine;
using Util.Extensions;

namespace WorldObjects
{
		public class WorldObjectInitialiser : MonoBehaviour
		{
				[SerializeField] private DoorContainerSO doorContainer;
				[SerializeField] private SwitchContainerSO switchContainer;
				[SerializeField] private JunkContainerSO junkContainer;
				[SerializeField] private ItemTypeContainerSO itemTypeContainer;

				public void Initialise(List<Door_Save> door_Saves, 
						List<Switch_Save> switch_Saves,
						List<Junk_Save> junk_Saves,
						List<Item_Save> itemSaves)
				{
						WorldObjectList worldObjects = WorldObjectList.FindInstant();

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

						parent = GameObject.Find("WorldObjects/junk").transform;

						worldObjects.junks.Clear();

						foreach ( Junk_Save junk in junk_Saves )
						{
								JunkTypeSO type = junkContainer.junks[junk.junkTypeId];
								GameObject junkObj = Instantiate(type.prefab, parent, true);
								junkObj.GetComponent<Junk>().Initialise(junk, type);
								worldObjects.junks.Add(junkObj);
						}
						
						worldObjects.items.ClearGameObjectReferences();
						foreach ( var itemSave in itemSaves ) {
							ItemTypeSO itemType = itemTypeContainer.GetItemFromID(itemSave.id);
							GameObject itemObj = Instantiate(itemType.prefab, worldObjects.ItemParent, true);
							itemObj.GetComponent<ItemComponent>().InitItem(itemType, itemSave.gridPos);
							worldObjects.items.Add(itemObj);
						}
				}
		}
}