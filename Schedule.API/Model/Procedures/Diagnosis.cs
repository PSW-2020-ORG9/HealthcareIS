using System.ComponentModel.DataAnnotations.Schema;
using Schedule.API.Infrastructure;
using Schedule.API.Infrastructure.Database;

namespace Schedule.API.Model.Procedures
{
    public class Diagnosis : Entity<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsInjury { get; set; }
    }
}