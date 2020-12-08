using System.Collections.Generic;
using Xunit;
using WPFHospitalEditor;
using WPFHospitalEditor.Repository;
using WPFHospitalEditor.MapObjectModel;
using Moq;
using Shouldly;
using WPFHospitalEditor.Service;

namespace WPFHospitalEditorUnitTests
{
    public class SearchMapObjectsOnHospitalMap
    {

        [StaFact]
        public void Map_object_search_for_existing_object()
        {
            var mapObjects = createMapObjectList();
            var searchResult = new List<MapObject>();

            MapObjectService mapObjectService = new MapObjectService();
            mapObjectService.iMapObjectRepository = createStubRepository();
            searchResult = mapObjectService.checkMapObjectSearchInput(mapObjects, "Informations", "Pick type of object");

            searchResult.ShouldNotBeEmpty();
        }

        [StaFact]
        public void Map_object_search_for_non_existing_object()
        {
            var mapObjects = createMapObjectList();
            var searchResult = new List<MapObject>();

            MapObjectService mapObjectService = new MapObjectService();
            mapObjectService.iMapObjectRepository = createStubRepository();
            searchResult = mapObjectService.checkMapObjectSearchInput(mapObjects, "", "Canteen");

            searchResult.ShouldBeEmpty();
        }
        private IMapObjectRepository createStubRepository()
        {
            var stubRepository = new Mock<IMapObjectRepository>();
            var mapObjects = createMapObjectList();

            stubRepository.Setup(m => m.getAllMapObjects()).Returns(mapObjects);
            return stubRepository.Object;
        }

        private List<MapObject> createMapObjectList()
        {
            var mapObjects = new List<MapObject>();
            MapObject elevator1 = new MapObject("Elevator 1", 11, new MapObjectMetrics(new MapObjectCoordinates(750.0, 170.0), new MapObjectDimensions(50.0, 60.0)), MapObjectType.Elevator, new MapObjectDoor(MapObjectDoorOrientation.Left), "1-0&Max weight=480kg");
            MapObject infos1 = new MapObject("Informations 1", 12, new MapObjectMetrics(new MapObjectCoordinates(0.0, 150.0), new MapObjectDimensions(100.0, 80.0)), MapObjectType.Informations, new MapObjectDoor(MapObjectDoorOrientation.Right), "1-0&Working Hours=07:00 - 00:00");
            mapObjects.Add(elevator1);
            mapObjects.Add(infos1);

            return mapObjects;
        }
    }
}
