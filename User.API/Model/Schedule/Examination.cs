// File:    Examination.cs
// Author:  Lana
// Created: 20 April 2020 23:40:27
// Purpose: Definition of Class Examination

using System;
using System.ComponentModel.DataAnnotations.Schema;


namespace User.API.Model.Schedule
{
    public class Examination : Procedure
    {
        [ForeignKey("ExaminationReport")]
        public int? ExaminationReportId { get; set; }
        public static TimeSpan TimeFrameSize = new TimeSpan(0,30,0);
        public static TimeSpan TimeConstraint = new TimeSpan(24,0,0);
        public ExaminationReport ExaminationReport { get; set; }
        public bool IsCanceled { get; set; }
    }
}