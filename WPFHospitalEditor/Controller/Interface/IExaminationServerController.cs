using System;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IExaminationServerController
    {
        Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId);
    }
}
