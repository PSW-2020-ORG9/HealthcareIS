using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor;

namespace WPFHospitalEditorIntegrationTests
{
    public class MapObjectSearch
    {
        [StaFact]
        public void Search_by_empty_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.searchInputTB.Text = "";
            hospitalMap.searchInputComboBox.Text = "Pick type of object";

            hospitalMap.checkMapObjectSearchInput();

            Assert.Empty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_empty_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.searchInputTB.Text = "";
            hospitalMap.searchInputComboBox.Text = "Informations";

            hospitalMap.checkMapObjectSearchInput();

            Assert.NotEmpty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.searchInputTB.Text = "Info";
            hospitalMap.searchInputComboBox.Text = "Pick type of object";

            hospitalMap.checkMapObjectSearchInput();

            Assert.NotEmpty(HospitalMap.searchResult);
        }

        [StaFact]
        public void Search_by_filled_text_box_and_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.searchInputTB.Text = "Informations 1";
            hospitalMap.searchInputComboBox.Text = "Informations";

            hospitalMap.checkMapObjectSearchInput();

            Assert.NotEmpty(HospitalMap.searchResult);
        }
    }
}
