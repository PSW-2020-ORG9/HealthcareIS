using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor;
using WPFHospitalEditor.MapObjectModel;

namespace WPFHospitalEditorIntegrationTests
{
    public class MapObjectSearch
    {
        [StaFact]
        public void Search_by_empty_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput(allMapObjects,"","Pick type of object");

            Assert.Empty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_empty_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput(allMapObjects, "", "Informations");

            Assert.NotEmpty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput(allMapObjects, "Info", "Pick type of object");

            Assert.NotEmpty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            List<MapObject> allMapObjects = mapObjectController.getAllMapObjects();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput(allMapObjects, "Informations 1", "Informations");

            Assert.NotEmpty(HospitalMap.searchResult);
        }
    }
}
