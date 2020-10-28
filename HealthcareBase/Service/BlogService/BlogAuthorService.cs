// File:    BlogAuthorService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class BlogAuthorService

using Model.Blog;
using Model.CustomExceptions;
using Model.Users.Employee;
using Repository.BlogRepository;
using System;
using System.Collections.Generic;

namespace Service.BlogService
{
    public class BlogAuthorService
    {
        static BlogAuthorService()
        {
            Console.WriteLine("Hello I am working");
        }

        private BlogAuthorRepository blogAuthorRepository;

        public BlogAuthorService(BlogAuthorRepository blogAuthorRepository)
        {
            this.blogAuthorRepository = blogAuthorRepository;
        }

        public BlogAuthor GetByID(int id)
        {
            return blogAuthorRepository.GetByID(id);
        }

        public BlogAuthor GetByDoctor(Doctor doctor)
        {
            return blogAuthorRepository.GetByDoctor(doctor);
        }

        public IEnumerable<BlogAuthor> GetAll()
        {
            return blogAuthorRepository.GetAll();
        }

        public BlogAuthor UpdateDescription(BlogAuthor blogAuthor)
        {
            var oldAuthor = blogAuthorRepository.GetByID(blogAuthor.Id);
            if (oldAuthor == null)
                throw new BadRequestException();
            if (!oldAuthor.Doctor.Equals(blogAuthor))
                throw new BadRequestException();

            return blogAuthorRepository.Update(blogAuthor);
        }

    }
}