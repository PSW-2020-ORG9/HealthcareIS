namespace WPFHospitalEditor.MapObjectModel
{
    public class MapObjectMetrics
    {
        public MapObjectCoordinates MapObjectCoordinates { get; set; }
        public MapObjectDimensions MapObjectDimensions { get; set; }

        public MapObjectMetrics(MapObjectCoordinates mapObjectCoordinates, MapObjectDimensions mapObjectDimensions)
        {
            this.MapObjectCoordinates = mapObjectCoordinates;
            this.MapObjectDimensions = mapObjectDimensions;
        }
    }
}