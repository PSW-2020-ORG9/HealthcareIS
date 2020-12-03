using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HealthcareBase.Model.Schedule.Procedures;
using HealthcareBase.Model.Schedule.Procedures.DTOs;

namespace HospitalWebApp.Mappers
{
    public class ExaminationMapper
    {
        public static Examination DtoToObject(ScheduledExaminationDTO scheduledExaminationDto)
        {
            return new Examination
            {
                DoctorId = scheduledExaminationDto.DoctorId,
                TimeInterval = scheduledExaminationDto.TimeInterval,
                PatientId = scheduledExaminationDto.PatientId,
                RoomId =  scheduledExaminationDto.RoomId,
                IsCanceled = false,
                Priority = ProcedurePriority.Low
            };
        }
    }
}
