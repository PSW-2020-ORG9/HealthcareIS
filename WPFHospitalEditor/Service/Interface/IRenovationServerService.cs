using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IRenovationServerService
    {
        string ScheduleRenovation(DateTime startDate, DateTime endDate, int doctorId, int patientId);
    }
}
