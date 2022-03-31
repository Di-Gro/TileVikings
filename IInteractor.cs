interface IInteractor {
    void OnInit();

    void Start();
    void Stop();

    event OnTileFocus(Tile tile);
    event OnTileSelect(Tile tile);
    event OnInteractableSelect(IInteractable interactable);
    event OnInteractableFocuse(IInteractable interactable);
}
