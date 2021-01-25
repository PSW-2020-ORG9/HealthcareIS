using Moq;
using Shouldly;
using System.Collections.Generic;
using WPFHospitalEditor.MapObjectModel;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.Service;
using Xunit;

namespace WPFHospitalEditorUnitTests
{
    public class FindNeighbourMapObjectsTest
    {
        [StaFact]
        public void Find_non_existing_object()
        {
            MapObjectService mapObjectService = new MapObjectService(CreateStubRepository());

            List<MapObject> mapObjects = mapObjectService.GetNeighborMapObjects(11);

            mapObjects.ShouldNotBeEmpty();
        }

        private IMapObjectRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IMapObjectRepository>();
            var mapObjects = CreateMapObjectList();

            stubRepository.Setup(m => m.GetAllMapObjects()).Returns(mapObjects);
            stubRepository.Setup(m => m.GetMapObjectById(11)).Returns(mapObjects[0]);

            return stubRepository.Object;
        }

        private List<MapObject> CreateMapObjectList()
        {

            var mapObjects = new List<MapObject>();
            MapObject elevator1 = new MapObject("Examination room 1", 11, new MapObjectMetrics(new MapObjectCoordinates(750.0, 170.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.ExaminationRoom, new MapObjectDoor(MapObjectDoorOrientation.Left), new MapObjectDescription(1, 0, "Max weight=480kg"));
            MapObject infos1 = new MapObject("Examination room 2", 12, new MapObjectMetrics(new MapObjectCoordinates(850.0, 170.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.ExaminationRoom, new MapObjectDoor(MapObjectDoorOrientation.Right), new MapObjectDescription(1, 0, "Working Hours=07:00 - 00:00"));
            mapObjects.Add(elevator1);
            mapObjects.Add(infos1);

            return mapObjects;
        }
    }
}
