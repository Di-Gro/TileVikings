class TileMapView {

    // Cкомбинированные меши тайлов
    // DONE // private Mesh tilesMesh;

    // Cкомбинированные меши подсветки тайлов
    // DONE // private Mesh tilesHilightMesh;

    // Нужно хранить индексы сегментов, чтобы подсветить отдельный тайл
    // private List<int> hilightSegmentIndexes; ну пригодилось

    // Визуализация границ провинций
    // DONE // private List<Spline> provinceBorders;

    public void Init(TileMapData data) {
        TileMap.BindEvents(this);

        //расставить меши клеток
        //объединить их в один меш
        //создать меши подсветки клеток
        //построить границы провинций
    }

    event OnHilightTile(MTile tile, Color color) {
        взять сегмент этого тайла в меше подсветки
        и изменить цвет вертексов
        либо создать динамическме материалы для каждого сегмента
        и изменить параметр цвета
    }

    event OnUnHilightTile(MTile tile) {}

    event OnUpdateProvince(MProvince prov) {
        // Может измениться владелец провинции
        // Или она моежт стать внутренней / внешней
        //перестроить границы провинции
        //обновить их цвет
    }

}
