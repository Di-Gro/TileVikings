class GameState {
    TurnTime time;
    Battle battle;

    public Battle CreateBattle(Province prov1, Province prov2) {
        time.StopUpdate();
        battle = new Battle(prov1, prov2);
        battle.Init();
        battle.BindEvents(this);
        return battle;
    }

    public TurnTime GetTime();
    public Battle GetBattle();
    public bool IsBattle();

    event OnBattleEnd() {
        Destroy(battle);
        battle = null;
        time.StartUpdate();
    }
}
