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
