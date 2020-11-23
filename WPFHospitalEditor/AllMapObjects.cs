using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Service;
using System.IO;

namespace WPFHospitalEditor
{
    class AllMapObjects
    {
        public static List<MapObject> allOuterMapObjects = new List<MapObject>();
        public static List<MapObject> allFirstBuildingFirstFloorObjects = new List<MapObject>();
        public static List<MapObject> allFirstBuildingSecondFloorObjects = new List<MapObject>();
        public static List<MapObject> allSecondBuildingFirstFloorObjects = new List<MapObject>();
        public static List<MapObject> allSecondBuildingSecondFloorObjects = new List<MapObject>();
        public static List<MapObject> allMapObjects = new List<MapObject>();

        
        FileRepository path1 = new FileRepository(AllConstants.MAPOBJECT_PATH);
        MapObjectController mapObjectController = new MapObjectController(new MapObjectService(new MapObjectRepository(new FileRepository(AllConstants.MAPOBJECT_PATH))));
        
        public AllMapObjects()
        {                                                 
            
        }       
    }
}
