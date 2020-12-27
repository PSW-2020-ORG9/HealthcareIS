using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Schedule.API.Model.Procedures
{
    public class Examination : Procedure
    {
        [ForeignKey("ExaminationReport")]
        public int? ExaminationReportId { get; set; }
        public ExaminationReport ExaminationReport { get; set; }
        
        public static TimeSpan TimeFrameSize = new TimeSpan(0,30,0);
        public static TimeSpan TimeConstraint = new TimeSpan(24,0,0);
        
        public bool IsCanceled { get; set; }
    }
}