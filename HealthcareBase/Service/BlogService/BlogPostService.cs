// File:    BlogPostService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class BlogPostService

using System;
using System.Collections.Generic;
using Model.Blog;
using Model.CustomExceptions;
using Repository.BlogRepository;

namespace Service.BlogService
{
    public class BlogPostService
    {
        private readonly BlogAuthorRepository blogAuthorRepository;
        private readonly BlogPostRepository blogPostRepository;

        public BlogPostService(BlogPostRepository blogPostRepository, BlogAuthorRepository blogAuthorRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogAuthorRepository = blogAuthorRepository;
        }

        public BlogPost GetByID(int id)
        {
            return blogPostRepository.GetByID(id);
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return blogPostRepository.GetAll();
        }

        public IEnumerable<BlogPost> GetByAuthor(BlogAuthor author)
        {
            return blogPostRepository.GetByAuthor(author);
        }

        public BlogPost Create(BlogPost blogPost)
        {
            if (blogAuthorRepository.GetByID(blogPost.Author.Id) == null)
                throw new BadRequestException();

            blogPost.TimeStamp = DateTime.Now;
            return blogPostRepository.Create(blogPost);
        }

        public BlogPost Update(BlogPost blogPost)
        {
            var oldPost = blogPostRepository.GetByID(blogPost.Id);
            if (oldPost == null)
                throw new BadRequestException();
            if (blogPost.TimeStamp != oldPost.TimeStamp)
                throw new BadRequestException();
            if (!oldPost.Author.Equals(blogPost.Author))
                throw new BadRequestException();

            return blogPostRepository.Update(blogPost);
        }

        public void Delete(BlogPost blogPost)
        {
            blogPostRepository.Delete(blogPost);
        }
    }
}