using System;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IExaminationServerService
    {
        public string ScheduleExamination(DateTime startTime, int DoctorId, int patientId);
    }
}
