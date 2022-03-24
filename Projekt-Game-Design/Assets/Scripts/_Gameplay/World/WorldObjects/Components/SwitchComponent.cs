using Audio;
using Characters;
using Events.ScriptableObjects;
using Events.ScriptableObjects.GameState;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace WorldObjects {
		public partial class SwitchComponent : WorldObject.Factory<SwitchComponent, SwitchComponent.SwitchData> {
				private const float EPSILON = 0.1f;

				[Header("Sending Events on:")]
				[SerializeField] private IntEventChannelSO switchActivatedEvent;

				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectEvent;
				[SerializeField] private PositionGameObjectEventChannelSO onTileEnterEC;

				[SerializeField] private SwitchAnimator switchAnimator;
				
				// [SerializeField] protected new SwitchTypeSO _type;
				public new SwitchTypeSO Type {
					get { return ( SwitchTypeSO )_type; }
					set { _type = value; }
				}

				[SerializeField] private SwitchData switchData;
				
				public float Range => switchData.Range;

				public bool Activated {
					get => switchData.Active;
					set => switchData.Active = value;
				}
				
				public bool IsActivated => Activated;
				public void ToggleState() => Activated = !Activated;

				public void Awake() {
						updateWorldObjectEvent.OnEventRaised += UpdateSwitch;
						// onTileEnterEC.OnEventRaised += (Vector3Int Position, GameObject obj) => UpdateSwitch();
				}

				public void OnDestroy() {
						updateWorldObjectEvent.OnEventRaised -= UpdateSwitch;
				}

				public void Initialise(Switch_Save switch_Save, SwitchTypeSO switchType) {
					switchData = switchType.ToComponentData();
					
					Activated = false;
						id = switch_Save.switchId;
						_type = switchType;

						Rotation = switch_Save.orientation;
						InitialiseOrientation();
						
						// Instantiate(switchType.model, transform);

						gameObject.GetComponent<GridTransform>().gridPosition = switch_Save.gridPos;

						if ( switch_Save.activated )
								SwitchActivated();
				}

				private void InitialiseOrientation()
				{
						gameObject.transform.rotation = Quaternion.LookRotation(Rotation);
				}

				// activates switch if conditions are met
				public void UpdateSwitch() {
						if ( !IsActivated ) {
								CharacterList characters = CharacterList.FindInstant();
								bool playerInRange = false;
								foreach ( GameObject player in characters.playerContainer ) {
										Vector3Int playerPos = player.GetComponent<GridTransform>().gridPosition;
										Vector3Int switchPos = gameObject.GetComponent<GridTransform>().gridPosition;
										if ( Vector3Int.Distance(playerPos, switchPos) < ( float )Range + EPSILON ) {
												playerInRange = true;
										}
								}

								if ( playerInRange )
										SwitchActivated();
						}
				}

				private void SwitchActivated() {
					Activated = true;
					switchAnimator.FlipSwitch();
					switchActivatedEvent.RaiseEvent(Id);

					//todo send event to play a sound
					AudioManager.FindSoundManager()?.PlaySound(Type.activationSound);
				}
		}
}