// File:    BlogAuthorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class BlogAuthorService

using System.Collections.Generic;
using HealthcareBase.Model.Blog;
using HealthcareBase.Model.CustomExceptions;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.BlogRepository;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Service.BlogService
{
    public class BlogAuthorService
    {
        private readonly RepositoryWrapper<BlogAuthorRepository> blogAuthorRepository;

        public BlogAuthorService(BlogAuthorRepository blogAuthorRepository)
        {
            this.blogAuthorRepository = new RepositoryWrapper<BlogAuthorRepository>(blogAuthorRepository);
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