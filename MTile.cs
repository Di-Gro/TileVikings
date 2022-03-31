class MTile {
    public TileType type;
    public Vector2D index;
    public int arrayIndex;
    public MUnit unit;
    public MObject mapObject;
    public MProvince province;

    event OnHilight;
    event OnUnHilight;

    public bool HasUnit();

    // Должны вызвать события, которые слушает TileView
    public void Hilight(Color color);
    public void UnHilight();
}
