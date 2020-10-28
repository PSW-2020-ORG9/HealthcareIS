// File:    ProcedureNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:30
// Purpose: Definition of Class ProcedureNotification

using Model.Schedule.Procedures;

namespace Model.Notifications
{
    public class ProcedureNotification : Notification
    {
        public Procedure Procedure { get; set; }

        public ProcedureUpdateType UpdateType { get; set; }
    }
}