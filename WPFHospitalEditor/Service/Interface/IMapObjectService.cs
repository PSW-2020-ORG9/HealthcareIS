using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Service
{
    public interface IMapObjectService
    {
        List<MapObject> getAllMapObjects();
        MapObject update(MapObject mapObject);
        List<MapObject> getOutterMapObjects();
        MapObject findMapObjectById(int id);
        List<MapObject> searchForMapObjects(string name, string type);
    }
}
