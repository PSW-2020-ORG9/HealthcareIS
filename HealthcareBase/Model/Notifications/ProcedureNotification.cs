// File:    ProcedureNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:30
// Purpose: Definition of Class ProcedureNotification

using Model.Schedule.Procedures;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Notifications
{
    public class ProcedureNotification : Notification
    {
        // TODO Procedures cannot be used as Types, only Surgeries and Examinations 

        [Column(TypeName = "nvarchar(24)")]
        public ProcedureUpdateType UpdateType { get; set; }
    }
}