// File:    ScheduleAdjustmentRequestRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:30:22
// Purpose: Definition of Interface ScheduleAdjustmentRequestRepository

using HealthcareBase.Model.Requests;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.RequestRepository
{
    public interface IScheduleAdjustmentRequestRepository : IWrappableRepository<ScheduleAdjustmentRequest, int>
    {
    }
}