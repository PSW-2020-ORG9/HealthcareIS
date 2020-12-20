using System;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IExaminationServerController
    {
        public string ScheduleExamination(DateTime startTime, int DoctorId, int patientId);
    }
}
