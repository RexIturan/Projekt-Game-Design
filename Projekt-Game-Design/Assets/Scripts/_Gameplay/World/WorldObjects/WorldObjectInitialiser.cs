using Characters;
using GDP01.TileEffects;
using SaveSystem.SaveFormats;
using System.Collections.Generic;
using GDP01._Gameplay.Provider;
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
				[SerializeField] private TileEffectContainerSO tileEffectContainer;

				private WorldObjectManager WorldObjectManager =>
					GameplayProvider.Current.WorldObjectManager;
				
				public void Initialise(List<Door_Save> door_Saves,
						List<Switch_Save> switch_Saves,
						List<Junk_Save> junk_Saves,
						List<TileEffect_Save> tileEffects_Saves,
						List<Item_Save> itemSaves) {


						Transform parent;
						WorldObjectList worldObjects = WorldObjectList.FindInstant();
						TileEffectManager tileEffects = FindObjectOfType<TileEffectManager>();

						WorldObjectManager?.ClearAllComponents();

						if ( WorldObjectManager is { } ) {
							
							//load doors
							foreach ( Door_Save doorSave in door_Saves ) {
								DoorTypeSO type = doorContainer.doors[doorSave.doorTypeId];
								GameObject doorObj = Instantiate(type.prefab);
								doorObj.GetComponent<Door>().InitFromSave(doorSave, type);
								Door door = doorObj.GetComponent<Door>();
								WorldObjectManager.AddDoor(door);
							}

							//load switches
							foreach ( Switch_Save switchSave in switch_Saves ) {
								SwitchTypeSO type = switchContainer.switches[switchSave.switchTypeId];
								GameObject switchObj = Instantiate(type.prefab);
								SwitchComponent switchComponent = switchObj.GetComponent<SwitchComponent>();
								switchComponent.Initialise(switchSave, type);
								WorldObjectManager.AddSwitch(switchComponent);
							}

							//load junk
							foreach ( Junk_Save junkSave in junk_Saves ) {
								JunkTypeSO type = junkContainer.junks[junkSave.junkTypeId];
								GameObject junkObj = Instantiate(type.prefab);
								Junk junk = junkObj.GetComponent<Junk>();
								junk.InitFromSave(junkSave, type);
								WorldObjectManager.AddJunk(junk);
							}
						
							foreach ( var itemSave in itemSaves ) {
								ItemTypeSO itemType = itemTypeContainer.GetItemFromID(itemSave.id);
								GameObject itemObj = Instantiate(itemType.prefab);
								ItemComponent item = itemObj.GetComponent<ItemComponent>();
								item.InitItem(itemType, itemSave.gridPos);
								WorldObjectManager.AddItem(item);
							}
						}
						
						// doors
						//  parent= GameObject.Find("WorldObjects/doors").transform;
						//
						// worldObjects.doors.Clear();
						//
						// foreach ( Door_Save door in door_Saves )
						// {
						// 		DoorTypeSO type = doorContainer.doors[door.doorTypeId];
						// 		GameObject doorObj = Instantiate(type.prefab, parent, true);
						// 		doorObj.GetComponent<Door>().Initialise(door, type);
						// 		worldObjects.doors.Add(doorObj);
						// }

						// parent = GameObject.Find("WorldObjects/switches").transform;
						//
						// worldObjects.switches.Clear();
						//
						// foreach ( Switch_Save switchSave in switch_Saves )
						// {
						// 		SwitchTypeSO type = switchContainer.switches[switchSave.switchTypeId];
						// 		GameObject switchObj = Instantiate(type.prefab, parent, true);
						// 		switchObj.GetComponent<SwitchComponent>().Initialise(switchSave, type);
						// 		worldObjects.switches.Add(switchObj);
						// }

						// worldObjects.items.ClearGameObjectReferences();
						// foreach ( var itemSave in itemSaves )
						// {
						// 	ItemTypeSO itemType = itemTypeContainer.GetItemFromID(itemSave.id);
						// 	GameObject itemObj = Instantiate(itemType.prefab, worldObjects.ItemParent, true);
						// 	itemObj.GetComponent<ItemComponent>().InitItem(itemType, itemSave.gridPos);
						// 	worldObjects.items.Add(itemObj);
						//
						// 		
						// }
						//
						//
						//TODO JUNK
						// parent = GameObject.Find("WorldObjects/junk").transform;
						//
						// worldObjects.junks.Clear();
						//
						// foreach ( Junk_Save junk in junk_Saves )
						// {
						// 		JunkTypeSO type = junkContainer.junks[junk.junkTypeId];
						// 		GameObject junkObj = Instantiate(type.prefab, parent, true);
						// 		junkObj.GetComponent<Junk>().Initialise(junk, type);
						// 		worldObjects.junks.Add(junkObj);
						// }

					
						parent = GameObject.Find("TileEffects").transform;

						tileEffects.Clear();

						foreach ( TileEffect_Save tileEffectSave in tileEffects_Saves )
						{
							GameObject prefab = tileEffectContainer.tileEffects[tileEffectSave.prefabID];
							GameObject tileEffectObj = Instantiate(prefab, parent, true);
							TileEffectController tileEffect = tileEffectObj.GetComponent<TileEffectController>();
							tileEffect.SetTimeUntilActivation(tileEffectSave.timeUntilActivation);
							tileEffect.SetTimeToLive(tileEffectSave.timeToLive);
							tileEffectObj.GetComponent<GridTransform>().MoveTo(tileEffectSave.position);

							tileEffects.Add(tileEffectObj);
						}
				}
		}
}