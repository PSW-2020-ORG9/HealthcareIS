// File:    BlogAuthorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface BlogAuthorRepository

using HealthcareBase.Model.Blog;
using HealthcareBase.Model.Users.Employee;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.BlogRepository
{
    public interface BlogAuthorRepository : IWrappableRepository<BlogAuthor, int>
    {
        BlogAuthor GetByDoctor(Doctor doctor);
    }
}