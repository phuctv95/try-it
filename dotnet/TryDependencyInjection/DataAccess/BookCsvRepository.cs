using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using log4net;

namespace DataAccess
{
    public class BookCsvRepository : IBookRepository
    {
        private const string BookFileName = "books.csv";
        private const char CsvSeparator = ',';
		private static readonly ILog _logger = LogManager.GetLogger(typeof(BookCsvRepository));
        private IList<Book> _books { get; set; } = new List<Book>();

        public void Remove(Book book)
        {
            var bookToRemove = _books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToRemove == null)
            {
                _logger.Error($"Unexpected result, not found the book \"{book.Title}\" to remove.");
                throw new NullReferenceException();
            }
            _books.RemoveAt(_books.IndexOf(bookToRemove));
            OverwriteCsvFile();
            _logger.Info($"Removed book \"{book.Title}\".");
        }

        public IList<Book> GetAllBooks()
        {
            _books = GetAllBooksFromCsv();
            _logger.Info($"Got {_books.Count} books.");
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
            _logger.Info($"Inserted book \"{book.Title}\".");
        }

        public void Update(Book book)
        {
            var bookToUpddate = _books.FirstOrDefault(b => b.Id == book.Id);
            if (bookToUpddate == null)
            {
                _logger.Error($"Unexpected result, not found the book \"{book.Title}\" to update.");
                throw new NullReferenceException();
            }
            bookToUpddate.Title = book.Title;
            OverwriteCsvFile();
            _logger.Info($"Updated book \"{book.Title}\".");
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
