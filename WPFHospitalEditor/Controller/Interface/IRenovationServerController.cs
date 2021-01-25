using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.Controller.Interface
{
    public interface IRenovationServerController
    {
        string ScheduleRenovation(DateTime startDate, DateTime endDate, int doctorId, int patientId);
    }
}
