using System;
using System.Collections.Generic;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class ExaminationServerController : IExaminationServerController
    {
        private readonly IExaminationServerService examinationServerService = new ExaminationServerService();

        public string Cancel(int examinationId)
        {
            return examinationServerService.Cancel(examinationId);
        }

        public IEnumerable<Examination> GetBySpecialtyId(int specialtyId)
        {
            return examinationServerService.GetBySpecialtyId(specialtyId);
        }

        public Examination ScheduleEmergencyExamination(DateTime startTime, int doctorId, int patientId)
        {
            return examinationServerService.ScheduleEmergencyExamination(startTime, doctorId, patientId);
        }

        public Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId)
        {
           return examinationServerService.ScheduleExamination(startTime, doctorId, patientId);
        }

        public IEnumerable<Examination> getByRoomId(int roomId)
        {
            return examinationServerService.GetByRoomId(roomId);
        }
    }
}
