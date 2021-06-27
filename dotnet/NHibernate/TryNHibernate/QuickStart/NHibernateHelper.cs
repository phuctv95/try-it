using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Criterion;
using NHibernate.Mapping.ByCode;
using NHibernate.Transform;
using NHibernate.Util;
using QuickStart.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuickStart
{
    public sealed class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {

                    var cfg = new Configuration();
                    cfg.Configure();
                    cfg.DataBaseIntegration(db => db.SchemaAction = SchemaAutoAction.Update);
                    cfg.AddMapping(GetMappings());
                    _sessionFactory = cfg.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        private static HbmMapping GetMappings()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<CatMapping>();
            mapper.AddMapping<BlogMapping>();
            mapper.AddMapping<PostMapping>();
            mapper.AddMapping<PersonMapping>();
            mapper.AddMapping<NationalCitizenMapping>();
            mapper.AddMapping<ForeignCitizenMapping>();
            mapper.AddMapping<SportMapping>();
            mapper.AddMapping<SportFootballMapping>();
            mapper.AddMapping<SportChessMapping>();
            return mapper.CompileMappingFor(new[] 
            { 
                typeof(Cat), typeof(Blog), typeof(Post), typeof(Person),
                typeof(NationalCitizen), typeof(ForeignCitizen),
                typeof(Sport), typeof(SportFootball), typeof(SportChess),
            });
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void Demo1()
        {
            using (var session = OpenSession())
            using (var tr = session.BeginTransaction())
            {
                var blog = session
                    .QueryOver<Blog>()
                    .Fetch(SelectMode.Skip, x => x.Posts)
                    .Where(x => x.BlogId == 32768)
                    .List<Blog>()
                    .FirstOrDefault();

                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.Posts?.Count);

                tr.Commit();
            }
        }

        public static void Demo2()
        {
            using (var session = OpenSession())
            using (var tr = session.BeginTransaction())
            {
                const int NotFoundId = 9999;

                // Throws NHibernate.ObjectNotFoundException later
                //var blog = session.Load<Blog>(NotFoundId);

                // blog will be null.
                var blog = session.Get<Blog>(NotFoundId);

                Console.WriteLine(blog?.BlogId);
                Console.WriteLine(blog?.Posts?.Count);

                tr.Commit();
            }
        }

        public static void Demo3()
        {
            using (var session = OpenSession())
            using (var tr = session.BeginTransaction())
            {
                // Conditions.
                var posts = session.CreateCriteria<Post>()
                    .Add(Restrictions.Like("Title", "Post%"))
                    .SetResultTransformer(Transformers.DistinctRootEntity)
                    .List<Post>();

                PrintPosts(posts, "Conditions");

                // Ordering.
                posts = session.CreateCriteria<Post>()
                    .AddOrder(Order.Asc("PostId"))
                    .SetMaxResults(5)
                    .SetResultTransformer(Transformers.DistinctRootEntity)
                    .List<Post>();

                PrintPosts(posts, "Ordering");

                // Associations.
                var blogs = session.CreateCriteria<Blog>()
                    .CreateCriteria("Posts")
                    .Add(Restrictions.Eq("Title", "Post1"))
                    .SetResultTransformer(Transformers.DistinctRootEntity)
                    .List<Blog>();

                PrintBlog(blogs, "Associations");

                // Fetching.
                blogs = session.CreateCriteria<Blog>()
                    .Fetch(SelectMode.Fetch, "Posts")
                    .SetResultTransformer(Transformers.DistinctRootEntity)
                    .List<Blog>();

                PrintBlog(blogs, "Fetching");

                // Example query.
                var example = Example
                    .Create(new Blog { Name = "My Blog" })
                    .ExcludeProperty("CreationDate")
                    .ExcludeZeroes();

                blogs = session.CreateCriteria<Blog>()
                    .Add(example)
                    .SetResultTransformer(Transformers.DistinctRootEntity)
                    .List<Blog>();

                PrintBlog(blogs, "Example query");

                // Projections.
                var postsCount = session.CreateCriteria<Post>()
                    .SetProjection(Projections.ProjectionList()
                        .Add(Projections.RowCount())
                        .Add(Projections.Max("Timestamp")))
                    .List<object[]>();

                Console.WriteLine(">> Projections:");
                Console.WriteLine($"{postsCount[0][0]} | {postsCount[0][1]}\n");

                // Sub-queries.
                var blogsWithRestrictions = DetachedCriteria.For<Post>()
                    .SetProjection(Projections.Property("Blog.BlogId"))
                    .Add(Restrictions.Eq("Title", "Post1"));
                blogs = session.CreateCriteria<Blog>()
                    .Add(Subqueries.PropertyIn("BlogId", blogsWithRestrictions))
                    .List<Blog>();

                PrintBlog(blogs, "Sub-queries");

                tr.Commit();
            }
        }

        public static void Demo4()
        {
            using (var session = OpenSession())
            using (var tr = session.BeginTransaction())
            {
                // Conditions.
                var posts = session.QueryOver<Post>()
                    .Where(x => x.Title.IsLike("Post%"))
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .List<Post>();

                PrintPosts(posts, "Conditions");

                //// Ordering.
                posts = session.QueryOver<Post>()
                    .OrderBy(x => x.PostId).Asc
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .Take(5)
                    .List<Post>();

                PrintPosts(posts, "Ordering");

                //// Associations.
                var blogs = session.QueryOver<Blog>()
                    .JoinQueryOver<Post>(x => x.Posts)
                    .Where(x => x.Title == "Post1")
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .List<Blog>();

                PrintBlog(blogs, "Associations");

                // Fetching.
                blogs = session.QueryOver<Blog>()
                    .Fetch(SelectMode.Fetch, x => x.Posts)
                    .TransformUsing(Transformers.DistinctRootEntity)
                    .List<Blog>();

                PrintBlog(blogs, "Fetching");

                // Projections.
                var postsWithFewColumns = session.QueryOver<Post>()
                    .Select(x => x.PostId, x => x.Title)
                    .List<object[]>();

                Console.WriteLine(">> Projections:");
                Console.WriteLine($"{postsWithFewColumns[0][0]} | {postsWithFewColumns[0][1]}");
                Console.WriteLine($"{postsWithFewColumns[1][0]} | {postsWithFewColumns[1][1]}\n");

                //// Sub-queries.
                var blogsWithRestrictions = QueryOver.Of<Post>()
                    .SelectList(x => x.Select(y => y.Blog.BlogId))
                    .Where(x => x.Title == "Post1");
                blogs = session.QueryOver<Blog>()
                    .Where(Subqueries.WhereProperty<Blog>(x => x.BlogId).In(blogsWithRestrictions))
                    .List<Blog>();

                PrintBlog(blogs, "Sub-queries");

                // Transform.
                var customPost = null as CustomPost;
                var customPosts = session.QueryOver<Post>()
                    .SelectList(p => p
                        .Select(x => x.PostId).WithAlias(() => customPost.Id)
                        .Select(Projections.SqlFunction(
                                "concat",
                                NHibernateUtil.String,
                                Projections.Cast(NHibernateUtil.String, Projections.Property<Post>(y => y.PostId)),
                                Projections.Constant("#"),
                                Projections.Property<Post>(y => y.Title))).WithAlias(() => customPost.Title))
                    .TransformUsing(Transformers.AliasToBean<CustomPost>())
                    .List<CustomPost>();

                Console.WriteLine(">> Transform:");
                Console.WriteLine($"{customPosts[0].Id} | {customPosts[0].Title}");
                Console.WriteLine($"{customPosts[1].Id} | {customPosts[1].Title}\n");

                // Select blog id, count post.
                var blog = null as Blog;
                var customPosts2 = session.QueryOver<Blog>(() => blog)
                    .JoinQueryOver<Post>(x => x.Posts)
                    .SelectList(x => x
                        .SelectGroup(y => blog.BlogId)
                        .SelectCount(y => y.BlogId))
                    .List<object[]>()
                    .Select(p => new { BlogId = p[0], PostsCount = p[1] });

                tr.Commit();

            }
        }
        private class CustomPost { public int Id;  public string Title; }

        public static void PrintBlog(IList<Blog> blogs, string title = null)
        {
            Console.WriteLine($">> {title}:");
            foreach (var blog in blogs)
            {
                Console.WriteLine($"{blog.BlogId} | {blog.Name}");
            }
            Console.WriteLine();
        }

        public static void PrintPosts(IList<Post> posts, string title = null)
        {
            Console.WriteLine($">> {title}:");
            foreach (var post in posts)
            {
                Console.WriteLine($"{post.PostId} | {post.Title}");
            }
            Console.WriteLine();
        }
    }
}