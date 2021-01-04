using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Controller
{
    interface IMapObjectController
    {
        List<MapObject> GetAllMapObjects();
        MapObject Update(MapObject mapObject);
        List<MapObject> GetOutterMapObjects();
        MapObject GetMapObjectById(int id);
        List<MapObject> SearchMapObjects(string name, string type);
        List<MapObject> GetAllBuildingMapObjects(int id);
    }
}
