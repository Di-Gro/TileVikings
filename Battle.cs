class Battle {
    TurnTime time;
    List<Tile> battleTiles;
    int playerUnitCount;
    int aiUnitCount;
    Province prov1;
    Province prov2;

    event OnBattleEnd;

    public void Init(Province prov1, Province prov2) {
        time = new TurnTime();
        time.Init(new InteractionManager(),  new InteractionManager());
        time.StartUpdate();

        battleTiles.Add(prov1.tiles);
        battleTiles.Add(prov2.tiles);
        TileMap.StartActiveMode(battleTiles);

        for(var tile in battleTiles)
            if(tile.HasUnit())
                tile.unit.BindEvents(this);

        UpdateSiege();
    }

    private void UpdateSiege() {
        if(в одной из провинций остались находятся юниты другого игрока) {
            SiegeManager.CreateSiege(tile.province);
            SiegeManager.StartSiege(tile.province);
        }
        if(в одной из провинций была осада, но сейчас нет юнитов ведущего ее игрока) {
            SiegeManager.CancelSiege(tile.province);
        }
    }

    event OnUnitDeath(Unit unit) {
        unit.RemoveEvents(this);

        if(unit.playerType == PlayerType.Player)
            playerUnitCount--;
        else
            aiUnitCount--;

        if(playerUnitCount == 0 || aiUnitCount == 0) {
            for(var tile in battleTiles)
                if(tile.HasUnit())
                    tile.unit.RemoveEvents(this);

            // Здесь можно расчитать результаты битвы, сколько юнитов погибло и т.д.
            // Чтобы выше это можно было посмотрет.
            TileMap.StopActiveMode();
            OnBattleEnd?.Invoke();
        }
    }
}
