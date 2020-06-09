using System;
using System.Linq;

namespace TryEntityFrameworkCore
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                db.Add(new Blog { Url = "https://docs.microsoft.com/" });
                db.SaveChanges();

                var blog = db.Blogs.OrderBy(b => b.BlogId).First();
                Console.WriteLine(blog.Url);

                blog.Posts.Add(
                    new Post
                    {
                        Title = "Hello world",
                        Content = "I wrote an app using EF Core"
                    });
                db.SaveChanges();

                var post = db.Posts.First();
                Console.WriteLine(post.Title);
            }
        }
    }
}
