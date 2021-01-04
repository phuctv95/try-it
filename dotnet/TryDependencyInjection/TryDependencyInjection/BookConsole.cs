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

        public BookConsole(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
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
            Console.Write("Id of book to update: ");
            var index = int.Parse(Console.ReadLine());
            _bookRepository.Remove(_books[index]);
        }

        private void UpdateBook()
        {
            Console.Write("Id of book to update: ");
            var index = int.Parse(Console.ReadLine());
            Console.Write("New title: ");
            _books[index].Title = Console.ReadLine();
            _bookRepository.Update(_books[index]);
        }

        private void InsertNewBook()
        {
            Console.Write("Book title: ");
            _bookRepository.Insert(new Book
            {
                Id = Guid.NewGuid(),
                Title = Console.ReadLine(),
                Available = new Random().Next(0, 2) == 0
            });
        }

        private int GetUserAction()
        {
            Console.WriteLine($"Actions: [{ActionInsert}] Insert | [{ActionUpdate}] Update | [{ActionRemove}] Remove | [{ActionExit}] Exit");
            Console.Write("Your choice: ");
            return int.Parse(Console.ReadLine());
        }

        private void PrintListBooks(IList<Book> books)
        {
            var id = 0;
            foreach (var book in books)
            {
                Console.WriteLine($"{id++} | {book.Title} | {(book.Available ? "Is available" : "Not available")}");
            }
        }
    }
}