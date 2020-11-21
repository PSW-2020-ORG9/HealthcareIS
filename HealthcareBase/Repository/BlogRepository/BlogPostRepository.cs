// File:    BlogPostRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface BlogPostRepository

using System.Collections.Generic;
using HealthcareBase.Model.Blog;
using HealthcareBase.Repository.Generics;

namespace HealthcareBase.Repository.BlogRepository
{
    public interface BlogPostRepository : IWrappableRepository<BlogPost, int>
    {
        IEnumerable<BlogPost> GetByAuthor(BlogAuthor author);
    }
}