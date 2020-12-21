using HealthcareBase.Model.Schedule.Procedures;
using System;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IExaminationServerService
    {
         Examination ScheduleExamination(DateTime startTime, int DoctorId, int patientId);
    }
}
