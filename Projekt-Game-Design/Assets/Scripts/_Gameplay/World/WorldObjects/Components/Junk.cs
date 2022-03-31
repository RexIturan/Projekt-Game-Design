using Audio;
using Characters;
using Characters.Types;
using Combat;
using SaveSystem.SaveFormats;
using UnityEngine;

namespace WorldObjects
{
		public class Junk : MonoBehaviour
		{
				public bool broken;

				[Header("Receiving Events on:")]
				[SerializeField] private VoidEventChannelSO updateWorldObjectsEvent;

				[Header("Sending Events On:")]
				[SerializeField] private VoidEventChannelSO redrawLevelEC;

				// public int junkId;
				public JunkTypeSO junkType;

				public Vector3 orientation;

				public void Initialise(Junk_Save saveData, JunkTypeSO junkType)
				{
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
				}

				private void InitialiseOrientation()
				{
						gameObject.transform.rotation = Quaternion.LookRotation(orientation);
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
						if ( !broken && gameObject.GetComponent<Statistics>().StatusValues.HitPoints.IsMin() )
						{
								JunkDestroyed();
						}
				}

				private void JunkDestroyed() {
						broken = true;
						//todo ??????
						// TODO(vincent) refactor -> Drop.GetLoot -> LootSpawner.Spawn(Loot)
						// E_DropLoot_OnEnter.DropLoot(redrawLevelEC, junkType.drop, gameObject.GetComponent<GridTransform>().gridPosition);

						AudioManager.FindSoundManager().PlaySound(junkType.destructionSound);
				}
		}
}