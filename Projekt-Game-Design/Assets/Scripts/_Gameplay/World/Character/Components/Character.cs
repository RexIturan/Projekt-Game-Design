﻿using System.Collections.Generic;
using Characters;
using Characters.Movement;
using Combat;
using GDP01._Gameplay.World.Character.Data;
using GDP01.Characters.Component;
using GDP01.Gameplay.SaveTypes;
using GDP01.World.Components;
using SaveSystem.V2.Data;
using UnityEngine;
using Visual.Healthbar;

namespace GDP01._Gameplay.World.Character.Components {
	
	[RequireComponent(typeof(Statistics))]
	[RequireComponent(typeof(GridTransform))]
	[RequireComponent(typeof(Attacker))]
	[RequireComponent(typeof(MovementController))]
	[RequireComponent(typeof(AbilityController))]
	[RequireComponent(typeof(ModelController))]
	[RequireComponent(typeof(Targetable))]
	public abstract class Character<T, D> : SaveObjectFactory<T, D>, ISaveState<D> where D : CharacterData, new() where T : ISaveState<D> {
		public int id;
		protected bool active;

		[SerializeField] protected CharacterTypeSO _type;
		
		//todo get from gameobject
		[SerializeField] protected Attacker _attacker;
		[SerializeField] protected Statistics _statistics;
		[SerializeField] protected Targetable _targetable;
		[SerializeField] protected GridTransform _gridTransform;
		[SerializeField] protected ModelController _modelController;
		[SerializeField] protected AbilityController _abilityController;
		[SerializeField] protected MovementController _movementController;
		[SerializeField] protected HealthbarController _healthbarController;

///// Properties ///////////////////////////////////////////////////////////////////////////////////		
		
		public GridTransform GridTransform => _gridTransform;
		public Vector3 Rotation {
			get { return _gridTransform.rotation; }
			protected set { _gridTransform.rotation = value; }
		}

		public Vector3Int GridPosition {
			get { return _gridTransform.gridPosition; }
			protected set {
				_gridTransform.gridPosition = value;
			}
		}

		public bool IsActive => active;
		
		//targetable
		public Targetable TargetableComponent => _targetable;
		public bool IsDead => _targetable.IsDead;
		public bool IsAlive => _targetable.IsAlive;

		public AbilityController AbilityController => _abilityController;
		
		public Statistics Statistics => _statistics;
		
///// SaveState ////////////////////////////////////////////////////////////////////////////////////		
		
		public virtual D Save() {
			//todo create DataSave
			return new D {
				Id = id,
				Active = active,
				Icon = _statistics.DisplayImage,
				Faction = _statistics.Faction,
				GridPosition = _gridTransform.gridPosition,
				Type = _type,
				
				//todo override -> CharacterSO
				Stats = _statistics.StatusValues,
				//todo -> CharacterSO
				MovementPointsPerEnergy = _movementController.movementPointsPerEnergy,
				MovementCostPerTile = _movementController.movementCostPerTile,
			};
		}

		public virtual void Load(D data) {
			id = data.Id;
			active = data.Active;
			
			//stats
			_statistics.StatusValues.InitValues(data.Stats.Stats);
			_statistics.SetFaction(data.Faction);
			_statistics.DisplayImage = data.Icon;

			//movement Position
			_movementController.movementPointsPerEnergy = data.MovementPointsPerEnergy;
			_movementController.movementCostPerTile = data.MovementCostPerTile;

			//Grid Position
			_gridTransform.gridPosition = data.GridPosition;

			//model
			// _modelController.prefab = playerType.modelPrefab;
			_modelController.Initialize(data.Type);
			
			//todo use base type for meshes and prefab
			
			_modelController.SetFactionMaterial(data.Faction);

			//Abilities
			_abilityController.BaseAbilities = data.BasicAbilities;

			_targetable.Initialise();
			
			//todo init components from data
		}
	}
}