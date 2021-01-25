using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class RenovationServerController : IRenovationServerController
    {
        private readonly IRenovationServerService renovationServerService = new RenovationServerService();

        public string ScheduleRenovation(TimeInterval timeInterval, int doctorId, int patientId)
        {
            return renovationServerService.ScheduleRenovation(timeInterval, doctorId, patientId);
        }
    }
}
