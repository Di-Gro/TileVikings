
// Связывает интерактор игрока с хелпером.
// Сообщает о начале и конце хода.
// При попытке завершить ход, завершает его,
// если у текущего интерактора пустой список действий.
// Если не пустой, сообщает интерактору, что нужно перейти к следующему действию.

class TurnTime {
    InteractionManager playerIM;
    InteractionManager enemyIM;

    event OnTurnStart;
    event OnTurnStop;

    public Init(playerIM, enemyIM) {
        playerIM.bindEvents(InteractionHelper.instance);
    }

    //public void Tick() { }

    // Сообщаем что игрок не можут сменить ход
    //public void OnPlayerBlockTurn();

    public void ToNextTurn() { }

    public InteractionManager GetPlayerIM();
    public InteractionManager GetAIIM();

}
