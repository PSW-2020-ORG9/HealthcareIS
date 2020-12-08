using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditor.Controller
{
    public class MapObjectController : IMapObjectController
    {
        private IMapObjectService IMapObjectService = new MapObjectService();

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
        public MapObject findMapObjectById(int id, List<MapObject> mapObjects)
        {
            return IMapObjectService.findMapObjectById(id, mapObjects);
        }
        public List<MapObject> checkMapObjectSearchInput(List<MapObject> mapObjects, string textBoxInput, string comboBoxTextInput)
        {
            return IMapObjectService.checkMapObjectSearchInput(mapObjects, textBoxInput, comboBoxTextInput);
        }
    }
}