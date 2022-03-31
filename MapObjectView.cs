
interface IMapObjectView {
    void Init(MapObject obj);
    
    event OnRemove();
}

class MapObject : IMapObject, IInteractable {
    MapObject obj;

    public void Init(MapObject obj) {
        obj.BindEvents(this);
    }

    event OnRemove() {
        obj.RemoveEvents(this);
        Destroy(this);
    }

    event OnHilight(Color color);
    event OnUnHilight();
}
