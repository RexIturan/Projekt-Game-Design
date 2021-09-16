[System.Serializable, System.Flags]
public enum EItemType
{
    SELLABLE= 1,
    QUEST = 2,
    WEAPON = 4,
    ARMOR = 8,
    HEAD = 16, // can you equip the Item as a hat/etc.
    BODY = 32, 
    BOOTS = 64,
    USABLE = 128
}