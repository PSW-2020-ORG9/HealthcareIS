using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IEventStoreServerController
    {
        string RecordFloorChange(FloorChangeDto floorChangeDto);
        string RecordEquipmentLookup(EquipmentLookupDto equipmentLookupDto);
        string RecordMedicationLookup(MedicationLookupDto medicationLookupDto);
    }
}
