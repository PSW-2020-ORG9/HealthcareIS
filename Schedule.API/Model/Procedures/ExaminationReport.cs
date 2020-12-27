using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;
using Schedule.API.Model.Dependencies;

namespace Schedule.API.Model.Procedures
{
    public class ExaminationReport : Entity<int>
    {
        public string Anamnesis { get; set; }

        public IEnumerable<Diagnosis> Diagnoses { get; set; }
        public IEnumerable<MedicationPrescription> Prescriptions { get; set; }
        // TODO Include prescriptions via Connections.GET() call
    }
}