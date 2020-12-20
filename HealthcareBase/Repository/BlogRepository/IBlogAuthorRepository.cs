// File:    BlogAuthorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface BlogAuthorRepository

using HealthcareBase.Model.Blog;
using HealthcareBase.Model.Users.Employee.Doctors;
using HealthcareBase.Repository.Generics.Interface;

namespace HealthcareBase.Repository.BlogRepository
{
    public interface IBlogAuthorRepository : IWrappableRepository<BlogAuthor, int>
    {
        BlogAuthor GetByDoctor(Doctor doctor);
    }
}