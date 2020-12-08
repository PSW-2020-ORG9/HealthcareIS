using System;
using Xunit;
using WPFHospitalEditor.MapObjectModel;
using System.Collections.Generic;
using WPFHospitalEditor;

namespace WPFHospitalEditorUnitTests
{
    public class CreateObjectTest
    {
        [StaFact]
        public void createMapObject()
        {
            MapObject elevator1 = new MapObject("Elevator 1", 11, new MapObjectMetrics(new MapObjectCoordinates(750.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, new MapObjectDoor(MapObjectDoorOrientation.Left), "1-0&Max weight=480kg");

            Assert.NotNull(elevator1);
        }

        [StaFact]
        public void createHospitalMap()
        {
            MapObject elevator1 = new MapObject("Elevator 1", 11, new MapObjectMetrics(new MapObjectCoordinates(750.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, new MapObjectDoor(MapObjectDoorOrientation.Left), "1-0&Max weight=480kg");
            MapObject infos1 = new MapObject("Informations 1", 12, new MapObjectMetrics(new MapObjectCoordinates(0.0, 150.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.Informations, new MapObjectDoor(MapObjectDoorOrientation.Right), "1-0&Working Hours=07:00 - 00:00");
            List<MapObject> mapObjects = new List<MapObject>();
            mapObjects.Add(elevator1);
            mapObjects.Add(infos1);
            HospitalMap hospitalMap = new HospitalMap(mapObjects, Role.Director);
            Assert.NotNull(mapObjects);
        }
    }
}
