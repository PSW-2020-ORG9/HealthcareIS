using System;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorIntegrationTests
{
    public class MapObjectControllerTest
    {
        [StaFact]
        public void Find_existing_map_object()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var allMapObjects = mapObjectController.getAllMapObjects();
            MapObject mapObject = mapObjectController.findMapObjectById(1, allMapObjects);

            Assert.NotNull(mapObject);
        }

        [StaFact]
        public void Find_non_existing_map_object()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var allMapObjects = mapObjectController.getAllMapObjects();
            MapObject mapObject = mapObjectController.findMapObjectById(101, allMapObjects);

            Assert.Null(mapObject);
        }
    }
}
