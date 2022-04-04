class MUnit {

    // DONE // UnitType type;
    // DONE // PlayerType owner;
    // DONE // MTile tile;

    // DONE // event OnRemove;
    // DONE // event OnHilight(Color color);
    // DONE // event OnUnHilight;
    event OnAttack(AttackResult result);
    // DONE // event OnMove(List<Tile> path);

    // DONE // public void Hilight(Color color);
    // DONE // public void UnHilight();
    public void Attack(AttackResult result);

    //public void Move(List<Tile> path); // убрал в TileMap

    // Устанавливает клетку к которой нужно идти несколько ходов.
    public void SetTarget(Tile tile);

}
