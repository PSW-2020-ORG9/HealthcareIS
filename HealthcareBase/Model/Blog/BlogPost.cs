// File:    BlogPost.cs
// Author:  Gudli
// Created: 20 April 2020 11:54:01
// Purpose: Definition of Class BlogPost

using System;
using System.ComponentModel.DataAnnotations;
using Repository.Generics;

namespace Model.Blog
{
    public class BlogPost : Entity<int>
    {
        public BlogPost(string title, string text, DateTime timeStamp, BlogAuthor author)
        {
            Title = title;
            Text = text;
            TimeStamp = timeStamp;
            Author = author;
        }

        public BlogPost()
        {
            Author = new BlogAuthor();
        }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime TimeStamp { get; set; }

        public BlogAuthor Author { get; set; }

        [Key]
        public int Id { get; set; }

        public int GetKey()
        {
            return Id;
        }

        public void SetKey(int id)
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            return obj is BlogPost post &&
                   Id == post.Id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + Id.GetHashCode();
        }
    }
}