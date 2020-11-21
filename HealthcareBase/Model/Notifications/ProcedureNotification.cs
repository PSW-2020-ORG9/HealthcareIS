// File:    ProcedureNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:30
// Purpose: Definition of Class ProcedureNotification

using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Schedule.Procedures;

namespace HealthcareBase.Model.Notifications
{
    public class ProcedureNotification : Notification
    {
        [ForeignKey("Procedure")]
        public int ProcedureId { get; set; }
        public Procedure Procedure { get; set; }

        [Column(TypeName = "nvarchar(24)")]
        public ProcedureUpdateType UpdateType { get; set; }
    }
}