// File:    ProcedureUpdateType.cs
// Author:  Lana
// Created: 27 May 2020 20:49:55
// Purpose: Definition of Enum ProcedureUpdateType

namespace HealthcareBase.Model.Requests
{
    public enum ProcedureUpdateType
    {
        Scheduled,
        Rescheduled,
        Cancelled
    }
}