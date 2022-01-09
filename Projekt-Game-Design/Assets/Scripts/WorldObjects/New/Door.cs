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
				public DoorSO doorType;
				public bool open;
				public bool locked;
				[SerializeField] private List<int> keyIds;
				[SerializeField] private List<int> switchIds;
				[SerializeField] private List<int> triggerIds;

				[SerializeField] private List<int> remainingSwitches;
				[SerializeField] private List<int> remainingTrigger;

				public void Initialise(Door_Save saveData, DoorSO doorType)
				{
						this.doorType = doorType;
						open = saveData.open;
						locked = true;

						if ( doorType.destructable )
								gameObject.AddComponent<Targetable>();

						Instantiate(doorType.model, transform);

						gameObject.GetComponent<GridTransform>().gridPosition = saveData.gridPos;

						Statistics stats = gameObject.GetComponent<Statistics>();
						stats.StatusValues.HitPoints.max = doorType.hitPoints;
						stats.StatusValues.HitPoints.value = saveData.currentHitPoints;

						keyIds = saveData.keyIds;
						switchIds = saveData.switchIds;
						triggerIds = saveData.triggerIds;
						remainingSwitches = saveData.remainingSwitchIds;
						remainingTrigger = saveData.remainingTriggerIds;
				}

				public void Awake()
				{
						updateWorldObjectsEvent.OnEventRaised += UpdateDoor;
						switchActivatedEvent.OnEventRaised += HandleSwitchActivatedEvent;
						triggerActivatedEvent.OnEventRaised += HandleTriggerActivatedEvent;
				}

				private void HandleSwitchActivatedEvent(int switchId)
				{
						remainingSwitches.Remove(switchId);
				}

				private void HandleTriggerActivatedEvent(int triggerId)
				{
						remainingTrigger.Remove(triggerId);
				}

				/**
				 * Checks the requirements for being locked or not. 
				 * Then checks if a player is next to it, and if a player is next to it, open. 
				 */
				private void UpdateDoor()
				{
						bool hasAllKeys = true;
						foreach(int keyId in keyIds)
						{
								bool hasKey = false;
								foreach(ItemSO item in inventory.playerInventory)
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
								foreach(GameObject player in characters.playerContainer)
								{
										Vector3Int playerPos = player.GetComponent<GridTransform>().gridPosition;
										Vector3Int doorPos = gameObject.GetComponent<GridTransform>().gridPosition;

										if ( Vector3Int.Distance(playerPos, doorPos) < OPENING_DISTANCE )
												playerInRange = true;
								}

								if ( playerInRange )
										open = true;
						}
				}
		}
}