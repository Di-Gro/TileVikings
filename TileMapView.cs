class TileMapView {

    // Cкомбинированные меши тайлов
    private Mesh tilesMesh;

    // Cкомбинированные меши тайлов
    private Mesh tilesHilightMesh;
    private List<int> hilightSegmentIndexes;

    public Dict<UnitType, ClassRef<IUnitView>> unitsPrefabs;

    //
    private List<MProvince> provinces;
    private List<MMapObject> objects; ?

    public void Init(TileMapData data);


    public bool CreateObject(List<MTile> where, ClassRef classRef);
    public void RemoveObject(MTile tile);

}
