using Characters;
using GDP01.Gameplay.SaveTypes;
using SaveSystem.V2.Data;
using UnityEngine;

namespace WorldObjects {
	public abstract partial class WorldObject {
		[RequireComponent(typeof(GridTransform))]
		public abstract class Factory<T, D> : SaveObjectFactory<T, D>, ISaveState<D> where D : WorldObject.Data, new() where T : ISaveState<D> {
		
			[SerializeField] protected int id;
			public int Id => id;
		
			//Type
			[SerializeField] protected WorldObject.TypeSO _type;
			public WorldObject.TypeSO Type {
				get { return _type; }
				set { _type = value;  }
			}

			// Other Components
			[SerializeField] protected GridTransform _gridTransform;

			public GridTransform GridTransform => _gridTransform;
			public Vector3 Rotation {
				get { return _gridTransform.rotation; }
				protected set { _gridTransform.rotation = value; }
			}

			public Vector3Int GridPosition {
				get { return _gridTransform.gridPosition; }
				set {
					_gridTransform.gridPosition = value;
				}
			}

///// Public Methodes //////////////////////////////////////////////////////////////////////////////			
			
///// SaveState ////////////////////////////////////////////////////////////////////////////////////

			public virtual D Save() {
				return new D {
					Id = id,
					GridPosition = _gridTransform.gridPosition,
					Rotation = _gridTransform.rotation,
					ReferenceData = Type?.ToReferenceData()
				};
			}

			public virtual void Load(D data) {
				id = data.Id;
				_type = ( TypeSO )data.ReferenceData.obj;
			
				//Grid Position
				_gridTransform.gridPosition = data.GridPosition;
			}
		}
	}
}