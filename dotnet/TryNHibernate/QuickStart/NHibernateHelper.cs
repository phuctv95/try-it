using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using QuickStart.Domain;

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
                    cfg.DataBaseIntegration(db => db.SchemaAction = SchemaAutoAction.Create);
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
            return mapper.CompileMappingFor(new[] 
            { 
                typeof(Cat), typeof(Blog), typeof(Post)
            });
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}