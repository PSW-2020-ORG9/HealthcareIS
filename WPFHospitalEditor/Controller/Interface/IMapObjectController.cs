using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Controller
{
    interface IMapObjectController
    {
        List<MapObject> GetAllMapObjects();
        MapObject Update(MapObject mapObject);
        List<MapObject> GetOutterMapObjects();
        MapObject FindMapObjectById(int id);
        List<MapObject> SearchForMapObjects(string name, string type);
    }
}
