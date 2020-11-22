using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Miscellaneous;
using Repository.Generics;

namespace Model.Users.Patient.MedicalHistory
{
    public class PersonalHistoryDiagnosis : Entity<int>
    {
        [Key]
        public int Id { get; set; }
        public int DiscoveredAtAge { get; set; }
        public string Description { get; set; }
        
        [Column(TypeName = "nvarchar(12)")]
        public ConditionType Type { get; set; }

        [ForeignKey("Diagnosis")]
        public string DiagnosisId { get; set; }
        public Diagnosis Diagnosis { get; set; }

        public int GetKey() => Id;
        public void SetKey(int id) => Id = id;
    }
}