using Characters;
using Combat;
using Events.ScriptableObjects;
using SaveSystem.SaveFormats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WorldObjects
{
		public class SwitchComponent : MonoBehaviour
		{
				private const float EPSILON = 0.1f;

				[Header("Sending Events on:")]
				[SerializeField] private IntEventChannelSO switchActivatedEvent;

				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectEvent;

				public int switchId;
				public SwitchTypeSO switchType;

				public void Awake()
				{
						updateWorldObjectEvent.OnEventRaised += UpdateSwitch;
				}

				public void OnDestroy()
				{
						updateWorldObjectEvent.OnEventRaised -= UpdateSwitch;
				}

				public void Initialise(Switch_Save switch_Save, SwitchTypeSO switchType)
				{
						this.switchId = switch_Save.switchId;
						this.switchType = switchType;
						
						Instantiate(switchType.model, transform);

						gameObject.GetComponent<GridTransform>().gridPosition = switch_Save.gridPos;
				}

				// activates switch if conditions are met
				public void UpdateSwitch()
				{
						CharacterList characters = CharacterList.FindInstant();
						bool playerInRange = false;
						foreach(GameObject player in characters.playerContainer)
						{
								Vector3Int playerPos = player.GetComponent<GridTransform>().gridPosition;
								Vector3Int switchPos = gameObject.GetComponent<GridTransform>().gridPosition;
								if(Vector3Int.Distance(playerPos, switchPos) < (float) switchType.range + EPSILON)
								{
										playerInRange = true;
								}
						}

						if ( playerInRange )
								switchActivatedEvent.RaiseEvent(switchId);
				}
		}
}