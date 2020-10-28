// File:    ScheduleAdjustmentRequestRepository.cs
// Author:  Lana
// Created: 02 June 2020 01:30:22
// Purpose: Definition of Interface ScheduleAdjustmentRequestRepository

using Model.Requests;
using Repository.Generics;

namespace Repository.RequestRepository
{
    public interface ScheduleAdjustmentRequestRepository : Repository<ScheduleAdjustmentRequest, int>
    {
    }
}