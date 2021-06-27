using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using System;
using System.Collections.Generic;

namespace QuickStart.Domain
{
    public class Post
    {
        public Post()
        {
            Tags = new HashSet<string>();
        }

        public virtual int PostId { get; protected set; }
        public virtual Blog Blog { get; set; }
        public virtual DateTime Timestamp { get; set; }
        public virtual string Title { get; set; }
        public virtual ISet<string> Tags { get; protected set; }
    }

    public class PostMapping : ClassMapping<Post>
    {
        public PostMapping()
        {
            Table(nameof(Post));
            Lazy(true);
            Id(x => x.PostId, x => x.Generator(Generators.HighLow));
            Property(x => x.Timestamp, x =>
            {
                x.NotNullable(true);
            });
            Property(x => x.Title, x =>
            {
                x.Length(50);
                x.NotNullable(true);
            });
            ManyToOne(x => x.Blog, x =>
            {
                x.Column("BlogId");
                x.NotNullable(true);
                x.Lazy(LazyRelation.NoProxy);
            });
            Set(
                x => x.Tags, 
                x =>
                {
                    x.Key(y =>
                    {
                        y.Column("PostId");
                        y.NotNullable(true);
                    });
                    x.Cascade(Cascade.All);
                    x.Lazy(CollectionLazy.NoLazy);
                    x.Fetch(CollectionFetchMode.Join);
                    x.Table("Tag");
                },
                x => 
                {
                    x.Element(y =>
                    {
                        y.Column("Tag");
                        y.Length(20);
                        y.NotNullable(true);
                        y.Unique(true);
                    });
                });
        }
    }
}