using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
    public class MapObjectController : IMapObjectController
    {
        public IMapObjectService IMapObjectService = new MapObjectService();

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
        public void setAllSelectedFieldsToFalse()
        {
            IMapObjectService.setAllSelectedFieldsToFalse();
        }
    }
}