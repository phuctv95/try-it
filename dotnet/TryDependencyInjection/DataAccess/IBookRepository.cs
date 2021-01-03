using System.Collections.Generic;

namespace DataAccess
{
    public interface IBookRepository
    {
        IList<Book> GetAllBooks();
        void Insert(Book book);
        void Update(Book book);
        void Remove(Book book);
    }
}
