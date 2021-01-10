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
        private Book _book { get; } = new Book
        {
            Id = Guid.NewGuid(),
            Title = "Some title",
            Available = true,
        };

        [SetUp]
        public void SetUp()
        {
            _bookRepository.GetAllBooks().Returns(new List<Book>() { _book });
        }

        [Test]
        public void Run_OnStartup_ShowAllBooks()
        {
            var bookConsole = new BookConsole(_bookRepository, _console);
            _console.ReadLine().Returns(ActionExit.ToString());

            bookConsole.Run();

            _console.Received(1).WriteLine($"0 | {_book.Title} | {(_book.Available ? "Is available" : "Not available")}");
        }

        [Test]
        public void Run_WhenUserChooseInsert_CallInsertBook()
        {
            const string NewBookTitle = "Some Title";
            var bookConsole = new BookConsole(_bookRepository, _console);
            _console.ReadLine().Returns(
                ActionInsert.ToString(),
                NewBookTitle,
                ActionExit.ToString());

            bookConsole.Run();

            _bookRepository.Received(1).Insert(Arg.Is<Book>(b => b.Id == Guid.Empty && b.Title == NewBookTitle));
        }

        [Test]
        public void Run_WhenUserChooseUpdate_CallUpdateBook()
        {
            const string NewBookTitle = "Some Title";
            var bookConsole = new BookConsole(_bookRepository, _console);
            _console.ReadLine().Returns(
                ActionUpdate.ToString(),
                0.ToString(),
                NewBookTitle,
                ActionExit.ToString());

            bookConsole.Run();

            _bookRepository.Received(1).Update(Arg.Is<Book>(
                b => b.Id == _book.Id && b.Title == NewBookTitle && b.Available == _book.Available));
        }

        [Test]
        public void Run_WhenUserChooseRemove_CallRemoveBook()
        {
            var bookConsole = new BookConsole(_bookRepository, _console);
            _console.ReadLine().Returns(
                ActionRemove.ToString(),
                0.ToString(),
                ActionExit.ToString());

            bookConsole.Run();

            _bookRepository.Received(1).Remove(Arg.Is<Book>(
                b => b.Id == _book.Id && b.Title == _book.Title && b.Available == _book.Available));
        }
    }
}