using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Service
{
    public interface IMapObjectService
    {
        List<MapObject> GetAllMapObjects();
        MapObject Update(MapObject mapObject);
        List<MapObject> GetOutterMapObjects();
        MapObject GetMapObjectById(int id);
        List<MapObject> SearchMapObjects(string name, string type);
        List<MapObject> GetAllBuildingMapObjects(int id);
        List<MapObject> GetNeigbourMapObjects(int roomId);
    }
}
