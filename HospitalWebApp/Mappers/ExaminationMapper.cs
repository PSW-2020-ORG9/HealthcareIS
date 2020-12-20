using System;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.Procedures.DTOs;
using HealthcareBase.Model.Utilities;

namespace HospitalWebApp.Mappers
{
    public class ExaminationMapper
    {
        public static Examination DtoToObject(ScheduledExaminationDTO dto)
        {
            return new Examination
            {
                DoctorId = dto.DoctorId,
                TimeInterval = new TimeInterval(dto.StartTime, dto.StartTime.Add(Examination.TimeFrameSize)),
                PatientId = dto.PatientId,
                IsCanceled = false,
                Priority = ProcedurePriority.Low
            };
        }
    }
}
