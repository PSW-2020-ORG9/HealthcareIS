using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Controller
{
    interface IMapObjectController
    {
        List<MapObject> getAllMapObjects();
        MapObject update(MapObject mapObject);
    }
}
