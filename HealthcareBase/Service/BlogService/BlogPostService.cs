// File:    BlogPostService.cs
// Author:  Gudli
// Created: 27 May 2020 19:02:37
// Purpose: Definition of Class BlogPostService

using System;
using System.Collections.Generic;
using Model.Blog;
using Model.CustomExceptions;
using Repository.BlogRepository;
using Repository.Generics;

namespace Service.BlogService
{
    public class BlogPostService
    {
        private readonly RepositoryWrapper<BlogAuthorRepository> blogAuthorRepository;
        private readonly RepositoryWrapper<BlogPostRepository> blogPostRepository;

        public BlogPostService(RepositoryWrapper<BlogPostRepository> blogPostRepository, RepositoryWrapper<BlogAuthorRepository> blogAuthorRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.blogAuthorRepository = blogAuthorRepository;
        }

        public BlogPost GetByID(int id)
        {
            return blogPostRepository.Repository.GetByID(id);
        }

        public IEnumerable<BlogPost> GetAll()
        {
            return blogPostRepository.Repository.GetAll();
        }

        public IEnumerable<BlogPost> GetByAuthor(BlogAuthor author)
        {
            return blogPostRepository.Repository.GetByAuthor(author);
        }

        public BlogPost Create(BlogPost blogPost)
        {
            if (blogAuthorRepository.Repository.GetByID(blogPost.Author.Id) == null)
                throw new BadRequestException();

            blogPost.TimeStamp = DateTime.Now;
            return blogPostRepository.Repository.Create(blogPost);
        }

        public BlogPost Update(BlogPost blogPost)
        {
            var oldPost = blogPostRepository.Repository.GetByID(blogPost.Id);
            if (oldPost == null)
                throw new BadRequestException();
            if (blogPost.TimeStamp != oldPost.TimeStamp)
                throw new BadRequestException();
            if (!oldPost.Author.Equals(blogPost.Author))
                throw new BadRequestException();

            return blogPostRepository.Repository.Update(blogPost);
        }

        public void Delete(BlogPost blogPost)
        {
            blogPostRepository.Repository.Delete(blogPost);
        }
    }
}