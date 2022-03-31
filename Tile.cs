class MTile {
    public TileType type;
    public Vector2D index;
    public int arrayIndex;
    public MUnit unit;
    public MObject mapObject;
    public MProvince province;
    public bool active;

    public bool HasUnit();

    public void Hilight(Color color) { TileMap.HilightTile(this, color); }
    public void UnHilight() { TileMap.UnHilightTile(this); }

    public bool HasNext(Direction dir) { TileMap.HasNext(this, dir); };
    public MTile GetNext(Direction dir) { TileMap.GetNext(this, dir); };

    public bool IsActive() => !TileMap.HasActiveMode() || active;
}
