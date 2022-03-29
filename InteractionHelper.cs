class InteractionHelper {
    // Должен бросить рэйкаст
    // Применить правила выбора
    // Исключить неактивные клетки
    // Подсветить объекты стандартной подсветкой выбора (?)
    // Сообщить слушателям
    //  О переходе курсора на новую клетку
    //  О выборе клетки
    //  О выборе объекта

    // Правила выбора должны определить при каких условиях произойдет смена интерактор.
    // Все, что не попадает под условие смены тнтерактора, будет отправлено в интерактор

    // Игрок нажал на объект правой или левой кнопкой мыши
    event OnTileSelect;
    event OnInteractableSelect;
    // Объект оказался под курсором
    event OnTileFocus;
    event OnInteractableFocuse;
    // Выбрано что-то, что приводит к смене интерактора
    event OnInteractableChange;

    public void SetRules(InteractionRules rules);
}

class InteractionRules { }
// change - Смены интерактора
// focus - Нахождение под курсором
// select - Нажатие. Нажать можно только на то, что в фокусе

// пример:
InteractionRules rules = {

    // Для смены, нужно нажать ЛКМ на любом IInteractable
    change = { Button.LeftMouse, Target.Interactable },

    // Под фокусом мгот быть клетки и любой IInteractable
    focus = { Target.Tile, Target.Interactable },

    // Для выбора нужно нажать ПКМ
    select = { Button.RightMouse }

};
