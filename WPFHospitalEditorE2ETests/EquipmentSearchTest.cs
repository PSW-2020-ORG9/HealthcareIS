using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using Xunit;

namespace WPFHospitalEditorE2ETests
{
    public class EquipmentSearchTest
    {
        [StaFact]
        public void Search_by_empty_combo_box()
        {           
            IEquipmentServerController equipmentServerController = new EquipmentServerController();

            var equipment =  equipmentServerController.GetEquipmentByType(AllConstants.emptyComboBox);

            Assert.Empty(equipment);
        }

        [StaFact]
        public void Search_by_filled_combo_box()
        {
            IEquipmentServerController equipmentServerController = new EquipmentServerController();

            var equipment = equipmentServerController.GetEquipmentByType("computer");

            Assert.NotEmpty(equipment);
        }
    }
}
