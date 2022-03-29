class IInteractor {
    public void OnInit();

    public void Start();
    public void Stop();

    event OnTileFocus;
    event OnTileSelect;
    event OnInteractableSelect;
    event OnInteractableFocuse;
}
