// File:    BlogAuthorFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class BlogAuthorFileRepository

using System.Collections.Generic;
using HealthcareBase.Model.Blog;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Utilities;
using HealthcareBase.Repository.Generics;
using HealthcareBase.Repository.UsersRepository.EmployeesAndPatientsRepository;

namespace HealthcareBase.Repository.BlogRepository
{
    public class BlogAuthorFileRepository : GenericFileRepository<BlogAuthor, int>, BlogAuthorRepository
    {
        private readonly DoctorRepository doctorRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public BlogAuthorFileRepository(DoctorRepository doctorRepository, string fileName) : base(fileName)
        {
            this.doctorRepository = doctorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public BlogAuthor GetByDoctor(Doctor doctor)
        {
            var authors = (List<BlogAuthor>) GetMatching(author => author.Doctor.Equals(doctor));

            if (authors.Count == 0)
                throw new BadReferenceException();

            return authors[0];
        }

        protected override BlogAuthor ParseEntity(BlogAuthor entity)
        {
            try
            {
                if (entity.Doctor != null)
                    entity.Doctor = doctorRepository.GetByID(entity.Doctor.GetKey());
            }
            catch (BadRequestException)
            {
                throw new ValidationException();
            }

            return entity;
        }

        protected override int GenerateKey(BlogAuthor entity)
        {
            return keyGenerator.GenerateKey();
        }
    }
}