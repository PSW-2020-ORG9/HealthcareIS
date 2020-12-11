using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Repository
{
    public interface IMapObjectRepository
    {
        List<MapObject> getAllMapObjects();
        MapObject update(MapObject mapObject);
        void saveAll(List<MapObject> entities);
        List<MapObject> getOutterMapObjects();
        MapObject findMapObjectById(int id);
    }
}
