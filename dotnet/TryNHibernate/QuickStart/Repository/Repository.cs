using QuickStart.Domain.Repository;
using System;

namespace QuickStart.Repository
{
    public class Repository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var tran = session.BeginTransaction())
            {
                session.Save(entity);
                tran.Commit();
            }
        }

        public void Remove(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var tran = session.BeginTransaction())
            {
                session.Delete(entity);
                tran.Commit();
            }
        }

        public void Update(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var tran = session.BeginTransaction())
            {
                session.Update(entity);
                tran.Commit();
            }
        }
    }
}
