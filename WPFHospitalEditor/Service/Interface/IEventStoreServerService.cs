﻿using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IEventStoreServerService
    {
        string RecordFloorChange(FloorChangeDto floorChangeDto);
        string RecordEquipmentLookup(EquipmentLookupDto equipmentLookupDto);
        string RecordMedicationLookup(MedicationLookupDto medicationLookupDto);
    }
}
