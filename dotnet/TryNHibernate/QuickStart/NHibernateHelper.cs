using NHibernate;
using NHibernate.Cfg;

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
                    _sessionFactory = new Configuration().Configure().BuildSessionFactory();
                }
                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static void CloseSession()
        {
            SessionFactory.Close();
        }
    }
}