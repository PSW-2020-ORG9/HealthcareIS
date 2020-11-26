using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.UserAccounts;

namespace HealthcareBase.Model.Users.Employee
{
    public class AdministrationWorker : Employee
    {
        [Column(TypeName = "nvarchar(24)")]
        public AdministrationWorkerType Type { get; set; }
    }
}