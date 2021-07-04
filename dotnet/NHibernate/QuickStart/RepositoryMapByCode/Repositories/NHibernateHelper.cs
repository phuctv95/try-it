using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;
using RepositoryMapByCode.Mappings;
using RepositoryMapByCode.Models;

namespace RepositoryMapByCode.Repositories
{
    public static class NHibernateHelper
    {
        private static ISessionFactory? _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var configuration = new Configuration();
                    configuration.Configure();
                    configuration.AddMapping(GetMapings());
                    _sessionFactory = configuration.BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        /// <summary>
        /// Drops existing tables and then regenerate them.
        /// </summary>
        public static void ResetSchema()
        {
            var configuration = new Configuration();
            configuration.Configure();
            configuration.AddMapping(GetMapings());
            new SchemaExport(configuration).Execute(false, true, false);
        }

        private static HbmMapping GetMapings()
        {
            var mapper = new ModelMapper();
            mapper.AddMapping<StreetMap>();
            mapper.AddMapping<CatStoreMap>();
            mapper.AddMapping<CatMap>();
            var hbmMapping = mapper.CompileMappingFor(new[]
                        {
                typeof(Street), typeof(CatStore), typeof(Cat)
            });
            hbmMapping.autoimport = false;
            return hbmMapping;
        }
    }
}
