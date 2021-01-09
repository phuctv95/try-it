using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataAccess
{
    public class BookCsvRepository : IBookRepository
    {
        private const string BookFileName = "books.csv";
        private const char CsvSeparator = ',';
        private IList<Book> _books { get; set; } = new List<Book>();

        public void Remove(Book book)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToRemove == null)
            {
                return;
            }
            _books.RemoveAt(_books.IndexOf(bookToRemove));
            OverwriteCsvFile();
        }

        public IList<Book> GetAllBooks()
        {
            _books = GetAllBooksFromCsv();
            return _books;
        }

        public void Insert(Book book)
        {
            book.Id = Guid.NewGuid();
            _books.Add(book);
            using (var file = File.AppendText(BookFileName))
            {
                file.WriteLine($"{book.Id}{CsvSeparator}{book.Title}{CsvSeparator}{book.Available}");
            }
        }

        public void Update(Book book)
        {
            var bookToUpddate = _books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToUpddate == null)
            {
                return;
            }
            bookToUpddate.Title = book.Title;
            OverwriteCsvFile();
        }

        private IList<Book> GetAllBooksFromCsv()
        {
            if (!File.Exists(BookFileName))
            {
                File.Create(BookFileName).Dispose();
                return new List<Book>();
            }

            var lines = File.ReadAllLines(BookFileName);
            return lines.Select(line =>
            {
                var elements = line.Split(CsvSeparator);
                return new Book { Id = Guid.Parse(elements[0]), Title = elements[1], Available = bool.Parse(elements[2]) };
            }).ToList();
        }

        private void OverwriteCsvFile()
        {
            if (!File.Exists(BookFileName))
            {
                File.Delete(BookFileName);
            }
            using (var file = File.CreateText(BookFileName))
            {
                foreach (var bookToWrite in _books)
                {
                    file.WriteLine($"{bookToWrite.Id}{CsvSeparator}{bookToWrite.Title}{CsvSeparator}{bookToWrite.Available}");
                }
            }
        }
    }
}
