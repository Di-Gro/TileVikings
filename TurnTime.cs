
class TurnTime {
    InteractionManager playerIM;
    InteractionManager enemyIM;
    InteractionManager currentIM;

    event OnTurnStart;
    event OnTurnStop;

    public Init(playerIM, enemyIM) {
        playerIM.bindEvents(InteractionHelper.instance);
    }

    public void Tick() { }

    // Сообщаем что игрок не можут сменить ход
    public void OnPlayerBlockTurn();

    public void ToNextTurn() {
        if(currentIM.HasInteraction()) {

        }
    }

    public InteractionManager GetPlayerIM();
    public InteractionManager GetAIIM();

}
