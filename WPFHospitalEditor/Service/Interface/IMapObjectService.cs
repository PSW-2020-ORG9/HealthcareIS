using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor.Service
{
    public interface IMapObjectService
    {
        List<MapObject> getAllMapObjects();
        MapObject update(MapObject mapObject);
        List<MapObject> getOutterMapObjects();
        MapObject findMapObjectById(int id);
        void checkMapObjectSearchInput(string textBoxInput, string comboBoxTextInput);
    }
}
