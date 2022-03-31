class MProvince {
    int index;
    PlayerType owner;
    List<MTile> tiles;

    public bool HasUnit(PlayerType owner);
    public List<MUnit> GetUnits(PlayerType owner);

}
