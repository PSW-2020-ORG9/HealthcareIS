// File:    ProcedureNotification.cs
// Author:  Lana
// Created: 27 May 2020 20:47:30
// Purpose: Definition of Class ProcedureNotification

using Model.Schedule.Procedures;
using System;

namespace Model.Notifications
{
    public class ProcedureNotification : Notification
    {
        private Procedure procedure;
        private ProcedureUpdateType updateType;

        public Procedure Procedure { get => procedure; set => procedure = value; }
        public ProcedureUpdateType UpdateType { get => updateType; set => updateType = value; }
    }
}