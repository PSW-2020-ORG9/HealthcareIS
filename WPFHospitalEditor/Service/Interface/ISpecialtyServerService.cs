using System;
using System.Collections.Generic;
using System.Text;
using WPFHospitalEditor.Model;

namespace WPFHospitalEditor.Service.Interface
{
   public interface ISpecialtyServerService
    {
        IEnumerable<Specialty> GetAllSpecialties();
    }
}
