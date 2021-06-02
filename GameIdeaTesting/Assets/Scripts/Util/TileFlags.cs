namespace Util {
    [System.Serializable, System.Flags]
    public enum TileFlags {
        walkable = 0x1,
        opaque = 0x2,
        destructible = 0x4,
        shootTrough = 0x8,
    }
}