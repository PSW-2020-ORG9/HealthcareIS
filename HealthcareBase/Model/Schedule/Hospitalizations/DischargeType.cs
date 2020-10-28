// File:    DischargeType.cs
// Author:  Lana
// Created: 20 April 2020 23:27:02
// Purpose: Definition of Enum DischargeType

namespace Model.Schedule.Hospitalizations
{
    public enum DischargeType
    {
        ToOtherFacility,
        ToResidence,
        Death,
        OnPersonalRequest,
        Statistical
    }
}