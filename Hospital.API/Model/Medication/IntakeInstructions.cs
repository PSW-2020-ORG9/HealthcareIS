using General;
using System;

namespace Hospital.API.Model.Medication
{
    public class IntakeInstructions : Entity<int>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TimesPerDay { get; set; }
        public double Dosage { get; set; }
        public string DosageUnit { get; set; }
        public string Description { get; set; }
    }
}