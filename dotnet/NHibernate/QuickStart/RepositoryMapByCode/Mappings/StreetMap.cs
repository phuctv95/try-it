using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using RepositoryMapByCode.Models;

namespace RepositoryMapByCode.Mappings
{
    public class StreetMap : ClassMapping<Street>
    {
        public StreetMap()
        {
            Id(
                x => x.Id,
                m => m.Generator(Generators.Identity));
            Property(
                x => x.Name,
                m => m.Length(MsSql2000Dialect.MaxSizeForLengthLimitedString + 1));
        }
    }
}
