using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class EventStoreServerController : IEventStoreServerController
    {
        private readonly IEventStoreServerService eventStoreServerService= new EventStoreServerService();

        public string RecordEquipmentLookup(EquipmentLookupDto equipmentLookupDto)
        {
            return eventStoreServerService.RecordEquipmentLookup(equipmentLookupDto);
        }

        public string RecordFloorChange(FloorChangeDto floorChangeDto)
        {
            return eventStoreServerService.RecordFloorChange(floorChangeDto);
        }

        public string RecordMedicationLookup(MedicationLookupDto medicationLookupDto)
        {
            return eventStoreServerService.RecordMedicationLookup(medicationLookupDto);
        }
    }
}
