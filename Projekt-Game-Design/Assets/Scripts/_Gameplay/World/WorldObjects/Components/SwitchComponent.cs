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
		public class SwitchComponent : MonoBehaviour
		{
				private const float EPSILON = 0.1f;

				[Header("Sending Events on:")]
				[SerializeField] private IntEventChannelSO switchActivatedEvent;

				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectEvent;

				[SerializeField] private SwitchAnimator switchAnimator;
				
				public int switchId;
				public SwitchTypeSO switchType;
				[SerializeField] private bool activated;
				public Vector3 orientation;
				
				public bool IsActivated {
					get { return activated; }
				}
				
				public void ToggleState() {
					activated = !activated;
				}

				public void Awake() {
						updateWorldObjectEvent.OnEventRaised += UpdateSwitch;
				}

				public void OnDestroy() {
						updateWorldObjectEvent.OnEventRaised -= UpdateSwitch;
				}

				public void Initialise(Switch_Save switch_Save, SwitchTypeSO switchType)
				{
						activated = false;
						this.switchId = switch_Save.switchId;
						this.switchType = switchType;

						orientation = switch_Save.orientation;
						InitialiseOrientation();
						
						// Instantiate(switchType.model, transform);

						gameObject.GetComponent<GridTransform>().gridPosition = switch_Save.gridPos;

						if ( switch_Save.activated )
								SwitchActivated();
				}

				private void InitialiseOrientation()
				{
						gameObject.transform.rotation = Quaternion.LookRotation(orientation);
				}

				// activates switch if conditions are met
				public void UpdateSwitch() {
						if ( !activated ) {
								CharacterList characters = CharacterList.FindInstant();
								bool playerInRange = false;
								foreach ( GameObject player in characters.playerContainer ) {
										Vector3Int playerPos = player.GetComponent<GridTransform>().gridPosition;
										Vector3Int switchPos = gameObject.GetComponent<GridTransform>().gridPosition;
										if ( Vector3Int.Distance(playerPos, switchPos) < ( float )switchType.range + EPSILON ) {
												playerInRange = true;
										}
								}

								if ( playerInRange )
										SwitchActivated();
						}
				}

				private void SwitchActivated() {
					activated = true;
					switchAnimator.FlipSwitch();
					switchActivatedEvent.RaiseEvent(switchId);

					AudioManager.FindSoundManager().PlaySound(switchType.activationSound);
				}
		}
}