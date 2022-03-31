class IInteractable {
    // Должен создать и параметризовать свой интерактор.
    public IInteractor CreateInteractor() {}

    event OnHilight(Color color);
    event OnUnHilight();
}
