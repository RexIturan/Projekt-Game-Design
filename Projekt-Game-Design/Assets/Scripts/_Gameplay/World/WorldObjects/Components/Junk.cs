using Audio;
using Characters;
using Characters.Types;
using Combat;
using Events.ScriptableObjects;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace WorldObjects
{
	[RequireComponent(typeof(Targetable))]
	[RequireComponent(typeof(Statistics))]
		public partial class Junk : WorldObject.Factory<Junk, Junk.JunkData>
		{
				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectsEvent;

				[Header("Sending Events On:")]
				[SerializeField] private VoidEventChannelSO redrawLevelEC;
				[SerializeField] private SoundEventChannelSO playSoundEC;

				public WorldObjectManager worldObjectManager;

				public new JunkTypeSO Type {
					get { return ( JunkTypeSO )_type; }
					set { _type = value; }
				}

				[SerializeField] private JunkData junkData;
				[SerializeField] private Targetable _targetable;
				[SerializeField] private Statistics _statistics;

				public bool Broken { get => junkData.broken; set => junkData.broken = value; }

				public bool IsBroken => Broken;

				public void InitFromSave(Junk_Save saveData, JunkTypeSO junkType) {
					_type = junkType;
					
					if(junkType.model) { 
						Instantiate(junkType.model, transform);
					}
					else
						Debug.LogError($"Junk type {junkType.name} (id: {junkType.id}) has no model. ");
						
					Broken = saveData.broken;

					GridTransform.RotateTo(saveData.orientation);

					_targetable = gameObject.AddComponent<Targetable>();
					_targetable.Initialise();

					GridTransform.MoveTo(saveData.gridPos);
					
					_statistics.StatusValues.InitValues(null);
					_statistics.StatusValues.HitPoints.Max = junkType.hitPoints;
					_statistics.StatusValues.HitPoints.Value = saveData.currentHitPoints;
					_statistics.SetFaction(Faction.Neutral);
					_statistics.SetArmorType(junkType.armorType);
				}

				public void Initialise(JunkTypeSO junkType)
				{
						_type = junkType;

						junkData.broken = false;

						GridTransform.RotateTo(Vector3.zero);

						_targetable = gameObject.AddComponent<Targetable>();
						_targetable.Initialise();

						GridTransform.MoveTo(Vector3.zero);

						_statistics.StatusValues.InitValues(null);
						_statistics.StatusValues.HitPoints.Max = junkType.hitPoints;
						_statistics.StatusValues.HitPoints.Value = junkType.hitPoints;
						_statistics.SetFaction(Faction.Neutral);

						// FirstUpdate();

						/*
						broken = saveData.broken;
						this.junkType = junkType;

						orientation = saveData.orientation;
						InitialiseOrientation();

						Instantiate(junkType.model, transform);

						if(junkType.destructable)
						{
								Targetable targetable = gameObject.AddComponent<Targetable>();
								targetable.Initialise();
						}

						gameObject.GetComponent<GridTransform>().gridPosition = saveData.gridPos;

						Statistics stats = gameObject.GetComponent<Statistics>();
						stats.StatusValues.HitPoints.Max = junkType.hitPoints;
						stats.StatusValues.HitPoints.Value = saveData.currentHitPoints;
						stats.SetFaction(Faction.Neutral);
						*/
				}

				public void Awake()
				{
						updateWorldObjectsEvent.OnEventRaised += UpdateJunk;
				}

				public void OnDestroy()
				{
						updateWorldObjectsEvent.OnEventRaised -= UpdateJunk;
				}

				/**
				 * Checks if junk is destroyed and if yes,
				 * drop loot from the loot table. 
				 */
				public void UpdateJunk()
				{
						if ( !IsBroken && gameObject.GetComponent<Statistics>().StatusValues.HitPoints.IsMin() )
						{
								JunkDestroyed();
						}
				}

				private void JunkDestroyed() {
						Broken = true;
						//todo ??????
						// TODO(vincent) refactor -> Drop.GetLoot -> LootSpawner.Spawn(Loot)
						// E_DropLoot_OnEnter.DropLoot(redrawLevelEC, junkType.drop, gameObject.GetComponent<GridTransform>().gridPosition);

						playSoundEC.RaiseEvent(Type.destructionSound);

						if ( worldObjectManager )
								worldObjectManager.RemoveJunkAt(transform.position);
						else
								Debug.LogError("No world object manager for junk set. Can't remove junk. ");
				}
		}
}