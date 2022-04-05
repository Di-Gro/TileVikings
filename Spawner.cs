class Spawner {

    public Dict<UnitType, ClassRef<IUnitView>> unitsPrefabs;
    public Dict<MapObjectType, ClassRef<IMapObject>> objectsPrefabs;

    event OnBeginPlay() {
        //TileMap.BindEvents(this);
    }

    event CreateUnit(MUnit unit) {
        //var classRef = unitsPrefabs[unit.type];
        //var unitView = classRef.Create();
        //unitView.Init(unit);
    }

    event CreateObject(MapObjectType type, Vector3 position) {
        var classRef = objectsPrefabs[obj.type];
        var obj = classRef.Create();
        obj.Init();
    }

}
