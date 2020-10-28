// File:    BlogPost.cs
// Author:  Gudli
// Created: 20 April 2020 11:54:01
// Purpose: Definition of Class BlogPost

using System;

namespace Model.Blog
{
    public class BlogPost : Repository.Generics.Entity<int>
    {
        private String title;
        private String text;
        private DateTime timeStamp;
        private BlogAuthor author;
        private int id;

        public BlogPost(string title, string text, DateTime timeStamp, BlogAuthor author)
        {
            this.title = title;
            this.text = text;
            this.timeStamp = timeStamp;
            this.author = author;
        }

        public BlogPost()
        {
            author = new BlogAuthor();
        }

        public string Title { get => title; set => title = value; }
        public string Text { get => text; set => text = value; }
        public DateTime TimeStamp { get => timeStamp; set => timeStamp = value; }
        public BlogAuthor Author { get => author; set => author = value; }

        public int Id { get => id; set => id = value; }

        public override bool Equals(object obj)
        {
            return obj is BlogPost post &&
                   id == post.id;
        }

        public override int GetHashCode()
        {
            return 1877310944 + id.GetHashCode();
        }

        public int GetKey()
        {
            return id;
        }

        public void SetKey(int id)
        {
            this.id = id;
        }
    }
}