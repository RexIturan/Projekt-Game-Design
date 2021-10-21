namespace Level.Grid {
    [System.Serializable, System.Flags]
    public enum TileProperties {
        Walkable = 0x1,
        Opaque = 0x2,
        Destructible = 0x4,
        ShootTrough = 0x8,
        Solid = 0x10
    }
}