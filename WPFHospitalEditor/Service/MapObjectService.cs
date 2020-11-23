using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Service

{
    class MapObjectService : IMapObjectService
    {
        private MapObjectRepository MapObjectRepository { get; set; }

        public MapObjectService(MapObjectRepository mapObjectRepository)
        {
            this.MapObjectRepository = mapObjectRepository;
        }

        public List<MapObject> getAllMapObjects()
        {
            return MapObjectRepository.getAllMapObjects();
        }

        public MapObject update(MapObject mapObject)
        {
            return MapObjectRepository.update(mapObject);
        }
    }
}
