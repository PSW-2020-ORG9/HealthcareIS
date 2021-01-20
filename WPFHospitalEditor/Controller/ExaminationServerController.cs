using System;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class ExaminationServerController : IExaminationServerController
    {
        private readonly IExaminationServerService examinationServerService = new ExaminationServerService();

        public Examination ScheduleEmergencyExamination(DateTime startTime, int doctorId, int patientId)
        {
            return examinationServerService.ScheduleEmergencyExamination(startTime, doctorId, patientId);
        }

        public Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId)
        {
           return examinationServerService.ScheduleExamination(startTime, doctorId, patientId);
        }
    }
}
