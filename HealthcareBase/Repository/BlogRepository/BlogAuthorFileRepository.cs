// File:    BlogAuthorFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class BlogAuthorFileRepository

using Model.Blog;
using Model.CustomExceptions;
using Model.Users.Employee;
using Model.Utilities;
using Repository.Generics;
using Repository.UsersRepository.EmployeesAndPatientsRepository;
using System;
using System.Collections.Generic;

namespace Repository.BlogRepository
{
    public class BlogAuthorFileRepository : GenericFileRepository<BlogAuthor, int>, BlogAuthorRepository
    {
        private IntegerKeyGenerator keyGenerator;
        private DoctorRepository doctorRepository;

        public BlogAuthorFileRepository(DoctorRepository doctorRepository, String fileName) : base(fileName)
        {
            this.doctorRepository = doctorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
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
        
        public BlogAuthor GetByDoctor(Doctor doctor)
        {
            var authors = (List<BlogAuthor>)GetMatching(author => author.Doctor.Equals(doctor));

            if (authors.Count == 0)
                throw new BadReferenceException();

            return authors[0];
        }
    }
}