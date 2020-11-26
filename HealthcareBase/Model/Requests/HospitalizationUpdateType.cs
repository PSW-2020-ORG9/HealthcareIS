// File:    HospitalizationUpdateType.cs
// Author:  Lana
// Created: 27 May 2020 20:52:29
// Purpose: Definition of Enum HospitalizationUpdateType

namespace HealthcareBase.Model.Requests
{
    public enum HospitalizationUpdateType
    {
        Scheduled,
        Cancelled,
        Rescheduled
    }
}