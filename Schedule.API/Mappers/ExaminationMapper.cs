using Schedule.API.Model.Procedures;
using Schedule.API.Model.Procedures.DTOs;
using Schedule.API.Model.Utilities;

namespace Schedule.API.Mappers
{
    public static class ExaminationMapper
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
