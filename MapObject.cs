class MapObject {
    MapObjectType type;
    List<MTile> tiles;

    event OnRemove;
    event OnHilight;
    event OnUnHilight;

    public void Hilight(Color color);
    public void UnHilight();
}
