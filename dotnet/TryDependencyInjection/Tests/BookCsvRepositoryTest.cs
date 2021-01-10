using System;
using System.IO;
using DataAccess;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class BookCsvRepositoryTest
    {
        private const string BookFileName = "books.csv";
        private const char CsvSeparator = ',';
        private IBookRepository _bookRepository = new BookCsvRepository();

        [Test]
        public void GetAllBooksTest()
        {
            try
            {
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Some title",
                    Available = false,
                };
                File.WriteAllText(BookFileName, $"{book.Id}{CsvSeparator}{book.Title}{CsvSeparator}{book.Available}");

                var actual = _bookRepository.GetAllBooks();

                actual.Count.Should().Be(1);
                actual[0].Id.Should().Be(book.Id);
                actual[0].Title.Should().Be(book.Title);
                actual[0].Available.Should().Be(book.Available);
            }
            finally
            {
                File.Delete(BookFileName);
            }
        }

        [Test]
        public void Insert()
        {
            try
            {
                var book = new Book
                {
                    Title = "Some title",
                    Available = false,
                };

                _bookRepository.Insert(book);

                var lines = File.ReadAllText(BookFileName)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                lines.Length.Should().Be(1);
                var rowData = lines[0].Split(CsvSeparator);
                (Guid.Parse(rowData[0])).Should().NotBeEmpty();
                rowData[1].Should().Be(book.Title);
                (bool.Parse(rowData[2])).Should().Be(book.Available);
            }
            finally
            {
                File.Delete(BookFileName);
            }
        }

        [Test]
        public void Update()
        {
            try
            {
                var book = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Some title",
                    Available = false,
                };
                File.WriteAllText(BookFileName, $"{book.Id}{CsvSeparator}{book.Title}{CsvSeparator}{book.Available}");
                _bookRepository.GetAllBooks();

                book.Title = "New title";
                _bookRepository.Update(book);

                var lines = File.ReadAllText(BookFileName)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                lines.Length.Should().Be(1);
                var rowData = lines[0].Split(CsvSeparator);
                (Guid.Parse(rowData[0])).Should().Be(book.Id);
                rowData[1].Should().Be(book.Title);
                (bool.Parse(rowData[2])).Should().Be(book.Available);
            }
            finally
            {
                File.Delete(BookFileName);
            }
        }

        [Test]
        public void Remove()
        {
            try
            {
                var book1 = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Title 1",
                    Available = false,
                };
                var book2 = new Book
                {
                    Id = Guid.NewGuid(),
                    Title = "Title 2",
                    Available = true,
                };
                File.WriteAllText(BookFileName,
                    $"{book1.Id}{CsvSeparator}{book1.Title}{CsvSeparator}{book1.Available}{Environment.NewLine}"
                    + $"{book2.Id}{CsvSeparator}{book2.Title}{CsvSeparator}{book2.Available}");
                _bookRepository.GetAllBooks();

                _bookRepository.Remove(book1);

                var lines = File.ReadAllText(BookFileName)
                    .Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
                lines.Length.Should().Be(1);
                var rowData = lines[0].Split(CsvSeparator);
                (Guid.Parse(rowData[0])).Should().Be(book2.Id);
                rowData[1].Should().Be(book2.Title);
                (bool.Parse(rowData[2])).Should().Be(book2.Available);
            }
            finally
            {
                File.Delete(BookFileName);
            }
        }
    }
}