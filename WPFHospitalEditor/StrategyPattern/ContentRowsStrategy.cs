using System.Collections.Generic;
using System.Linq;
using WPFHospitalEditor.Controller;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.DTOs;

namespace WPFHospitalEditor.StrategyPattern
{
    interface IContentRowsStrategy
    {
        AdditionalInformationDTO GetContentRows();
    }

    class ContentRowsStrategy : IContentRowsStrategy
    {
        private readonly IContentRowsStrategy strategy;

        public ContentRowsStrategy(IContentRowsStrategy strategy)
        {
            this.strategy = strategy;
        }

        public AdditionalInformationDTO GetContentRows()
        {
            return strategy.GetContentRows();
        }
    }
    class MedicationContentRows : IContentRowsStrategy
    {
        private readonly int id;
        public MedicationContentRows(int id)
        {
            this.id = id;
        }
        public AdditionalInformationDTO GetContentRows()
        {
            IMedicationServerController medicationServerController = new MedicationServerController();
            IEnumerable<MedicationDto> allMedications = medicationServerController.GetAllMedication();

            string[] medicationContentRows = new string[allMedications.Count()];
            for (int i = 0; i < allMedications.Count(); i++)
            {
                medicationContentRows[i] = allMedications.ElementAt(i).Name + AllConstants.ContentSeparator + allMedications.ElementAt(i).Quantity;
            }
            return new AdditionalInformationDTO()
            {
                MapObjectId = id,
                ContentRows = medicationContentRows
            };
        }
    }

    class EquipmentContentRows : IContentRowsStrategy
    {
        private readonly int id;

        public EquipmentContentRows(int id)
        {
            this.id = id;
        }
        public AdditionalInformationDTO GetContentRows()
        {
            IEquipmentServerController equipmentServerController = new EquipmentServerController();
            IEnumerable<EquipmentDto> allEquipment = equipmentServerController.GetEquipmentByRoomId(id);


            string[] equipmentContentRows = new string[allEquipment.Count()];
            for (int i = 0; i < allEquipment.Count(); i++)
            {
                equipmentContentRows[i] = allEquipment.ElementAt(i).Name + AllConstants.ContentSeparator + allEquipment.ElementAt(i).Quantity;
            }
            return new AdditionalInformationDTO()
            {
                MapObjectId = id,
                ContentRows = equipmentContentRows
            };
        }
    }
}
