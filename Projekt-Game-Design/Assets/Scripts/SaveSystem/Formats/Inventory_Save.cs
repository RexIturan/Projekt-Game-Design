using System;
using System.Collections.Generic;

namespace SaveSystem.SaveFormats {
    [Serializable]
    public class Inventory_Save {
        public int size;
        public List<int> itemIds;

        public Inventory_Save() {
            this.itemIds = new List<int>();
        }
    }
}