using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Type;
using RepositoryMapByCode.Models;

namespace RepositoryMapByCode.Mappings
{
    public class CatMap : ClassMapping<Cat>
    {
        public CatMap()
        {
            Id(
                c => c.Id,
                m =>
                {
                    m.Column(c =>
                    {
                        c.Name("CatId");
                        c.SqlType("char(32)");
                        c.NotNullable(true);
                    });
                    m.Generator(Generators.UUIDHex());
                });
            Version(c => c.Version, m => m.Type(new DateTimeType()));
            Property(c => c.Name, m =>
            {
                m.Length(16);
                m.NotNullable(true);
            });
            Property(c => c.Sex);
            Property(c => c.Weight);
            ManyToOne(
                c => c.CatStore,
                m => m.Column("CatStoreId"));
        }
    }
}
