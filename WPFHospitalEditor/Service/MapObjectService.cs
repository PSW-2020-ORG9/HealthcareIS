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
        public MapObject findMapObjectById(int id, List<MapObject> mapObjects)
        {
            return MapObjectRepository.findMapObjectById(id, mapObjects);
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

        public List<MapObject> checkMapObjectSearchInput(List<MapObject> mapObjects, string textBoxInput, string comboBoxTextInput)
        {
            List<MapObject> searchResult = new List<MapObject>();
            MessageBox.Show("Lista: " + mapObjects.Count + " TextBox: " + textBoxInput + "ComboBox: " + comboBoxTextInput);
            foreach (MapObject mapObject in mapObjects)
            {
                if (textBoxEmpty(mapObject, textBoxInput, comboBoxTextInput))
                {
                    searchResult.Add(mapObject);
                }
                else if (typeNotChosen(mapObject, textBoxInput, comboBoxTextInput))
                {
                    searchResult.Add(mapObject);
                }
                else if (bothParametersActive(mapObject, textBoxInput, comboBoxTextInput))
                {
                    searchResult.Add(mapObject);
                }
            }
            MessageBox.Show("SearchResult: " + searchResult.Count);
            return searchResult;
        }

        private bool textBoxEmpty(MapObject mapObject, string textBox, string comboBox)
        {
            return mapObject.MapObjectType.ToString().Equals(comboBox) && textBox.Equals("");
        }
        private bool typeNotChosen(MapObject mapObject, string textBox, string comboBox)
        {
            return mapObject.Name.ToLower().Contains(textBox.ToLower()) && comboBox.Equals("Pick type of object") && !textBox.Equals("");
        }
        private bool bothParametersActive(MapObject mapObject, string textBox, string comboBox)
        {
            return mapObject.MapObjectType.ToString().Equals(comboBox) && mapObject.Name.ToLower().Contains(textBox.ToLower()) && !textBox.Equals("");
        }        
    }
}
