class UnitInteractor : IInteractor {

    public Unit unit;
    public Tile battleTile;

    private UIUnitWidget ui;
    private UIBattleTip battleTip;
    private UITileTip tileTip;
    private UITileTip unitTip;

    private List<Tile> availableTiles;
    private List<Tile> pathOnFocus;

    event OnInteractorStop;

    Init(Unit unit) {}
    Init(Unit unit, Tile battleTile) {}

    public void Start() {
        var ui = UIHelper.Create(ClassRef<UIUnitWidget>());
        ui.BindEvents(this);

        InteractionRules rules = {
            change = { Button.LeftMouse, Target.Interactable },
            focus = { Target.Tile, Target.Interactable },
            select = { Button.RightMouse }
        };
        InteractionHelper.SetRules(rules);

        if(battleTile == null) {
            OnMoveAttackSelected();
        } else {
            if(battleTile.HasUnit())
                Attack(battleTile);
            else
                Move(battleTile);
        }
    }

    public void Stop() {
        for (var tile in availableTiles)
            tile.UnHilight();
        unit.UnHilight();
        PathHelper.UnHilight();

        unit.RemoveEvents(this);
        ui.RemoveEvents(this);

        ui?.Hide();
        battleTip?.Hide();
        tileTip?.Hide();
    }

    private void StopSelf() {
        OnInteractorStop?.Invoke();
    }

    private void OnMoveAttackSelected() {
        TileRequest request = { findTile = true };
        availableTiles = TileMap.FindTiles(unit.tile, request, false, 0, unit.actionPoints);
        for (var tile in availableTiles)
            tile.Hilight(Colors.AvailableTile);
        unit.Hilight(Colors.SelectedUnit);
    }

    private void Attack(Tile tile) {
        if(TryStartBattle(tile))
            return;
        // Наверное юниты должны действовать согласованно
        // Поэтому нужно сообщить только одному
        // Либо должен быть отдельный класс,
        // Который будет управлять сразу двумя юнитами
        var result = GameState.AttackResult.Get(unit, tile.unit);
        unit.Attack(result);
        unit.BindEvents(this);
    }

    private void Move(Tile tile) {
        if(TryStartBattle(tile))
            return;
        UpdateSiege(tile);
        
        TileMap.MoveUnit(pathOnFocus);
        // unit.Move(pathOnFocus);
        // if(tile.HasUnit())
        //     tile.unit.Move(pathOnFocus.reverse);

        unit.BindEvents(this);
    }

    private bool TryStartBattle(Tile tile) {
        if(!GameState.IsBattle() && tile.province.HasUnit(PlayerType.Enemy)) {

            // Наверное, придется пробросить что-то еще
            var copy = new UnitInteractor(unit, tile);
            var battle = GameState.CreateBattle(););
            battle.time.GetPlayerIM().RunInteraction(copy);

            StopSelf();
            return true;
        }
        return false;
    }

    // Нужно вызывать до изменения модели.
    private void UpdateSiege(Tile tile) {
        if(нужно начать осаду) {
            var siege = SiegeManager.CreateSiege(tile.province);
        } else if (нужно снять осаду) {
            SiegeManager.CancelSiege(tile.province);
        }
    }

    private void OnEnemyFocus(Tile tile) {
        tile.Hilight(Colors.Attack);
        tile.unit.Hilight(Colora.Attack);
        var battleTip = UIHelper.Create(ClassRef<UIBattleTip>());
    }

    event OnEndAttack() { StopSelf(); }
    event OnEndMove() { StopSelf(); }

    event OnTileFocus(Tile tile) {
        if(tile.HasUnit()) {
            if(tile.unit.owner != unit.owner)
                OnEnemyFocus(tile);
            else
                unitTip = UIHelper.Create(ClassRef<UIUnitTip>());
        } else {
            tile.Hilight(Colors.FocusedTile);
            var tileTip = UIHelper.Create(ClassRef<UITileTip>());
        }
        pathOnFocus = TileMap.FindPath(unit.tile, tile);
        PathHelper.Hilight(path, Colors.Path);
    }

    event OnTileSelect(Tile tile) {
        if(availableTiles.Contains(tile)) {
            if(tile.HasUnit() && tile.unit.owner != unit.owner)
                Attack(tile);
            else if(CanMove(tile))
                Move(tile);

        } else {
            unit.SetTarget(tile);
        }
        StopSelf();
    }

    event OnInteractableFocuse(IInteractable interactable) {
        if(interactable is Unit) {
            unit = interactable as Unit;
            if(unit.owner != unit.owner)
                OnEnemyFocus(unit.tile);
            else
                unitTip = UIHelper.Create(ClassRef<UIUnitTip>());
        }
        pathOnFocus = TileMap.FindPath(unit.tile, tile);
        PathHelper.Hilight(path, Colors.Path);
    }

    event OnInteractableSelect(IInteractable interactable) {
        unit = interactable as Unit;
        if(availableTiles.Contaains(unit.tile)) {
            if(unit.owner != unit.owner)
                Attack(unit.tile);
            else if(CanMove(unit.tile))
                Move(unit.tile);
        } else {
            unit.SetTarget(nit.tile);
        }
        StopSelf();
    }

    event OnSleepSelect() {
        action.Stop();
        action = new UnitSleepAction();
        action.Start();
    }

}
