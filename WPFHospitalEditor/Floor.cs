using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
{
    public class Floor
    {
        private List<MapObject> allMapObjectsOnFloor;

        public Floor(List<MapObject> allMapObjectsOnFloor)
        {
            this.allMapObjectsOnFloor = allMapObjectsOnFloor;
        }

        public List<MapObject> getAllFloorMapObjects()
        {
            return allMapObjectsOnFloor;
        }
    }
}
