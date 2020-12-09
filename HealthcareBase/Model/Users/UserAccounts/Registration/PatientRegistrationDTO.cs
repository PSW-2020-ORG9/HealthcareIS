using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using HealthcareBase.Model.Users.Generalities;
using HealthcareBase.Model.Users.Patient;
using HealthcareBase.Model.Users.Patient.MedicalHistory.Relationship;

namespace HealthcareBase.Model.Users.UserAccounts.Registration
{
    public class PatientRegistrationDTO
    {
        public string Jmbg { get; set; }
        public string InsuranceNumber { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string TelephoneNumber { get; set; } //
        public IEnumerable<Citizenship> Citizenships { get; set; } //
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public MaritalStatus MaritalStatus { get; set; }
        public Gender Gender { get; set; }
        public int CityOfResidenceId { get; set; }
        public int CityOfBirthId { get; set; }
        
        public IEnumerable<AllergyManifestation> Allergies { get; set; } //
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; } //

    }
}