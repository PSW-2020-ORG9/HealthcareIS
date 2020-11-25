using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Service

{
    class MapObjectService : IMapObjectService
    {
        public MapObjectRepository MapObjectRepository = new MapObjectRepository();

        public List<MapObject> getAllMapObjects()
        {
            return MapObjectRepository.getAllMapObjects();
        }

        public MapObject update(MapObject mapObject)
        {
            return MapObjectRepository.update(mapObject);
        }
        public List<MapObject> getOutterMapObjects(List<MapObject> allMapObjects)
        {
            return MapObjectRepository.getOutterMapObjects(allMapObjects);
        }
        public MapObject findMapObjectById(int id)
        {
            return MapObjectRepository.findMapObjectById(id);
        }
    }
}
