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
                if (File.Exists(BookFileName))
                {
                    File.Delete(BookFileName);
                }
            }
        }
    }
}