
// Перемещение:
//  Правая кнопка - выбор клетки для перемещения или юнита для атаки.
//  Левая кнопка - выбор другого юнита (смена интерактора)
//
// Найм юнита:
//  Левая кнопка - выбор другого домики или юнита (смена интерактора)
//  Правая кнопка - ничего
//
// Строительство:
//  Левая кнопка - выбор клетки
//  Выбора других интеракторов нет.

class InteractionManager {
    // Если события пробрасываются в интерактор, может нужно подвисать именно интерактор.

    private IInteractor runned;
    private List<IInteractor> waitedInteractors;

    // Сменить интерактор
    event OnInteractableChange(IInteractable interactable) {
        OnInteractorStop();

        runned = interactable.CreateInteractor();
        runned.BindEvents(this);
        runned.Start();
    }
    event OnInteractorStop() {
        runned?.Stop();
        runned?.RemoveEvents(this);
        runned?.Destroy();
    }
    event AddInteraction(){}
    event RunIntaruction(){}
    event HasInteraction(){}

    {
    event OnTileFocus(Tile tile){}
    event OnTileSelect(Tile tile){}
    event OnInteractableFocuse(IInteractable interactable){}
    event OnInteractableSelect(IInteractable interactable){}
    } => runned;
}
