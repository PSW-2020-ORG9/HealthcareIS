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
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput("", AllConstants.emptyComboBox);

            Assert.Empty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_empty_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput("", "Informations");

            Assert.NotEmpty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput("Info", AllConstants.emptyComboBox);

            Assert.NotEmpty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);

            mapObjectController.checkMapObjectSearchInput("Informations 1", "Informations");

            Assert.NotEmpty(HospitalMap.searchResult);
        }
    }
}
