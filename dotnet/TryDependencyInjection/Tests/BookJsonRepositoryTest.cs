using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DataAccess;
using FluentAssertions;
using NUnit.Framework;

namespace Tests
{
    public class BookJsonRepositoryTest
    {
        private const string BookFileName = "books.json";

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
                File.WriteAllText(BookFileName, JsonSerializer.Serialize(new List<Book>() { book }));
                var bookRepository = new BookJsonRepository();

                var actual = bookRepository.GetAllBooks();

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
                var bookRepository = new BookJsonRepository();

                bookRepository.Insert(book);

                var json = File.ReadAllText(BookFileName);
                var books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
                books.Count.Should().Be(1);
                books[0].Id.Should().NotBeEmpty();
                books[0].Title.Should().Be(book.Title);
                books[0].Available.Should().Be(book.Available);
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
                File.WriteAllText(BookFileName, JsonSerializer.Serialize(new List<Book>() { book }));
                var bookRepository = new BookJsonRepository();
                bookRepository.GetAllBooks();

                book.Title = "New title";
                bookRepository.Update(book);

                var json = File.ReadAllText(BookFileName);
                var books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
                books.Count.Should().Be(1);
                books[0].Id.Should().Be(book.Id);
                books[0].Title.Should().Be(book.Title);
                books[0].Available.Should().Be(book.Available);
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
                File.WriteAllText(BookFileName, JsonSerializer.Serialize(new List<Book>() { book1, book2 }));
                var bookRepository = new BookJsonRepository();
                bookRepository.GetAllBooks();

                bookRepository.Remove(book1);

                var json = File.ReadAllText(BookFileName);
                var books = JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
                books.Count.Should().Be(1);
                books[0].Id.Should().Be(book2.Id);
                books[0].Title.Should().Be(book2.Title);
                books[0].Available.Should().Be(book2.Available);
            }
            finally
            {
                File.Delete(BookFileName);
            }
        }
    }
}