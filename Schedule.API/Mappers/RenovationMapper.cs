using Schedule.API.Model.Procedures;
using Schedule.API.Model.Procedures.DTOs;
using Schedule.API.Model.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Schedule.API.Mappers
{
    public class RenovationMapper
    {
        public static Examination DtoToObject(ScheduledRenovationDTO dto)
        {
            return new Examination
            {
                DoctorId = dto.DoctorId,
                TimeInterval = dto.TimeInterval,
                PatientId = dto.PatientId,
                IsCanceled = false,
                Priority = ProcedurePriority.Low,
            };
        }
    }
}
