using System;
using System.Linq;

namespace efcore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Create
                Console.WriteLine("Inserting a new blog");
                db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
                db.SaveChanges();

                // Read
                Console.WriteLine("Querying for a blog");
                var blog = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .First();

                // Update
                Console.WriteLine("Updating the blog and adding a post");
                blog.Url = "https://devblogs.microsoft.com/dotnet";

                var post=                    new Post
                    {
                        Title = "Hello World",
                        Content = "I wrote an app using EF Core!"
                    };
                post.Comments.Add(new Comment{Description="Hola!!!!!!*******" });
                post.Comments.Add(new Comment{Description="Hola!!!!!!*******" });
                blog.Posts.Add(post);
                db.SaveChanges();


                var posts=db.Posts.ToList();
                foreach(var p in posts)
                {
                   Console.WriteLine($"{p.Title} ");     
                } 

                // Delete
                //Console.WriteLine("Delete the blog");
                //db.Remove(blog);
                //db.SaveChanges();

                var comments=db.Comment.ToList();
                foreach(var c in comments)
                {
                   Console.WriteLine($"{c.Description} ");     
                } 
            }
        }
    }


}
