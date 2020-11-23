using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Repository
{
    class MapObjectRepository : IMapObjectRepository
    {
        private FileRepository Stream { get; set; }

        public MapObjectRepository(FileRepository stream)
        {
            this.Stream = stream;
        }

        public MapObject update(MapObject mapObject)
        {
            
            var allMapObjects = Stream.getAll().ToList(); 
            {
                foreach (MapObject mapObj in allMapObjects)
                {
                    updateMapObjectIfFound(mapObj, mapObject);
                }
            }
            Stream.saveAll(allMapObjects);
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
        }
        
        public List<MapObject> getAllMapObjects()
        {
            return Stream.getAll();
        }
            
    }
}
