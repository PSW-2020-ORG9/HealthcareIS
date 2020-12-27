using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Repository
{
    public interface IMapObjectRepository
    {
        List<MapObject> GetAllMapObjects();
        MapObject Update(MapObject mapObject);
        void SaveAll(List<MapObject> entities);
        List<MapObject> GetOutterMapObjects();
        MapObject GetMapObjectById(int id);
    }
}
