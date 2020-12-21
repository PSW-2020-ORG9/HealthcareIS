using HealthcareBase.Model.Schedule.Procedures;
using System;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IExaminationServerController
    {
        Examination ScheduleExamination(DateTime startTime, int DoctorId, int patientId);
    }
}
