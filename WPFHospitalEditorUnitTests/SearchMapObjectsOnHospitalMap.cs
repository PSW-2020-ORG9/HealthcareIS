﻿using System.Collections.Generic;
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
            var mapObjects = CreateMapObjectList();
            var searchedMapObjects = new List<MapObject>();

            MapObjectService mapObjectService = new MapObjectService(createStubRepository());
            searchedMapObjects = mapObjectService.SearchMapObjects("Examination", AllConstants.EmptyComboBox);

            searchedMapObjects.ShouldNotBeEmpty();
        }

        [StaFact]
        public void Map_object_search_for_non_existing_object()
        {
            var mapObjects = CreateMapObjectList();
            var searchedMapObjects = new List<MapObject>();

            MapObjectService mapObjectService = new MapObjectService(createStubRepository());
            searchedMapObjects = mapObjectService.SearchMapObjects("", "Canteen");

            searchedMapObjects.ShouldBeEmpty();
        }
        private IMapObjectRepository createStubRepository()
        {
            var stubRepository = new Mock<IMapObjectRepository>();
            var mapObjects = CreateMapObjectList();
            stubRepository.Setup(m => m.GetAllMapObjects()).Returns(mapObjects);
            return stubRepository.Object;
        }

        private List<MapObject> CreateMapObjectList()
        {
            var mapObjects = new List<MapObject>();
            MapObject regular3 = new MapObject("Examination 3", 17, new MapObjectMetrics(new MapObjectCoordinates(280.0, 0.0), new MapObjectDimensions(140.0, 120.0)), MapObjectType.ExaminationRoom, new MapObjectDoor(MapObjectDoorOrientation.Down), new MapObjectDescription(1, 0, "Working Hours=09:00 - 21:00"));
            MapObject op1 = new MapObject("Surgery room 1", 18, new MapObjectMetrics(new MapObjectCoordinates(460.0, 0.0), new MapObjectDimensions(160.0, 120.0)), MapObjectType.SurgeryRoom, new MapObjectDoor(MapObjectDoorOrientation.Down), new MapObjectDescription(1, 0, "Working Hours=09:00 - 19:00"));
            mapObjects.Add(regular3);
            mapObjects.Add(op1);

            return mapObjects;
        }
    }
}
