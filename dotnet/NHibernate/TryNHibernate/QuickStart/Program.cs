using NHibernate;
using QuickStart.Domain;
using QuickStart.Repository;
using System;
using System.Linq;

namespace QuickStart
{
    class Program
    {
        static void Main(string[] args)
        {
            //Blog blog;
            //using (var session = NHibernateHelper.OpenSession())
            //using (var tr = session.BeginTransaction())
            //{
            //    blog = session.Get<Blog>(32768);
            //    Console.WriteLine(NHibernateUtil.IsInitialized(blog.Posts));
            //    Console.WriteLine(blog.BlogId);
            //    Console.WriteLine(blog.Posts.Count);
            //    Console.WriteLine(blog.Posts[0].Title);
            //    Console.WriteLine(blog.Posts[1].Title);
            //    tr.Commit();
            //}

            //var blog = new Blog
            //{
            //    Name = "My Blog",
            //    CreationDate = DateTime.Now,
            //};
            //var post1 = new Post
            //{
            //    Title = "Post1",
            //    Timestamp = DateTime.Now,
            //    Blog = blog,
            //};
            //var post2 = new Post
            //{
            //    Title = "Post2",
            //    Timestamp = DateTime.Now,
            //    Blog = blog,
            //};
            //post1.Tags.Add("C#");
            //post1.Tags.Add("NHibernate");
            //post2.Tags.Add("JavaScript");
            //blog.Posts.Add(post1);
            //blog.Posts.Add(post2);

            //var repo = new Repository<Blog>();
            //repo.Add(blog);
            //Console.WriteLine(blog.BlogId);
            //Console.WriteLine(blog.Posts.Count);

            NHibernateHelper.Demo4();

            Console.ReadLine();
        }
    }
}
