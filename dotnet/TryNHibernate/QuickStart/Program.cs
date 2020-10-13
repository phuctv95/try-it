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
            //using (var session = NHibernateHelper.OpenSession())
            //using (var tr = session.BeginTransaction())
            //{

            //    tr.Commit();
            //}

            var blog = new Blog
            {
                Name = "My Blog",
                CreationDate = DateTime.Now,
            };
            var post1 = new Post
            {
                Title = "Abc",
                Timestamp = DateTime.Now,
                Blog = blog,
            };
            post1.Tags.Add("C#");
            post1.Tags.Add("NHibernate");
            blog.Posts.Add(post1);

            var repo = new Repository<Blog>();
            repo.Add(blog);

        }
    }
}
