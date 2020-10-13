using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace efcore
{
    public class BloggingContext : DbContext
    {
        
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<Comment> Comment { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite(@"Data Source=G:\2020\Upc2020\Microservices\blogging.db");
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }

        public List<Post> Posts { get; } = new List<Post>();
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }

           public List<Comment> Comments { get; } = new List<Comment>();
    }
    public class Comment
    {
        public int CommentId { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
        public string Description { get; set; }

    }

  
}