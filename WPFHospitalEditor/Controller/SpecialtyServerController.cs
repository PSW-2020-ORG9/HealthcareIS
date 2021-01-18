using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Controller.Interface;
using WPFHospitalEditor.Model;
using WPFHospitalEditor.Service;
using WPFHospitalEditor.Service.Interface;

namespace WPFHospitalEditor.Controller
{
    public class SpecialtyServerController : ISpecialtyServerController
    {
        private readonly ISpecialtyServerService specialtyServerService = new SpecialtyServerService();

        public IEnumerable<Specialty> GetAllSpecialties()
        {
           return specialtyServerService.GetAllSpecialties();
        }
    }
}
