// File:    Examination.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Examination

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Medication;
using Model.Miscellaneous;
using Model.Users.Patient.MedicalHistory;

namespace Model.Schedule.Procedures
{
    public class Examination : Procedure
    {
        [ForeignKey("ExaminationReport")]
        public int ExaminationReportId { get; set; }
        public ExaminationReport ExaminationReport { get; set; }
        
        public int MedicalRecordId { get; set; }
    }
}