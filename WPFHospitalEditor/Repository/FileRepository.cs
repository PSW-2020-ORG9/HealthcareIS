using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using Newtonsoft.Json;
using System.IO;

namespace WPFHospitalEditor.Repository
{
    class FileRepository
    {
        private string path;

        public FileRepository(string path)
        {
            this.path = path;
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

        public void saveBuilding(Building building)
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.TypeNameHandling = TypeNameHandling.All;
            serializer.Formatting = Formatting.Indented;
            serializer.ContractResolver = new ContractResolver();
            using (StreamWriter writer = new StreamWriter(path))
            using (JsonWriter jwriter = new JsonTextWriter(writer))
            {
                serializer.Serialize(jwriter, building);
            }
        }
    }
}
