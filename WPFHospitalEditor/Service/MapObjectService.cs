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
            if (textBoxInput.Equals("") && comboBoxTextInput.Equals(AllConstants.emptyComboBox)) return;
            foreach (MapObject mapObject in mapObjects)
            {
                if(compareInput(mapObject, textBoxInput, comboBoxTextInput))
                    HospitalMap.searchResult.Add(mapObject);
            }
        }

        private bool compareInput(MapObject mapObject, string textBoxInput, string comboBoxTextInput)
        {
            bool result = mapObject.Name.ToLower().Contains(textBoxInput.ToLower());
            if(!comboBoxTextInput.Equals(AllConstants.emptyComboBox))
            {
                result = result && mapObject.MapObjectType.ToString().Equals(comboBoxTextInput);
            }

            return result;
        }
    }
}
