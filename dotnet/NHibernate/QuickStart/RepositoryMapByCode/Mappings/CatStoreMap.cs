using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RepositoryMapByCode.Models;

namespace RepositoryMapByCode.Mappings
{
    public class CatStoreMap : ClassMapping<CatStore>
    {
        public CatStoreMap()
        {
            Id(
                c => c.Id,
                m => m.Generator(Generators.Guid));
            Property(c => c.Name);
            Bag(
                c => c.Cats,
                m =>
                {
                    m.Table(nameof(Cat));
                    m.Key(k => k.Column("CatStoreId"));
                    m.Cascade(Cascade.All | Cascade.DeleteOrphans);
                },
                m => m.OneToMany());
            Set(
                c => c.PhoneNumbers,
                m =>
                {
                    m.Table("PhoneNumber");
                    m.Key(k => k.Column("CatStoreId"));
                },
                m => m.Element(e =>
                {
                    e.Column("PhoneNumber");
                }));
        }
    }
}
