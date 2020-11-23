using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
    class MapObjectController : IMapObjectController
    {
        private MapObjectService MapObjectService { get; set; }

        public MapObjectController(MapObjectService mapObjectService)
        {
            this.MapObjectService = mapObjectService;
        }

        public List<MapObject> getAllMapObjects()
        {
            return MapObjectService.getAllMapObjects();
        }
        public MapObject update(MapObject mapObject)
        {
            return MapObjectService.update(mapObject);
        }
    }
}