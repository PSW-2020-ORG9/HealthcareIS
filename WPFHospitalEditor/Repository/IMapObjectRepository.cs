using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Repository
{
    interface IMapObjectRepository
    {
        List<MapObject> getAllMapObjects();
        MapObject update(MapObject mapObject);
        void saveAll(List<MapObject> entities);
        List<MapObject> getAll();
        List<MapObject> getOutterMapObjects();
        MapObject findMapObjectById(int id);
        void setAllSelectedFieldsToFalse();
    }
}
