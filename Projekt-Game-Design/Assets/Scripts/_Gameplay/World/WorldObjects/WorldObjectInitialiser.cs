using Characters;
using GDP01.TileEffects;
using SaveSystem.SaveFormats;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		public class WorldObjectInitialiser : MonoBehaviour
		{
				[SerializeField] private DoorContainerSO doorContainer;
				[SerializeField] private SwitchContainerSO switchContainer;
				[SerializeField] private JunkContainerSO junkContainer;
				[SerializeField] private TileEffectContainerSO tileEffectContainer;

				public void Initialise(List<Door_Save> door_Saves, 
						List<Switch_Save> switch_Saves,
						List<Junk_Save> junk_Saves,
						List<TileEffect_Save> tileEffects_Saves)
				{
						WorldObjectList worldObjects = WorldObjectList.FindInstant();
						TileEffectList tileEffects = FindObjectOfType<TileEffectList>();

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

						parent = GameObject.Find("WorldObjects/TileEffects").transform;

						tileEffects.Clear();

						foreach ( TileEffect_Save tileEffect in tileEffects_Saves )
						{
								GameObject prefab = tileEffectContainer.tileEffects[tileEffect.prefabID];
								GameObject tileEffectObj = Instantiate(prefab, parent, true);
								TileEffectController tileEffectController = tileEffectObj.GetComponent<TileEffectController>();
								tileEffectController.SetTimeUntilActivation(tileEffect.timeUntilActivation);
								tileEffectController.SetTimeToLive(tileEffect.timeToLive);
								tileEffectObj.GetComponent<GridTransform>().gridPosition = tileEffect.position;

								tileEffects.Add(tileEffectObj);
						}
				}
		}
}