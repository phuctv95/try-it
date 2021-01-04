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
        private IList<Book> Books { get; }

        public BookCsvRepository()
        {
            Books = ReadAllBooks();
        }

        public void Remove(Book book)
        {
            Books.RemoveAt(Books.IndexOf(Books.First(b => b.Id == book.Id)));

            if (!File.Exists(BookFileName))
            {
                File.Delete(BookFileName);
            }
            using (var file = File.CreateText(BookFileName))
            {
                foreach (var bookToWrite in Books)
                {
                    file.WriteLine($"{bookToWrite.Id}{CsvSeparator}{bookToWrite.Title}{CsvSeparator}{bookToWrite.Available}");
                }
            }
        }

        public IList<Book> GetAllBooks()
        {
            return Books;
        }

        public void Insert(Book book)
        {
            book.Id = Guid.NewGuid();
            using (var file = File.AppendText(BookFileName))
            {
                file.WriteLine($"{book.Id}{CsvSeparator}{book.Title}{CsvSeparator}{book.Available}");
            }
            Books.Add(book);
        }

        public void Update(Book book)
        {
            if (!File.Exists(BookFileName))
            {
                File.Delete(BookFileName);
            }
            using (var file = File.CreateText(BookFileName))
            {
                foreach (var bookToWrite in Books)
                {
                    file.WriteLine($"{bookToWrite.Id}{CsvSeparator}{bookToWrite.Title}{CsvSeparator}{bookToWrite.Available}");
                }
            }
        }

        private IList<Book> ReadAllBooks()
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
    }
}
