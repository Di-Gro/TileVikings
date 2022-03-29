
class TileMap {
    private List<MTile> tiles;
    private List<MUnit> units;
    private List<MMapObject> objects;

    public void Init(TileMapData data);

    public void AddUnit();
    public void RemoveUnit();
    public void MoveUnit();

    public void AddObject();
    public void RemoveObject();

    public int IndexToInt(Vector2D index);

    public MTile GetTile(Vector2D index);
    public MTile GetTileAtMouse();

    public bool HasNext(MTile tile, Direction dir);
    public MTile GetNext(MTile tile, Direction dir);

    public List<MTile> FindTiles (
        MTile startTile,
        TileRequest request,
        bool single,
        int distance = 0,
        int actionPoints = 0);

    private bool MatchRequest(MTile tile, TileRequest request);
}

class TileMapData {
    int width;
    int height;
    List<TileType> tiles;
}

class MapObject {}

class MUnit {
    PlayerType owner;
    MTile tile;

    event OnHilight;
    event OnUnHilight;
    event OnAttack;

    public void Hilight(Color color);
    public void UnHilight();
    public void Attack(AttackResult result);
    public void Move(List<Tile> path);

    // Устанавливает клетку к которой нужно идти несколько ходов. 
    public void SetTarget(Tile tile);
}

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

class MProvince {
    PlayerType owner;
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

public void Init(TileMapData data) {

    for(int x = 0; in(0, data.width)) {
        for(int y = 0; in(0, data.height)){
            int index = IndexTpInt(x,y);
            MTile tile = MTile(data.tiles[index]);
            tiles.Add(tile);


        }
    }

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
