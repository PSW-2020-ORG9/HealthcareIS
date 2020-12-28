using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor;
using WPFHospitalEditor.Controller;
using Xunit;

namespace WPFHospitalEditorE2ETests
{
    public class MedicineSearchTests
    {
        [StaFact]
        public void Search_by_empty_combo_box()
        {
            IMedicationServerController medicationServerController = new MedicationServerController();

            var medication = medicationServerController.GetAllMedicationByName(AllConstants.EmptyComboBox);

            Assert.Empty(medication);
        }

        [StaFact]
        public void Search_by_filled_combo_box()
        {
            IMedicationServerController medicationServerController = new MedicationServerController();

            var medication = medicationServerController.GetAllMedicationByName("Bromazepam");

            Assert.NotEmpty(medication);
        }
    }
}
