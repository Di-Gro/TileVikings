class TileMapData {
    int width;
    int height;
    List<TileType> tiles;
}

class TileMap {
    private List<MTile> tiles;
    private List<MProvince> provinces;
    private List<MUnit> units;
    private List<MMapObject> objects;

    public void Init(TileMapData data);

    public bool CreateUnit(MTile tile);
    public void RemoveUnit(MTile tile);

    public bool CreateObject(List<MTile> where, ClassRef classRef);
    public void RemoveObject(MTile tile);

    // В методах Create и Remove можно создать объекты View представления,
    // если не будет отдельного TileMapView. 

    public int IndexToInt(Vector2D index);

    public MTile GetTile(Vector2D index);
    public MTile GetTileAtMouse();

    public List<MTile> FindTiles (
        MTile startTile,
        TileRequest request,
        bool single,
        int distance = 0,
        int actionPoints = 0);

    private bool MatchRequest(MTile tile, TileRequest request);

    // Убрать в Модель клетки ->
    public bool HasNext(MTile tile, Direction dir);
    public MTile GetNext(MTile tile, Direction dir);
    // <-
    // Убрать в MUnit
    public void MoveUnit();
    // <-

    public void Init(TileMapData data) {
        data - пока заменить на случаные типы клеток

        создать w на h клеток и добавить в список tiles
        создать список провинций
        связать клетки и провинции:
        записать в каждый тайл ссылку на провинцию
        в провинциях создать списки со ссылками на их клетки
    }

    public bool CreateUnit(Tile tile, PlayerType owner) {
        if(tile.HasUnit())
            return false;

        var unit = new MUnit();
        units.Add(unit);
        unit.tile = tile;
        unit.owner = owner;

        tile.unit = unit;
        return true;
    }

    public void RemoveUnit(MTile tile) {
        if(!tile.HasUnit())
            return;

        unit = tile.unit;
        unit.tile = null;
        tile.unit = null;
        units.Remove(unit);
        Destroy(unit)
    }

    public bool CreateObject(List<MTile> where, ClassRef classRef) {
        for(var tile in where) {
            if(tile.HasObject())
                return false;
        }
        var obj = classRef.Create();
        obj.tiles = where;
        objects.Add(obj);

        for(var tile in where)
            tile.object = obj;

        return true;
    }

    public void RemoveObject(MTile tile) {
        if(!tile.HasObject())
            return;

        var obj = tile.object;
        for(var tile in obj.tiles)
            tile.object = null;

        obj.tiles = null;
        Destroy(obj)
    }
}

class MapObject {
    List<MTile> tiles;
}

struct Cost -> Int Vector {
    int distance = -1;
    int actions = -1;
}

struct TileRequest {
    bool findTile = false;
    bool findUnit = false;
    bool findObject = false;

    bool findAnyUnit = false;
    bool findAnyObject = false;

    TileType tile;
    UnitType unit;
    MapObjectType object;
}

public List<MTile> FindTiles (
    MTile startTile,
    TileRequest request,
    bool single,
    int distance = 0,
    int actionPoints = 0)
{

    bool useDistance = distance > 0;
    bool useActions = actionPoints > 0;

    List<MTile> res;
    List<Cost> costs = List<Cost>(TileMap.tilesCount); // TODO: установить размер заранее
    List<MTile> queue = { startTile };

    costs[TileMap.IndexToInt(index)] = Cost(0, 0);

    for(var curentIndex in queue) {
        MTile tile = TileMap.GetTile(curentIndex);
        Cost cost = costs[TileMap.IndexToInt(tile.index)];

        if(MatchRequest(tile, request))
            res.Add(curentIndex);

        for(var d in Direction) {
            next = tile.GetNext(d);
            Cost nextCost = costs[TileMap.IndexToInt(next.index)];

            bool visited = nextCost.distance >= 0;
            if(visited)
                continue;

            nextCost = cost + Cost(1, ActionPoints[next.type]);
            costs[TileMap.IndexToInt(next.index)] = nextCost;

            bool canMove = true;

            if(useActions && nextCost.actions > actionPoints)
                canMove = false;
            if(useDistance && nextCost.distance > distance)
                canMove = false;

            is(canMove)
                queue.Add(next.index);
        }
    }

    if(single) {
        int minDist = int.maxValue;
        Vector2D resIndex;
        for(var tileIndex in res) {
            int dist = costs[TileMap.IndexToInt(tileIndex)].distance;
            if(dist < minDist) {
                minDist = dist;
                resIndex = tileIndex;
            }
        }
        res.clear();
        res.Add(resIndex);
    }

    return res;
}

private bool MatchRequest(Tile tile, TileRequest request) {
    bool res = true;

    if(request.findTile)
        res = res && tile.type == request.tile;

    if(request.findUnit) {
        var unit = TileMap.GetUnit(tile.unitId);
        var unitType = unit != null ? unit.type : Unit.Type.None;
        res = res && unitType == request.unit;
    }
    if(request.findObject) {
        var object = TileMap.GetObject(tile.mapObjectId);
        var ojectType = object != null ? object.type : MapObject.Type.None;
        res = res && ojectType == request.object;
    }
    return res;
}

public void Move(List<Tile> path) {
    Tile from = path.First();
    Tile to = path.Last();

    if(from.HasUnit())
        from.unit.Move(path);

    if(to.HasUnit())
        to.unit.Move(path.reverse);
}

void Client() {
    Tile tile;
    TileRequest request = {
        findTile = true,
        tile = Tile.Type.Plaint
    };

    var tiles = TileMap.FindTiles(tile.index, request, false, 0, 3);


}
