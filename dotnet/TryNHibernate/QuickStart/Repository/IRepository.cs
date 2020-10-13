using System;
using System.Collections.Generic;

namespace QuickStart.Domain.Repository
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
