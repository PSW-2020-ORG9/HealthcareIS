using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Service.Interface
{
    public interface IRenovationServerService
    {
        string ScheduleRenovation(TimeInterval timeInterval, int doctorId, int patientId);
    }
}
