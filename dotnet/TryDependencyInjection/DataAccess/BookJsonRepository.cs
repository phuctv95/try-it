using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace DataAccess
{
    public class BookJsonRepository : IBookRepository
    {
        private IList<Book> _books { get; set; } = new List<Book>();
        private const string BookFileName = "books.json";

        public void Remove(Book book)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToRemove == null)
            {
                return;
            }
            _books.RemoveAt(_books.IndexOf(bookToRemove));
            OverwriteJsonFile(_books);
        }

        public IList<Book> GetAllBooks()
        {
            _books = GetAllBooksFromJsonFile();
            return _books;
        }

        public void Insert(Book book)
        {
            book.Id = Guid.NewGuid();
            _books.Add(book);
            OverwriteJsonFile(_books);
        }

        public void Update(Book book)
        {
            var bookToUpdate = _books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToUpdate == null)
            {
                return;
            }
            bookToUpdate.Title = book.Title;
            OverwriteJsonFile(_books);
        }

        private void OverwriteJsonFile(IList<Book> books)
        {
            var json = JsonSerializer.Serialize(books);
            File.WriteAllText(BookFileName, json);
        }

        private IList<Book> GetAllBooksFromJsonFile()
        {
            if (!File.Exists(BookFileName))
            {
                File.WriteAllText(BookFileName, JsonSerializer.Serialize(new List<Book>()));
            }
            var json = File.ReadAllText(BookFileName);
            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }
    }
}
