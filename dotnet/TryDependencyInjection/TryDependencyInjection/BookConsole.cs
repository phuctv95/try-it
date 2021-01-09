using DataAccess;
using System;
using System.Collections.Generic;

namespace TryDependencyInjection
{
    public class BookConsole
    {
        private const int ActionInsert = 1;
        private const int ActionUpdate = 2;
        private const int ActionRemove = 3;
        private const int ActionExit = 4;

        private IList<Book> _books = new List<Book>();
        private readonly IBookRepository _bookRepository;
        private readonly IMyConsole _console;

        public BookConsole(IBookRepository bookRepository, IMyConsole console)
        {
            _bookRepository = bookRepository;
            _console = console;
        }

        public void Run()
        {
            while (true)
            {
                _books = _bookRepository.GetAllBooks();
                PrintListBooks(_books);
                var action = GetUserAction();
                if (action == ActionExit)
                {
                    break;
                }
                if (action == ActionInsert)
                {
                    InsertNewBook();
                }
                if (action == ActionUpdate)
                {
                    UpdateBook();
                }
                if (action == ActionRemove)
                {
                    RemoveBook();
                }
            }
        }

        private void RemoveBook()
        {
            _console.Write("Id of book to update: ");
            var index = int.Parse(_console.ReadLine());
            _bookRepository.Remove(_books[index]);
        }

        private void UpdateBook()
        {
            _console.Write("Id of book to update: ");
            var index = int.Parse(_console.ReadLine());
            _console.Write("New title: ");
            _books[index].Title = _console.ReadLine();
            _bookRepository.Update(_books[index]);
        }

        private void InsertNewBook()
        {
            _console.Write("Book title: ");
            _bookRepository.Insert(new Book
            {
                Title = _console.ReadLine(),
                Available = new Random().Next(0, 2) == 0
            });
        }

        private int GetUserAction()
        {
            _console.WriteLine($"Actions: [{ActionInsert}] Insert | [{ActionUpdate}] Update | [{ActionRemove}] Remove | [{ActionExit}] Exit");
            _console.Write("Your choice: ");
            return int.Parse(_console.ReadLine());
        }

        private void PrintListBooks(IList<Book> books)
        {
            var id = 0;
            foreach (var book in books)
            {
                _console.WriteLine($"{id++} | {book.Title} | {(book.Available ? "Is available" : "Not available")}");
            }
        }
    }
}