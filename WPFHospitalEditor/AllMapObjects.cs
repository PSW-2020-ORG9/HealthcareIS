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

            MapObject road1 = new MapObject(3, new MapObjectMetrics(new MapObjectCoordinates(0.0, 20.0), new MapObjectDimensions(900.0, 20.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject road2 = new MapObject(4, new MapObjectMetrics(new MapObjectCoordinates(440.0, 0.0), new MapObjectDimensions(20.0, 550.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject road5 = new MapObject(5, new MapObjectMetrics(new MapObjectCoordinates(50.0, 330.0), new MapObjectDimensions(500.0, 20.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject road4 = new MapObject(6, new MapObjectMetrics(new MapObjectCoordinates(50.0, 460.0), new MapObjectDimensions(500, 20.0)), MapObjectType.Road, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject building1 = new MapObject(1, new MapObjectMetrics(new MapObjectCoordinates(20.0, 60.0), new MapObjectDimensions(380.0, 220.0)), MapObjectType.Building, "Zgrada1", new MapObjectDoor(MapObjectDoorOrientation.Right), "");
            MapObject building2 = new MapObject(2, new MapObjectMetrics(new MapObjectCoordinates(500.0, 60.0), new MapObjectDimensions(300.0, 220.0)), MapObjectType.Building, "Zgrada2", new MapObjectDoor(MapObjectDoorOrientation.Left), "");
            MapObject parking1 = new MapObject(7, new MapObjectMetrics(new MapObjectCoordinates(20.0, 300.0), new MapObjectDimensions(380.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject parking2 = new MapObject(8, new MapObjectMetrics(new MapObjectCoordinates(20.0, 430.0), new MapObjectDimensions(380.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject parking3 = new MapObject(9, new MapObjectMetrics(new MapObjectCoordinates(500.0, 300.0), new MapObjectDimensions(300.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");
            MapObject parking4 = new MapObject(10, new MapObjectMetrics(new MapObjectCoordinates(500.0, 430.0), new MapObjectDimensions(300.0, 80.0)), MapObjectType.Parking, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), "");

            allOuterMapObjects.Add(road1);
            allOuterMapObjects.Add(road2);
            allOuterMapObjects.Add(road5);
            allOuterMapObjects.Add(road4);
            allOuterMapObjects.Add(building1);
            allOuterMapObjects.Add(building2);
            allOuterMapObjects.Add(parking1);
            allOuterMapObjects.Add(parking2);
            allOuterMapObjects.Add(parking3);
            allOuterMapObjects.Add(parking4);

            for (int i = 0; i < 7; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(20 + (45.5 * (1 + i)) - 1 + 2 * i, 300.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
            for (int i = 0; i < 7; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(20 + (45.5 * (1 + i)) - 1 + 2 * i, 430.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
            for (int i = 0; i < 5; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(800 - (45.5 * (1 + i)) + 1 - 2 * i, 300.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
            for (int i = 0; i < 5; i++)
            {
                allOuterMapObjects.Add(new MapObject(0, new MapObjectMetrics(new MapObjectCoordinates(800 - (45.5 * (1 + i)) + 1 - 2 * i, 430.0), new MapObjectDimensions(2.0, 40.0)), MapObjectType.ParkingSlot, "", new MapObjectDoor(MapObjectDoorOrientation.NoDoors), ""));
            }
        }
    }
}
