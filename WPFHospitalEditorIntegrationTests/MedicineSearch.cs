using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor;

namespace WPFHospitalEditorIntegrationTests
{
    public class MedicineSearch
    {
        [StaFact]
        public void Search_by_empty_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.medicationSearchComboBox.Text = AllConstants.emptyComboBox;

            hospitalMap.checkMedicineSearchInput();

            Assert.Empty(HospitalMap.medicationSearchResult);
        }

        [StaFact]
        public void Search_by_filled_combo_box()
        {
            MapObjectController mapObjectController = new MapObjectController();
            HospitalMap hospitalMap = new HospitalMap(mapObjectController.getAllMapObjects(), Role.Director);
            hospitalMap.medicationSearchComboBox.Text = "Bromazepam";

            hospitalMap.checkMedicineSearchInput();

            Assert.NotEmpty(HospitalMap.medicationSearchResult);
        }
    }
}
