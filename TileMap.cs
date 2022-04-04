class TileMapData {
    int width;
    int height;
    List<TileType> tiles;
}

class TileMap {
    // DONE // private List<MTile> tiles;
    // DONE // private List<MProvince> provinces;
    // DONE // private List<MUnit> units;
    private List<MMapObject> objects;

    // DONE // private bool activeMode = false;
    // DONE // private List<Tile> m_activeTiles;

    // DONE // event OnCreateUnit(MUnit unit);
    // DONE // event OnHilightTile(MTile tile, Color color);
    // DONE // event OnUnHilightTile(MTile tile);
    // DONE // event OnActiveModeStart();
    // DONE // event OnActiveModeStop();

    // DONE // public void Init(TileMapData data);

    // DONE // public bool CreateUnit(MTile tile, UnitType type, PlayerType owner);
    // DONE // public void RemoveUnit(MTile tile);

    public bool CreateObject(List<MTile> where, MapObjectType type);
    public void RemoveObject(MTile tile);

    // Сообщают View, что нужно подсветить клетку
    // DONE // public void HilightTile(MTile tile, Color color) => OnHilightTile?.Invoke(tile, color);
    // DONE // public void UnHilightTile(MTile tile) => OnUnHilightTile?.Invoke(tile);

    // Возврщает соседнюю клетку в указанном направлении
    // public bool HasNext(MTile tile, Direction dir); Объединил с GetNext()
    // DONE // public MTile GetNext(MTile tile, Direction dir);

    // Перемещает юнит из начальной точки пути в конечную.
    // Если в двух клетках есть юниты, они меняются местами.
    // DONE // public void SwapUnits(List<Tile> path);

    // Запоминает список активных клеток
    // Помечает эти клетки как активные
    // Устанавливает ActiveMode
    // При снятии помечает сохраненные клетки неактивными
    // DONE // public void StartActiveMode(List<Tile> activeTiles);
    // DONE // public void StopActiveMode();
    // DONE // public bool HasActiveMode();
    // DONE //public List<Tile> GetActiveTiles();

    // DONE // public int IndexToInt(Vector2D index);
    // DONE // public bool IsValidIndex(Vector2D index);

    // DONE // public MTile GetTile(Vector2D index);
    //public MTile GetTileAtMouse(); убрал в interaclitn helper

    public List<MTile> FindTiles (
        MTile startTile,
        TileRequest request,
        bool single,
        int distance = 0,
        int actionPoints = 0);

    public List<Tile> FindPath(MTile fromTile, MTile toTile);

    private bool MatchRequest(MTile tile, TileRequest request);

    /////////////////////////////////////////////////

    public void Init(TileMapData data) {
        //data - пока заменить на случаные типы клеток

        // создать w на h клеток и добавить в список tiles
        // записать в клетку ее индекс
        // создать список провинций
        // записать в провинции их индексы
        // связать клетки и провинции:
        // записать в каждый тайл ссылку на провинцию
        // в провинциях создать списки со ссылками на их клетки
    }

    public bool CreateUnit(MTile tile, UnitType type, PlayerType owner) {
        //if(tile.HasUnit())
            //return false;

        //var unit = new MUnit();
        //units.Add(unit);
        //unit.tile = tile;
        //unit.type = type;
        //unit.owner = owner;

        //tile.unit = unit;

        //OnUnitCreate?.Invoke(unit);
        //return true;
    }

    public void RemoveUnit(MTile tile) {
        //if(!tile.HasUnit())
            //return;

        //unit.OnRemove?.Invoke();

        //unit = tile.unit;
        //unit.tile = null;
        //tile.unit = null;
        //units.Remove(unit);
        Destroy(unit) // Кажется, UObject разрушаются сборщиком мусора
    }

    public bool CreateObject(List<MTile> where, MapObjectType type) {
        for(var tile in where) {
            if(tile.HasObject())
                return false;
        }
        var obj = new MapObject();
        obj.type = type;
        obj.tiles = where;
        objects.Add(obj);

        for(var tile in where)
            tile.object = obj;

        OnCreateObject?.Invoke(obj);
        return true;
    }

    public void RemoveObject(MTile tile) {
        if(!tile.HasObject())
            return;

        var obj = tile.object;
        obj.OnRemove?.Invoke();

        for(var tile in obj.tiles)
            tile.object = null;

        obj.tiles = null;
        Destroy(obj)
    }

    public void SwapUnits(List<Tile> path) {
        //Tile from = path.First();
        //Tile to = path.Last();

        //if(from.HasUnit())
            //from.unit.OnMove?.Invoke(path);

        //if(to.HasUnit())
            //to.unit.OnMove?.Invoke(path.reverse);

        //var tmp = from.unit;
        //from.unit = to.unit;
        //to.unit = tmp;
    }
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



void Client() {
    Tile tile;
    TileRequest request = {
        findTile = true,
        tile = Tile.Type.Plaint
    };

    var tiles = TileMap.FindTiles(tile.index, request, false, 0, 3);


}
