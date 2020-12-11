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
            var searchedMapObjects = new List<MapObject>();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            searchedMapObjects = mapObjectController.searchForMapObjects("", AllConstants.emptyComboBox);

            Assert.Empty(searchedMapObjects);
        }

        [StaFact]
        public void Search_by_empty_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            searchedMapObjects = mapObjectController.searchForMapObjects("", "Informations");

            Assert.NotEmpty(searchedMapObjects);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            searchedMapObjects = mapObjectController.searchForMapObjects("Info", AllConstants.emptyComboBox);

            Assert.NotEmpty(searchedMapObjects);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            var searchedMapObjects = new List<MapObject>();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            searchedMapObjects = mapObjectController.searchForMapObjects("Informations 1", "Informations");

            Assert.NotEmpty(searchedMapObjects);
        }
    }
}
