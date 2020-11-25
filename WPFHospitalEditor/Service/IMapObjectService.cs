using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Service
{
    interface IMapObjectService
    {
        List<MapObject> getAllMapObjects();
        MapObject update(MapObject mapObject);
        List<MapObject> getOutterMapObjects(List<MapObject> allMapObjects);
        MapObject findMapObjectById(int id);
    }
}
