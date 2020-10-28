// File:    BlogAuthorRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface BlogAuthorRepository

using Model.Blog;
using Model.Users.Employee;
using Repository.Generics;
using System;

namespace Repository.BlogRepository
{
    public interface BlogAuthorRepository : Repository<BlogAuthor, int>
    {
        BlogAuthor GetByDoctor(Doctor doctor);
    }
}