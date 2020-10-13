using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace QuickStart.Domain
{
    public class Cat
    {
        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual char Sex { get; set; }

        public virtual float Weight { get; set; }
    }

    public class CatMapping : ClassMapping<Cat>
    {
        public CatMapping()
        {
            Table("Cat");
            Id(x => x.Id, x =>
            {
                x.Column(y => 
                {
                    y.Name("CatId");
                    y.SqlType("char(32)");
                    y.NotNullable(true);
                });
                x.Generator(Generators.UUIDHex());
            });
            Property(x => x.Name, x =>
            {
                x.Column("Name");
                x.Length(16);
                x.NotNullable(true);
            });
            Property(x => x.Sex);
            Property(x => x.Weight);
        }
    }
}
