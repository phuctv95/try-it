namespace RepositoryMapByCode.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        T Get(object id);
    }
}
