[System.Serializable, System.Flags]
public enum ItemType
{
    Sellable= 1,
    Quest = 2,
    Weapon = 4,
    Armor = 8,
    Head = 16, // can you equip the Item as a hat/etc.
    Body = 32, 
    Boots = 64,
    Usable = 128
}