using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Repository;
using System;

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
        public MapObject GetMapObjectById(int id)
        {
            return IMapObjectService.GetMapObjectById(id);
        }
        public List<MapObject> SearchMapObjects(string name, string type)
        {
            return IMapObjectService.SearchMapObjects(name, type);
        }

        public List<MapObject> GetAllBuildingMapObjects(int id)
        {
            return IMapObjectService.GetAllBuildingMapObjects(id);
        }

        internal MapObject GetMapObjectById(object roomId)
        {
            throw new NotImplementedException();
        }

        public List<MapObject> GetNeighborMapObjects(int roomId)
        {
            return IMapObjectService.GetNeighborMapObjects(roomId);
        }
    }
}