using System;
using System.Collections.Generic;
using DataAccess;
using NSubstitute;
using NUnit.Framework;

namespace TryDependencyInjection
{
    public class BookConsoleTest
    {
        private const int ActionInsert = 1;
        private const int ActionUpdate = 2;
        private const int ActionRemove = 3;
        private const int ActionExit = 4;
        private IBookRepository _bookRepository = Substitute.For<IBookRepository>();
        private IMyConsole _console = Substitute.For<IMyConsole>();

        [Test]
        public void Run_OnStartup_ShowAllBooks()
        {
            var bookConsole = new BookConsole(_bookRepository, _console);
            var book = new Book
            {
                Id = Guid.NewGuid(),
                Title = "Some title",
                Available = true,
            };
            _bookRepository.GetAllBooks().Returns(new List<Book>() { book });
            _console.ReadLine().Returns(ActionExit.ToString());

            bookConsole.Run();

            _console.Received(1).WriteLine($"0 | {book.Title} | {(book.Available ? "Is available" : "Not available")}");
        }
    }
}