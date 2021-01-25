using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IRenovationServerController
    {
        string ScheduleRenovation(TimeInterval timeInterval, int doctorId, int patientId);
    }
}
