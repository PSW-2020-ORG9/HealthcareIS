using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Repository
{
    public class MapObjectRepository : IMapObjectRepository
    {
        private readonly string path = AllConstants.MAPOBJECT_PATH;

        public MapObject Update(MapObject mapObject)
        {           
            var allMapObjects = GetAllMapObjects().ToList(); 
            foreach (MapObject mapObj in allMapObjects)
            {
                    UpdateMapObjectIfFound(mapObj, mapObject);
            }
            SaveAll(allMapObjects);
            return mapObject;
            
        }
        
        public void UpdateMapObjectIfFound(MapObject mapObj, MapObject mapObjForUpdate)
        {
            if (mapObj.Id == mapObjForUpdate.Id)
            {
                EditAllMapObjectAttributes(mapObj, mapObjForUpdate);
            }
        }

        public void EditAllMapObjectAttributes(MapObject mapObj, MapObject mapObjectsForUpdate)
        {
            mapObj.Description = mapObjectsForUpdate.Description;
            mapObj.Name = mapObjectsForUpdate.Name;
        }

        public List<MapObject> GetAllMapObjects()
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

        public List<MapObject> GetOutterMapObjects()
        {
            List<MapObject> allOuterMapObjects = new List<MapObject>();
            var allMapObjects = GetAllMapObjects().ToList();
            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.Description.Equals(""))
                {
                    allOuterMapObjects.Add(mapObject);
                }
            }           
            return allOuterMapObjects;
        }

        public void SaveAll(List<MapObject> entities)
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
        
        public MapObject GetMapObjectById(int id)
        {
            var allMapObjects = GetAllMapObjects();
            foreach (MapObject mapObj in allMapObjects)
            {
                if (mapObj.Id == id)
                {
                    return mapObj;
                }
            }
            return null;
        }

        public List<MapObject> GetAllBuildingMapObjects(int id)
        {
            string jsonString = File.Exists(path) ? File.ReadAllText(path) : "";
            var settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };
            if (!string.IsNullOrEmpty(jsonString))
            {
                List<MapObject> buildingMapObjects = new List<MapObject>();
                List<MapObject> allMapObjects = JsonConvert.DeserializeObject<List<MapObject>>(jsonString, settings);
                foreach (MapObject mapobject in allMapObjects)
                {
                    if (mapobject.Description != "" && int.Parse(FindBuilding(mapobject)) == id)
                    {
                        buildingMapObjects.Add(mapobject);
                    }
                }
                return buildingMapObjects;
            }
            else
            {
                return new List<MapObject>();
            }
        }
        private String FindBuilding(MapObject mapObjectIteration)
        {
            String[] firstSplit = mapObjectIteration.Description.Split("&");
            String[] buildingIndex = firstSplit[0].Split("-");
            return buildingIndex[0];
        }
    }
}
