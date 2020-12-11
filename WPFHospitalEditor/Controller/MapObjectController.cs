using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Controller
{
    public class MapObjectController : IMapObjectController
    {
        private IMapObjectService IMapObjectService = new MapObjectService(new MapObjectRepository());

        public List<MapObject> getAllMapObjects()
        {
            return IMapObjectService.getAllMapObjects();
        }
        public MapObject update(MapObject mapObject)
        {
            return IMapObjectService.update(mapObject);
        }
        public List<MapObject> getOutterMapObjects()
        {
            return IMapObjectService.getOutterMapObjects();
        }
        public MapObject findMapObjectById(int id)
        {
            return IMapObjectService.findMapObjectById(id);
        }
        public List<MapObject> searchForMapObjects(string name, string type)
        {
            return IMapObjectService.searchForMapObjects(name, type);
        }
    }
}