// Должен слушать события модели и визуализировать происходящее

interface IUnitView {
    void Init(MUnit unit);

    event OnRemove();
    event OnAttack();
    event OnMove(List<Tile> path);
}

class UnitView : IUnitView, IInteractable {
    MUnit unit;

    public void Init(MUnit unit) {
        unit.BindEvents(this);
    }

    event OnRemove() {
        unit.RemoveEvents(this);
        Destroy(this);
    }

    event OnHilight(Color color) {}
    event OnUnHilight() {}
    event OnAttack() {}
    event OnMove(List<Tile> path) {}

}
