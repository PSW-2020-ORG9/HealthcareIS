using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Repository
{
    public class MapObjectRepository : IMapObjectRepository
    {
        private string path = AllConstants.MAPOBJECT_PATH;

        public MapObject update(MapObject mapObject)
        {
            
            var allMapObjects = getAll().ToList(); 
            {
                foreach (MapObject mapObj in allMapObjects)
                {
                    updateMapObjectIfFound(mapObj, mapObject);
                }
            }
            saveAll(allMapObjects);
            return mapObject;
            
        }
        
        public void updateMapObjectIfFound(MapObject mapObj, MapObject mapObjForUpdate)
        {
            if (mapObj.Id == mapObjForUpdate.Id)
            {
                editAllMapObjectAttributes(mapObj, mapObjForUpdate);
            }
        }

        public void editAllMapObjectAttributes(MapObject mapObj, MapObject mapObjForUpdate)
        {
            mapObj.Description = mapObjForUpdate.Description;
            mapObj.Name = mapObjForUpdate.Name;
            mapObj.selected = mapObjForUpdate.selected;
        }
        public List<MapObject> getAllMapObjects()
        {
            List<MapObject> allMapObjectsToReturn = new List<MapObject>();
            var allMapObjects = getAll();
            if(SearchResultDialog.selectedObject == null)
            {
                foreach (MapObject mapObject in allMapObjects)
                {
                    allMapObjectsToReturn.Add(mapObject);
                }
            }
            else
            {
                foreach (MapObject mapObject in allMapObjects)
                {
                    if (SearchResultDialog.selectedObject.Id == mapObject.Id)
                    {
                        mapObject.rectangle.Fill = Brushes.Red;
                    }
                    allMapObjectsToReturn.Add(mapObject);
                }
            }
            return allMapObjectsToReturn;
        }

        public List<MapObject> getAll()
        {
            string jsonString = File.Exists(path) ? File.ReadAllText(path) : "";
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            if (!string.IsNullOrEmpty(jsonString))
            {

                return JsonConvert.DeserializeObject<List<MapObject>>(jsonString, settings);
            }
            else
            {
                return new List<MapObject>();
            }
        }

        public List<MapObject> getOutterMapObjects()
        {
            List<MapObject> allOuterMapObjects = new List<MapObject>();
            var allMapObjects = getAll().ToList();
            if (SearchResultDialog.selectedObject == null)
            {
                foreach (MapObject mapObject in allMapObjects)
                {
                    if (mapObject.Description.Equals(""))
                    {
                        allOuterMapObjects.Add(mapObject);
                    }
                }
            }
            else
            {
                foreach (MapObject mapObject in allMapObjects)
                {
                    if (mapObject.Description.Equals(""))
                    {
                        if (SearchResultDialog.selectedObject.Id == mapObject.Id)
                        {
                            mapObject.rectangle.Fill = Brushes.Red;
                        }
                        allOuterMapObjects.Add(mapObject);
                    }
                }
            }
            return allOuterMapObjects;
        }

        public void saveAll(List<MapObject> entities)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Formatting = Formatting.Indented;
            serializer.ContractResolver = new ContractResolver();
            using (StreamWriter writer = new StreamWriter(path))
            using (JsonWriter jwriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jwriter, entities);
            }
        }
        public MapObject findMapObjectById(int id)
        {
            var allMapObjects = getAll().ToList();
            {
                foreach (MapObject mapObj in allMapObjects)
                {
                    if(mapObj.Id == id)
                    {
                        return mapObj;
                    }
                }
            }
            return null;
        }
    }
}
