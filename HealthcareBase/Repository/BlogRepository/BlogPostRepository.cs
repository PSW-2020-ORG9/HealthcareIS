// File:    BlogPostRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Interface BlogPostRepository

using Model.Blog;
using Repository.Generics;
using System;
using System.Collections.Generic;

namespace Repository.BlogRepository
{
    public interface BlogPostRepository : Repository<BlogPost, int>
    {
        IEnumerable<BlogPost> GetByAuthor(BlogAuthor author);

    }
}