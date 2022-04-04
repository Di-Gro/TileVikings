
class Siege {
    int progress = Константа.ПродолжительностьОсады;
    UI ui;
}

class SiegeManager {

    Dict<Province, Siege> sieges;

    event OnStartSiege;
    event OnStopSiege;

    public void Init() {
        TurnTime.bindEvents(this);
    }

    public void CreateSiege(Province prov) {
        if(HasSiege(prov))
            return;

        sieges.Add(prov, new Siege());
    }

    public void StartSiege(Province prov) {
        if(!HasSiege(prov))
            return;

        ieges[prov].ui = UIHelper.Create(ClassRef<UISiege>());
        OnStartSiege?.Invoke();
    }

    public void CancelSiege(Province prov) {
        if(!HasSiege(prov))
            return;

        sieges[prov].ui?.Hide();
        sieges.Remove(prov);
    }

    private void StopSiege(Province prov) {
        sieges[prov].ui?.Hide();
        sieges.Remove(prov);
        OnStopSiege?.Invoke();
    }

    public bool HasSiege(Province prov) {}

    event OnTurnStart() {
        for(prov, siege in sieges) {
            siege.progress--;
            if(siege.progress == 0)
                StopSiege(prov);
        }
    }

}
