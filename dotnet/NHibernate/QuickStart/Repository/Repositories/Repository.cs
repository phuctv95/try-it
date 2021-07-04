namespace Repository.Repositories
{
    public class Repository<T> : IRepository<T>
    {
        public void Add(T entity)
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Save(entity);
            transaction.Commit();
        }

        public void Remove(T entity)
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Delete(entity);
            transaction.Commit();
        }

        public void Update(T entity)
        {
            using var session = NHibernateHelper.OpenSession();
            using var transaction = session.BeginTransaction();
            session.Update(entity);
            transaction.Commit();
        }

        public T Get(object id)
        {
            using var session = NHibernateHelper.OpenSession();
            return session.Get<T>(id);
        }
    }
}
