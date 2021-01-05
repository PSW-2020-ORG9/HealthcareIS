using System;
using System.Collections.Generic;
using System.Text;

namespace WPFHospitalEditor.DTOs
{
    public class DoctorDto
    {
        public string Name { get; set; }
        public string DoctorId { get; internal set; }
        public string Surname { get; internal set; }
        public string DepartmentName { get; internal set; }
    }
}
