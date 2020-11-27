using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Service

{
    public class MapObjectService : IMapObjectService
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
        public List<MapObject> getOutterMapObjects()
        {
            return MapObjectRepository.getOutterMapObjects();
        }
        public MapObject findMapObjectById(int id)
        {
            return MapObjectRepository.findMapObjectById(id);
        }
        public void setAllSelectedFieldsToFalse()
        {
            MapObjectRepository.setAllSelectedFieldsToFalse();
        }
    }
}
