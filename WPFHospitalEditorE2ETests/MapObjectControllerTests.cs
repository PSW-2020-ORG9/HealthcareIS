using System;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.MapObjectModel;
using Xunit;

namespace WPFHospitalEditorE2ETests
{
    public class MapObjectControllerTests
    {
        [StaFact]
        public void Find_existing_map_object()
        {
            MapObjectController mapObjectController = new MapObjectController();

            MapObject mapObject = mapObjectController.findMapObjectById(1);

            Assert.NotNull(mapObject);
        }

        [StaFact]
        public void Find_non_existing_map_object()
        {
            MapObjectController mapObjectController = new MapObjectController();

            MapObject mapObject = mapObjectController.findMapObjectById(101);

            Assert.Null(mapObject);
        }
    }
}
