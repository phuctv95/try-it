using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Repository.Models;

namespace Repository.Repositories
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
            new SchemaExport(configuration).Execute(false, true, false);
        }
    }
}
