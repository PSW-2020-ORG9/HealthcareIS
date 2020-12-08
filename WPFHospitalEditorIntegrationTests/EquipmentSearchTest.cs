using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor;

namespace WPFHospitalEditorIntegrationTests
{
    public class EquipmentSearchTest
    {
        [StaFact]
        public void Search_by_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.equipmentSearchComboBox.Text = "Pick equipment type";

            hospitalMap.checkEquipmentSearchInput();

            Assert.Empty(HospitalMap.equipmentSearchResult);
        }

        [StaFact]
        public void Search_by_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.equipmentSearchComboBox.Text = "Computer";

            hospitalMap.checkEquipmentSearchInput();

            Assert.NotEmpty(HospitalMap.equipmentSearchResult);
        }
    }
}
