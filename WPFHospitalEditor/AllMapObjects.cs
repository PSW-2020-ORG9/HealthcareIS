using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditor
{
    class AllMapObjects
    {
        public static List<MapObject> allOuterMapObjects = new List<MapObject>();
        public static List<MapObject> allFirstBuildingFirstFloorObjects = new List<MapObject>();
        public static List<MapObject> allFirstBuildingSecondFloorObjects = new List<MapObject>();
        public static List<MapObject> allSecondBuildingFirstFloorObjects = new List<MapObject>();
        public static List<MapObject> allSecondBuildingSecondFloorObjects = new List<MapObject>();

        public AllMapObjects()
        {
        }
    }
}
