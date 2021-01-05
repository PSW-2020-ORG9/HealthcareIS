using System;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IExaminationServerController
    {
        Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId);
    }
}
