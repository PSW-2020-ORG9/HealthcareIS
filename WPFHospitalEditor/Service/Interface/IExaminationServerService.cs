using System;
using System.Collections.Generic;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IExaminationServerService
    {
         Examination ScheduleExamination(DateTime startTime, int doctorId, int patientId);
        Examination ScheduleEmergencyExamination(DateTime startTime, int doctorId, int patientId);
        IEnumerable<Examination> GetBySpecialtyId(int specialtyId);
        string Cancel(int examinationId);
    }
}
