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
        public MapObjectRepository MapObjectRepository = new MapObjectRepository();
        public IMapObjectRepository iMapObjectRepository = new MapObjectRepository();

        public MapObjectService()
        {
            MapObjectRepository = new MapObjectRepository();
        }

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
            return MapObjectRepository.update(mapObject);
        }
        public List<MapObject> getOutterMapObjects()
        {
            return colorSelectedObject(MapObjectRepository.getOutterMapObjects());
        }
        public MapObject findMapObjectById(int id)
        {
            var allMapObjects = iMapObjectRepository.getAllMapObjects();
            foreach (MapObject mapObj in allMapObjects)
            {
                if (mapObj.Id == id)
                {
                    return mapObj;
                }
            }
            return null;
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

        public void checkMapObjectSearchInput(string textBoxInput, string comboBoxTextInput)
        {
            HospitalMap.searchResult.Clear();
            List<MapObject> mapObjects = iMapObjectRepository.getAllMapObjects();
            foreach (MapObject mapObject in mapObjects)
            {
                compareInput(mapObject, textBoxInput, comboBoxTextInput);               
            }
        }
        public void compareInput(MapObject mapObject, string textBox, string comboBox)
        {
            if(mapObject.MapObjectType.ToString().Equals(comboBox) && textBox.Equals(""))
            {
                HospitalMap.searchResult.Add(mapObject);
            }
            else if(mapObject.Name.ToLower().Contains(textBox.ToLower()) && comboBox.Equals("Pick type of object") && !textBox.Equals(""))
            {
                HospitalMap.searchResult.Add(mapObject);
            }
            else if(mapObject.MapObjectType.ToString().Equals(comboBox) && mapObject.Name.ToLower().Contains(textBox.ToLower()) && !textBox.Equals(""))
            {
                HospitalMap.searchResult.Add(mapObject);
            }
        }       
    }
}
