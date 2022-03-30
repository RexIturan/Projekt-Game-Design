using Audio;
using Characters;
using Combat;
using Events.ScriptableObjects;
using SaveSystem.SaveFormats;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters.Types;
using GDP01._Gameplay.Provider;
using GDP01._Gameplay.World.Character;
using UnityEngine;
using WorldObjects.Doors;

namespace WorldObjects
{
	[RequireComponent(typeof(Targetable))]
	[RequireComponent(typeof(Statistics))]
		public partial class Door : WorldObject.Factory<Door, Door.DoorData> {
				private const float OPENING_DISTANCE = 1.1f;

				//todo refactor this replace with inventory query event channel
				[SerializeField] private InventorySO inventory;

				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectsEvent;
				[SerializeField] private IntEventChannelSO switchActivatedEvent;
				[SerializeField] private IntEventChannelSO triggerActivatedEvent;
				[SerializeField] private SlidingDoorController slidingDoorController;

				public new DoorTypeSO Type {
					get { return ( DoorTypeSO )_type; }
					set { _type = value; }
				}

				[SerializeField] private DoorData doorData;
				[SerializeField] private Targetable _targetable;
				[SerializeField] private Statistics _statistics;
				
				public bool Open { get => doorData.open; set => doorData.open = value; }
				public bool Broken { get => doorData.broken; set => doorData.broken = value; }
				public bool Locked { get => doorData.locked; set => doorData.locked = value; }

				public List<int> Keys {
					get => doorData.keyIds; private set => doorData.keyIds = value;
				}
				
				public List<int> Switches { 
					get => doorData.switchIds; private set => doorData.switchIds = value; 
				}

				public List<int> Triggers {
					get => doorData.triggerIds; private set => doorData.triggerIds = value;
				}

				public List<int> RemainingSwitches {
					get => doorData.remainingSwitches; 
					private set => doorData.remainingSwitches = value;
				}

				public List<int> RemainingTriggers {
					get => doorData.remainingTrigger; 
					private set => doorData.remainingTrigger = value;
				}

				public bool IsOpen => Open;
				public bool IsBroken => Broken;
				public bool IsLocked => Locked;



				public void InitFromSave(Door_Save saveData, DoorTypeSO doorType) {
					_type = doorType;
						
					doorData.open = saveData.open;
					doorData.locked = true;
					doorData.broken = false;

					GridTransform.RotateTo(saveData.orientation);

					_targetable = gameObject.AddComponent<Targetable>();
					_targetable.Initialise();

					GridTransform.MoveTo(saveData.gridPos);
					
					_statistics.StatusValues.InitValues(null);
					_statistics.StatusValues.HitPoints.Max = doorType.hitPoints;
					_statistics.StatusValues.HitPoints.Value = saveData.currentHitPoints;
					_statistics.SetFaction(Faction.Neutral);

					Keys = saveData.keyIds;
					Switches = saveData.switchIds;
					Triggers = saveData.triggerIds;
					RemainingSwitches = saveData.remainingSwitchIds;
					RemainingTriggers = saveData.remainingTriggerIds;

					FirstUpdate();
				}
				
				public void Initialise(DoorTypeSO doorType) {
						_type = doorType;
						
						doorData.open = false;
						doorData.locked = true;
						doorData.broken = false;

						GridTransform.RotateTo(Vector3.zero);

						_targetable = gameObject.AddComponent<Targetable>();
						_targetable.Initialise();

						GridTransform.MoveTo(Vector3.zero);

						_statistics.StatusValues.InitValues(null);
						_statistics.StatusValues.HitPoints.Max = doorType.hitPoints;
						_statistics.StatusValues.HitPoints.Value = doorType.hitPoints;
						_statistics.SetFaction(Faction.Neutral);

						Keys = new List<int>();
						Switches = new List<int>();
						Triggers = new List<int>();
						RemainingSwitches = new List<int>();
						RemainingTriggers = new List<int>();

						FirstUpdate();
				}

				private void FirstUpdate() {
					if ( Keys.Count > 0 ) {
						slidingDoorController.InitValues(DoorType.Key, Keys.Count);
					} else if ( Switches.Count > 0 ) {
						slidingDoorController.InitValues(DoorType.Switch, Switches.Count);
					}

					if ( RemainingSwitches.Count < Switches.Count ) {
						var activeSwitches = Switches.Count - RemainingSwitches.Count;
						for ( int i = 0; i < activeSwitches; i++ ) {
							slidingDoorController.OpenLock();
						}
					}

					UpdateDoor();
						
					if ( IsOpen ) {
						OpenDoor();
					}
				}
				
				// causes the model's front face to go into the direction 
				// that is specified by the vector in orientation
				// for example: orientation (1, 0) would cause the door to have its front face
				// faced towards the positive x-Axis-Vector
				// private void InitialiseOrientation()
				// {
				// 	gameObject.transform.rotation = Quaternion.LookRotation(Rotation);
				// }

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
						if (Switches.Contains(switchId) ) {
							RemainingSwitches.Remove(switchId);
							slidingDoorController.OpenLock();
							UpdateDoor();
						}
				}

				private void HandleTriggerActivatedEvent(int triggerId)
				{
					if (Switches.Contains(triggerId) ) {
						RemainingSwitches.Remove(triggerId);
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
						if ( !IsBroken && !IsOpen )
						{
								if ( gameObject.GetComponent<Statistics>().StatusValues.HitPoints.IsMin() )
								{
										DoorDestroyed();
								}
								else
								{
										bool hasAllKeys = true;
										foreach ( int keyId in Keys )
										{
												bool hasKey = false;
												foreach ( ItemTypeSO item in inventory.InventorySlots )
												{
														if ( item.id == keyId )
																hasKey = true;
												}
												if ( !hasKey )
														hasAllKeys = false;
										}

										// hasAllKeys = keyIds.All(i => inventory.playerInventory.Any(item => item.id == i));

										if ( hasAllKeys && RemainingSwitches.Count == 0 && RemainingTriggers.Count == 0 )
												Locked = false;
										
										if ( !IsLocked )
										{
												bool playerInRange = false;

												if ( !Type.openManually )
														playerInRange = true;
												else
												{
													Vector3Int doorPos = gameObject.GetComponent<GridTransform>().gridPosition;
													
													List<PlayerCharacterSC> foundPlayerCharacters = 
														GameplayProvider.Current.CharacterManager.GetPlayerCharactersWhere(
															(player) => Vector3Int.Distance(player.GridPosition, doorPos) <
															            OPENING_DISTANCE).ToList();
													
													if ( foundPlayerCharacters is { Count: >0 } ) {
														playerInRange = true;
													}
														// foreach ( GameObject player in GameplayProvider.Current.CharacterManager.playerContainer )
														// {
														// 		Vector3Int playerPos = player.GetComponent<GridTransform>().gridPosition;
														// 		
														//
														// 		if ( Vector3Int.Distance(playerPos, doorPos) < OPENING_DISTANCE )
														// 				playerInRange = true;
														// }
												}

												if ( playerInRange || Switches.Count > 0)
														OpenDoor();
										}
								}
						}
				}

				private void DoorDestroyed()
				{
						slidingDoorController.OpenDoor();
						Open = true;
						Broken = true;

						AudioManager.FindSoundManager()?.PlaySound(Type.destructionSound);
				}

				private void OpenDoor()
				{
						slidingDoorController.OpenDoor();
						Open = true;

						AudioManager.FindSoundManager()?.PlaySound(Type.openingSound);
				}
		}
}