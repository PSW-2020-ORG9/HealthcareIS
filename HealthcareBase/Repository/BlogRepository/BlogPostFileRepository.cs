// File:    BlogPostFileRepository.cs
// Author:  Gudli
// Created: 21 May 2020 20:31:56
// Purpose: Definition of Class BlogPostFileRepository

using System.Collections.Generic;
using Model.Blog;
using Model.CustomExceptions;
using Model.Utilities;
using Repository.Generics;

namespace Repository.BlogRepository
{
    public class BlogPostFileRepository : GenericFileRepository<BlogPost, int>, BlogPostRepository
    {
        private readonly BlogAuthorRepository blogAuthorRepository;
        private readonly IntegerKeyGenerator keyGenerator;

        public BlogPostFileRepository(BlogAuthorRepository blogAuthorRepository, string filePath) : base(filePath)
        {
            this.blogAuthorRepository = blogAuthorRepository;
            keyGenerator = new IntegerKeyGenerator(GetAllKeys());
        }

        public IEnumerable<BlogPost> GetByAuthor(BlogAuthor author)
        {
            return GetMatching(blogPost => blogPost.Author.Equals(author));
        }

        protected override int GenerateKey(BlogPost entity)
        {
            return keyGenerator.GenerateKey();
        }

        protected override BlogPost ParseEntity(BlogPost entity)
        {
            try
            {
                if (entity.Author != null)
                    entity.Author = blogAuthorRepository.GetByID(entity.Author.GetKey());
            }
            catch (BadRequestException)
            {
                throw new BadReferenceException();
            }

            return entity;
        }
    }
}