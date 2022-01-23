using Audio;
using Characters;
using Combat;
using Events.ScriptableObjects;
using SaveSystem.SaveFormats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Types;
using UnityEngine;
using WorldObjects.Doors;

namespace WorldObjects
{
		public class Door : MonoBehaviour
		{
				private const float OPENING_DISTANCE = 1.1f;

				//todo refactor this
				[SerializeField] private InventorySO inventory;

				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectsEvent;
				[SerializeField] private IntEventChannelSO switchActivatedEvent;
				[SerializeField] private IntEventChannelSO triggerActivatedEvent;

				public int doorId;
				public DoorTypeSO doorType;
				public bool open;
				[SerializeField] private bool broken;
				public bool locked;
				public List<int> keyIds;
				public List<int> switchIds;
				public List<int> triggerIds;

				public List<int> remainingSwitches;
				public List<int> remainingTrigger;

				public Vector3 orientation;

				[SerializeField] private SlidingDoorController slidingDoorController;

				public void Initialise(Door_Save saveData, DoorTypeSO doorType)
				{
						this.doorType = doorType;
						open = saveData.open;
						locked = true;
						broken = false;

						orientation = saveData.orientation;
						InitialiseOrientation();

						if ( doorType.destructable )
						{
								Targetable targetable = gameObject.AddComponent<Targetable>();
								targetable.Initialise();
						}

						// Instantiate(doorType.model, transform);

						gameObject.GetComponent<GridTransform>().gridPosition = saveData.gridPos;

						Statistics stats = gameObject.GetComponent<Statistics>();
						stats.StatusValues.HitPoints.Max = doorType.hitPoints;
						stats.StatusValues.HitPoints.Value = saveData.currentHitPoints;
						stats.SetFaction(Faction.Neutral);

						keyIds = saveData.keyIds;
						switchIds = saveData.switchIds;
						triggerIds = saveData.triggerIds;
						remainingSwitches = saveData.remainingSwitchIds;
						remainingTrigger = saveData.remainingTriggerIds;

						if ( keyIds.Count > 0 ) {
							slidingDoorController.InitValues(DoorType.Key, keyIds.Count);
						} else if ( switchIds.Count > 0 ) {
							slidingDoorController.InitValues(DoorType.Switch, switchIds.Count);
						}

						if ( remainingSwitches.Count < switchIds.Count ) {
							var activeSwitches = switchIds.Count - remainingSwitches.Count;
							for ( int i = 0; i < activeSwitches; i++ ) {
								slidingDoorController.OpenLock();
							}
						}

						UpdateDoor();
						
						if ( open ) {
							OpenDoor();
						}
				}

				// causes the model's front face to go into the direction 
				// that is specified by the vector in orientation
				// for example: orientation (1, 0) would cause the door to have its front face
				// faced towards the positive x-Axis-Vector
				private void InitialiseOrientation()
				{
					gameObject.transform.rotation = Quaternion.LookRotation(orientation);
				}

				public void Awake()
				{
						updateWorldObjectsEvent.OnEventRaised += UpdateDoor;
						switchActivatedEvent.OnEventRaised += HandleSwitchActivatedEvent;
						triggerActivatedEvent.OnEventRaised += HandleTriggerActivatedEvent;
				}

				public void OnDestroy()
				{
						updateWorldObjectsEvent.OnEventRaised -= UpdateDoor;
						switchActivatedEvent.OnEventRaised -= HandleSwitchActivatedEvent;
						triggerActivatedEvent.OnEventRaised -= HandleTriggerActivatedEvent;
				}

				private void HandleSwitchActivatedEvent(int switchId)
				{
						if (switchIds.Contains(switchId) ) {
							remainingSwitches.Remove(switchId);
							slidingDoorController.OpenLock();
							UpdateDoor();
						}
				}

				private void HandleTriggerActivatedEvent(int triggerId)
				{
					if (switchIds.Contains(triggerId) ) {
						remainingTrigger.Remove(triggerId);
						slidingDoorController.OpenLock();
						UpdateDoor();
					}
				}

				/**
				 * Checks the requirements for being locked or not. 
				 * Then checks if a player is next to it, and if a player is next to it, open. 
				 */
				private void UpdateDoor()
				{
						if ( !broken && !open )
						{
								if ( gameObject.GetComponent<Statistics>().StatusValues.HitPoints.IsMin() )
								{
										DoorDestroyed();
								}
								else
								{
										bool hasAllKeys = true;
										foreach ( int keyId in keyIds )
										{
												bool hasKey = false;
												foreach ( ItemSO item in inventory.playerInventory )
												{
														if ( item.id == keyId )
																hasKey = true;
												}
												if ( !hasKey )
														hasAllKeys = false;
										}

										// hasAllKeys = keyIds.All(i => inventory.playerInventory.Any(item => item.id == i));

										if ( hasAllKeys && remainingSwitches.Count == 0 && remainingTrigger.Count == 0 )
												locked = false;
										
										if ( !locked )
										{
												CharacterList characters = CharacterList.FindInstant();
												bool playerInRange = false;

												if ( !doorType.openManually )
														playerInRange = true;
												else
												{
														foreach ( GameObject player in characters.playerContainer )
														{
																Vector3Int playerPos = player.GetComponent<GridTransform>().gridPosition;
																Vector3Int doorPos = gameObject.GetComponent<GridTransform>().gridPosition;

																if ( Vector3Int.Distance(playerPos, doorPos) < OPENING_DISTANCE )
																		playerInRange = true;
														}
												}

												if ( playerInRange || switchIds.Count > 0)
														OpenDoor();
										}
								}
						}
				}

				private void DoorDestroyed()
				{
						slidingDoorController.OpenDoor();
						open = true;
						broken = true;

						AudioManager.FindSoundManager().PlaySound(doorType.destructionSound);
				}

				private void OpenDoor()
				{
						slidingDoorController.OpenDoor();
						open = true;

						AudioManager.FindSoundManager().PlaySound(doorType.openingSound);
				}
		}
}