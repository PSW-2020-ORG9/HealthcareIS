// File:    BlogAuthorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class BlogAuthorService

using System.Collections.Generic;
using HealthcareBase.Model.Blog;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.BlogRepository;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Service.BlogService
{
    public class BlogAuthorService
    {
        private readonly RepositoryWrapper<IBlogAuthorRepository> blogAuthorRepository;

        public BlogAuthorService(IBlogAuthorRepository blogAuthorRepository)
        {
            this.blogAuthorRepository = new RepositoryWrapper<IBlogAuthorRepository>(blogAuthorRepository);
        }

        public BlogAuthor GetByID(int id)
        {
            return blogAuthorRepository.Repository.GetByID(id);
        }

        public BlogAuthor GetByDoctor(Doctor doctor)
        {
            return blogAuthorRepository.Repository.GetByDoctor(doctor);
        }

        public IEnumerable<BlogAuthor> GetAll()
        {
            return blogAuthorRepository.Repository.GetAll();
        }

        public BlogAuthor UpdateDescription(BlogAuthor blogAuthor)
        {
            var oldAuthor = blogAuthorRepository.Repository.GetByID(blogAuthor.Id);
            if (oldAuthor == null)
                throw new BadRequestException();
            if (!oldAuthor.Doctor.Equals(blogAuthor))
                throw new BadRequestException();

            return blogAuthorRepository.Repository.Update(blogAuthor);
        }
    }
}