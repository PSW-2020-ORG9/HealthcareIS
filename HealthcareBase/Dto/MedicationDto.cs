using HealthcareBase.Model.Medication;
using System;

namespace HealthcareBase.Dto
{
   public class MedicationDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public MedicineType Type { get; set; }
        public int Quantity { get; set; }
    }
}
