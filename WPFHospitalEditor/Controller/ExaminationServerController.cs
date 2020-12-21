using HealthcareBase.Model.Schedule.Procedures;
using System;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class ExaminationServerController : IExaminationServerController
    {
        IExaminationServerService examinationServerService = new ExaminationServerService();
        public Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId)
        {
           return examinationServerService.ScheduleExamination(startTime, doctorId, patientId);
        }
    }
}
