using Audio;
using Characters;
using Combat;
using Events.ScriptableObjects;
using SaveSystem.SaveFormats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		public class Door : MonoBehaviour
		{
				private const float OPENING_DISTANCE = 1.1f;

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
				[SerializeField] private List<int> keyIds;
				[SerializeField] private List<int> switchIds;
				[SerializeField] private List<int> triggerIds;

				[SerializeField] private List<int> remainingSwitches;
				[SerializeField] private List<int> remainingTrigger;

				[SerializeField] private Vector3 orientation;

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

						Instantiate(doorType.model, transform);

						gameObject.GetComponent<GridTransform>().gridPosition = saveData.gridPos;

						Statistics stats = gameObject.GetComponent<Statistics>();
						stats.StatusValues.HitPoints.max = doorType.hitPoints;
						stats.StatusValues.HitPoints.value = saveData.currentHitPoints;
						stats.SetFaction(Faction.Neutral);

						keyIds = saveData.keyIds;
						switchIds = saveData.switchIds;
						triggerIds = saveData.triggerIds;
						remainingSwitches = saveData.remainingSwitchIds;
						remainingTrigger = saveData.remainingTriggerIds;
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
						remainingSwitches.Remove(switchId);
						UpdateDoor();
				}

				private void HandleTriggerActivatedEvent(int triggerId)
				{
						remainingTrigger.Remove(triggerId);
						UpdateDoor();
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

												if ( playerInRange )
														DoorOpened();
										}
								}
						}
				}

				private void DoorDestroyed()
				{
						open = true;
						broken = true;

						SoundManager.FindSoundManager().PlaySound(doorType.destructionSound);
				}

				private void DoorOpened()
				{
						open = true;

						SoundManager.FindSoundManager().PlaySound(doorType.openingSound);
				}
		}
}