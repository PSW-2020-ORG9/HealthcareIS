using System.Collections.Generic;
using System.Windows.Media;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;

namespace WPFHospitalEditor.Service

{
    public class MapObjectService : IMapObjectService
    {
        private readonly IMapObjectRepository iMapObjectRepository = null;

        public MapObjectService(IMapObjectRepository IMapObjectRepository)
        {
            iMapObjectRepository = IMapObjectRepository;
        }

        public List<MapObject> GetAllMapObjects()
        {
            return iMapObjectRepository.GetAllMapObjects();
        }

        public MapObject Update(MapObject mapObject)
        {
            return iMapObjectRepository.Update(mapObject);
        }

        public List<MapObject> GetOutterMapObjects()
        {
            return iMapObjectRepository.GetOutterMapObjects();
        }

        public MapObject GetMapObjectById(int id)
        {
            return iMapObjectRepository.GetMapObjectById(id);
        }

        public List<MapObject> SearchMapObjects(string name, string type)
        {
            var mapObjects = new List<MapObject>();           
            if (string.IsNullOrEmpty(name) && type.Equals(AllConstants.EmptyComboBox)) return mapObjects;
            List<MapObject> allMapObjects = iMapObjectRepository.GetAllMapObjects();
            foreach (MapObject mapObject in allMapObjects)
            {
                if(CompareInput(mapObject, name, type))
                    mapObjects.Add(mapObject);
            }
            return mapObjects;
        }

        private bool CompareInput(MapObject mapObject, string name, string type)
        {
            bool result = mapObject.Name.ToLower().Contains(name.ToLower());
            if(!type.Equals(AllConstants.EmptyComboBox))
            {
                result = result && mapObject.MapObjectType.ToString().Equals(type);
            }

            return result;
        }

        public List<MapObject> GetAllBuildingMapObjects(int id)
        {
            return iMapObjectRepository.GetAllBuildingMapObjects(id);
        }
    }
}
