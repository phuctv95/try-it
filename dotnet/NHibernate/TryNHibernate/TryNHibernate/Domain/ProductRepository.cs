using NHibernate.Criterion;
using System;
using System.Collections.Generic;

namespace TryNHibernate.Domain
{
    public class ProductRepository : IProductRepository
    {
        public void Add(Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(product);
                transaction.Commit();
            }
        }

        public void Update(Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(product);
                transaction.Commit();
            }
        }

        public ICollection<Product> GetByCategory(string category)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Category", category))
                    .List<Product>();
            }
        }

        public Product GetById(Guid productId)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session.Get<Product>(productId);
            }
        }

        public Product GetByName(string name)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return session
                    .CreateCriteria(typeof(Product))
                    .Add(Restrictions.Eq("Name", name))
                    .UniqueResult<Product>();
            }
        }

        public void Remove(Product product)
        {
            using (var session = NHibernateHelper.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Delete(product);
                transaction.Commit();
            }
        }
    }
}
