using System.ComponentModel.DataAnnotations.Schema;
using Model.Users.UserAccounts;

namespace Model.Users.Employee
{
    public class AdministrationWorker : Employee
    {
        [Column(TypeName = "nvarchar(24)")]
        public AdministrationWorkerType Type { get; set; }
    }
}