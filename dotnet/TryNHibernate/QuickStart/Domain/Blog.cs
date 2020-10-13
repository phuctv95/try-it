using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace QuickStart.Domain
{
    public class Blog
    {
        public Blog()
        {
            Posts = new List<Post>();
        }

        public virtual int BlogId { get; protected set; }
        public virtual string Name { get; set; }
        public virtual DateTime CreationDate { get; set; }
        public virtual int PostCount { get; protected set; }
        public virtual IList<Post> Posts { get; protected set; }
    }

    public class BlogMapping : ClassMapping<Blog>
    {
        public BlogMapping()
        {
            Table(nameof(Blog));
            Lazy(true);
            Id(x => x.BlogId, x => x.Generator(Generators.HighLow));
            Property(x => x.Name, x =>
            {
                x.Length(50);
                x.NotNullable(true);
            });
            Property(x => x.CreationDate, x =>
            {
                x.NotNullable(true);
            });
            Property(x => x.PostCount, x =>
            {
                x.Formula("(SELECT COUNT(1) FROM Post WHERE Post.BlogId = BlogId)");
            });
            List(
                x => x.Posts, 
                x =>
                {
                    x.Key(y =>
                    {
                        y.Column("BlogId");
                        y.NotNullable(true);
                    });
                    x.Lazy(CollectionLazy.Lazy);
                    x.Cascade(Cascade.All | Cascade.DeleteOrphans);
                    x.Inverse(true);
                }, 
                x => x.OneToMany());
        }
    }
}
