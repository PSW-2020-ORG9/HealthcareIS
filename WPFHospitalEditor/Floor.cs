using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
{
    public class Floor
    {
        private readonly List<MapObject> allMapObjectsOnFloor;

        public Floor(List<MapObject> allMapObjectsOnFloor)
        {
            this.allMapObjectsOnFloor = allMapObjectsOnFloor;
        }
        public Floor()
        {
            allMapObjectsOnFloor = new List<MapObject>();
        }

        public List<MapObject> GetAllFloorMapObjects()
        {
            return allMapObjectsOnFloor;
        }
        public MapObject AddMapObject(MapObject mapObject)
        {
            allMapObjectsOnFloor.Add(mapObject);
            return mapObject;
        }
    }
}
