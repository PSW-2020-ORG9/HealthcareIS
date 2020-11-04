// File:    BlogPostRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface BlogPostRepository

using System.Collections.Generic;
using Model.Blog;
using Repository.Generics;

namespace Repository.BlogRepository
{
    public interface BlogPostRepository : IWrappableRepository<BlogPost, int>
    {
        IEnumerable<BlogPost> GetByAuthor(BlogAuthor author);
    }
}