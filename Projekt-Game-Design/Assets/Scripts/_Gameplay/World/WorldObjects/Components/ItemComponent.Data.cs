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
				Type = data.Type;
			}
		}

		public override ItemData Save() {
			var data = base.Save();
			data.Init(_itemData);
			return data;
		}

		public override void Load(ItemData data) {
			base.Load(data);

			_itemData = data;
		}
	}
}