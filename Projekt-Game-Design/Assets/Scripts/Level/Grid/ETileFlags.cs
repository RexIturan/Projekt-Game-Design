namespace Level.Grid {
    [System.Serializable, System.Flags]
    public enum ETileFlags {
        walkable = 0x1,
        opaque = 0x2,
        destructible = 0x4,
        shootTrough = 0x8,
        solid = 0x10
    }
}