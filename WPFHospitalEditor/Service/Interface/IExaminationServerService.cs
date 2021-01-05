using System;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IExaminationServerService
    {
         Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId);
    }
}
