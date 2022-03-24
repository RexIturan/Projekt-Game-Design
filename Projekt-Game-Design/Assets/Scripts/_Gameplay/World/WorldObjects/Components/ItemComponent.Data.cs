using System;
using UnityEngine;

namespace WorldObjects {
	public partial class ItemComponent {
		[Serializable]
		public class ItemData : WorldObject.Data {
			// public new ItemSO Type { get => ( ItemSO )_type; set => _type = value; }
			// [SerializeField] protected new ItemSO.ItemTypeData _type;
			// public new ItemSO.ItemTypeData Type { get => _type; set => _type = value; }

			public void Init(ItemData data) {
				ReferenceData = data.ReferenceData;
			}
		}

		public override ItemData Save() {
			var data = base.Save();
			_itemData.ReferenceData = Type.ToReferenceData();
			data.Init(_itemData);
			return data;
		}

		public override void Load(ItemData data) {
			base.Load(data);

			Type = ( ItemTypeSO )data.ReferenceData.obj;
			
			_itemData = data;
		}
	}
}