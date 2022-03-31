class MUnit {

    UnitType type;
    PlayerType owner;
    MTile tile;

    event OnRemove;
    event OnHilight(Color color);
    event OnUnHilight;
    event OnAttack(AttackResult result);
    event OnMove(List<Tile> path);

    public void Hilight(Color color);
    public void UnHilight();
    public void Attack(AttackResult result);
    public void Move(List<Tile> path);

    // Устанавливает клетку к которой нужно идти несколько ходов.
    public void SetTarget(Tile tile);

}
