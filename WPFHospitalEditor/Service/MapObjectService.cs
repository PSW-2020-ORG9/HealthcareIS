using System.Collections.Generic;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Service

{
    public class MapObjectService : IMapObjectService
    {
        public MapObjectRepository MapObjectRepository = new MapObjectRepository();

        public List<MapObject> getAllMapObjects()
        {
            return colorSelectedObject(MapObjectRepository.getAllMapObjects());
        }

        public MapObject update(MapObject mapObject)
        {
            return MapObjectRepository.update(mapObject);
        }
        public List<MapObject> getOutterMapObjects()
        {
            return colorSelectedObject(MapObjectRepository.getOutterMapObjects());
        }
        public MapObject findMapObjectById(int id)
        {
            return MapObjectRepository.findMapObjectById(id);
        }
        private List<MapObject> colorSelectedObject(List<MapObject> allMapObjects)
        {
            foreach (MapObject mapObject in allMapObjects)
            {               
                if(SearchResultDialog.selectedObjectId == mapObject.Id)
                {
                    mapObject.rectangle.Fill = Brushes.Red;
                }              
            }
            return allMapObjects;
        }
    }
}
