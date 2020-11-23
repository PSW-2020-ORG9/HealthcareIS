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

        public static char separator = Path.DirectorySeparatorChar;

        public static string MAPOBJECT_PATH = $"..{separator}..{separator}..{separator}Repository{separator}Data{separator}AllMapObjects.json";
        FileRepository path1 = new FileRepository(MAPOBJECT_PATH);
        MapObjectController mapObjectController1 = new MapObjectController(new MapObjectService(new MapObjectRepository(new FileRepository(MAPOBJECT_PATH))));

        public AllMapObjects()
        {                                                 
            allMapObjects = mapObjectController1.getAllMapObjects();
            loadMap(allMapObjects);
        }

        private void loadMap(List<MapObject> allMapObjects)
        {
            foreach (MapObject mapObject in allMapObjects)
            {
                if (mapObject.Description.Equals(""))
                {
                    allOuterMapObjects.Add(mapObject);
                }
                else
                {
                    string[] firstSplit = mapObject.Description.Split("&");
                    string[] buildingAndFloor = firstSplit[0].Split("-");
                    addObjectsToSpecificBuildingAndFloor(buildingAndFloor[0], buildingAndFloor[1], mapObject);                   
                }
            }
        }

        public void addObjectsToSpecificBuildingAndFloor(String building, String floor,MapObject mapObject)
        {
            if (building.Equals("1") && floor.Equals("0"))
            {
                allFirstBuildingFirstFloorObjects.Add(mapObject);
            }
            else if (building.Equals("1") && floor.Equals("1"))
            {
                allFirstBuildingSecondFloorObjects.Add(mapObject);
            }
            else if (building.Equals("2") && floor.Equals("0"))
            {
                allSecondBuildingFirstFloorObjects.Add(mapObject);
            }
            else if (building.Equals("2") && floor.Equals("1"))
            {
                allSecondBuildingSecondFloorObjects.Add(mapObject);
            }
        }
    }
}
