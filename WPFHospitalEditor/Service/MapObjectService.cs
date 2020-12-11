using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Service

{
    public class MapObjectService : IMapObjectService
    {
        private IMapObjectRepository iMapObjectRepository = null;

        public MapObjectService(IMapObjectRepository IMapObjectRepository)
        {
            iMapObjectRepository = IMapObjectRepository;
        }

        public List<MapObject> getAllMapObjects()
        {
            return colorSelectedObject(iMapObjectRepository.getAllMapObjects());
        }

        public MapObject update(MapObject mapObject)
        {
            return iMapObjectRepository.update(mapObject);
        }
        public List<MapObject> getOutterMapObjects()
        {
            return colorSelectedObject(iMapObjectRepository.getOutterMapObjects());
        }
        public MapObject findMapObjectById(int id)
        {
            return iMapObjectRepository.findMapObjectById(id);
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

        public List<MapObject> searchForMapObjects(string name, string type)
        {
            var mapObjects = new List<MapObject>();
            List<MapObject> allMapObjects = iMapObjectRepository.getAllMapObjects();
            if (name.Equals("") && type.Equals(AllConstants.emptyComboBox)) return mapObjects;
            foreach (MapObject mapObject in allMapObjects)
            {
                if(compareInput(mapObject, name, type))
                    mapObjects.Add(mapObject);
            }
            return mapObjects;
        }

        private bool compareInput(MapObject mapObject, string name, string type)
        {
            bool result = mapObject.Name.ToLower().Contains(name.ToLower());
            if(!type.Equals(AllConstants.emptyComboBox))
            {
                result = result && mapObject.MapObjectType.ToString().Equals(type);
            }

            return result;
        }
    }
}
