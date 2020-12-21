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

        public List<MapObject> getAllFloorMapObjects()
        {
            return allMapObjectsOnFloor;
        }
        public MapObject addMapObject(MapObject mapObject)
        {
            allMapObjectsOnFloor.Add(mapObject);
            return mapObject;
        }
    }
}
