using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Controller
{
    public class MapObjectController : IMapObjectController
    {
        private readonly IMapObjectService IMapObjectService = new MapObjectService(new MapObjectRepository());

        public List<MapObject> GetAllMapObjects()
        {
            return IMapObjectService.GetAllMapObjects();
        }
        public MapObject Update(MapObject mapObject)
        {
            return IMapObjectService.Update(mapObject);
        }
        public List<MapObject> GetOutterMapObjects()
        {
            return IMapObjectService.GetOutterMapObjects();
        }
        public MapObject FindMapObjectById(int id)
        {
            return IMapObjectService.FindMapObjectById(id);
        }
        public List<MapObject> SearchForMapObjects(string name, string type)
        {
            return IMapObjectService.SearchForMapObjects(name, type);
        }
    }
}