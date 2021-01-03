using DataAccess;
using System;
using System.Collections;
using System.Collections.Generic;

namespace TryDependencyInjection
{
    public class BookConsole
    {
        private const int ACTION_INSERT = 1;
        private const int ACTION_UPDATE = 2;
        private const int ACTION_REMOVE = 3;
        private const int ACTION_EXIT = 4;

        private IList<Book> Books = new List<Book>();
        private readonly IBookRepository BookRepository;

        public BookConsole(IBookRepository bookRepository)
        {
            BookRepository = bookRepository;
        }

        public void Run()
        {
            while (true)
            {
                Books = BookRepository.GetAllBooks();
                PrintListBooks(Books);
                var action = GetUserAction();
                if (action == ACTION_EXIT)
                {
                    break;
                }
                if (action == ACTION_INSERT)
                {
                    InsertNewBook();
                }
                if (action == ACTION_UPDATE)
                {
                    UpdateBook();
                }
                if (action == ACTION_REMOVE)
                {
                    RemoveBook();
                }
            }
        }

        private void RemoveBook()
        {
            Console.Write("Id of book to update: ");
            var index = int.Parse(Console.ReadLine());
            BookRepository.Remove(Books[index]);
        }

        private void UpdateBook()
        {
            Console.Write("Id of book to update: ");
            var index = int.Parse(Console.ReadLine());
            Console.Write("New title: ");
            Books[index].Title = Console.ReadLine();
            BookRepository.Update(Books[index]);
        }

        private void InsertNewBook()
        {
            Console.Write("Book title: ");
            BookRepository.Insert(new Book
            {
                Id = Guid.NewGuid(),
                Title = Console.ReadLine(),
                Available = new Random().Next(0, 2) == 0
            });
        }

        private int GetUserAction()
        {
            Console.WriteLine($"Actions: [{ACTION_INSERT}] Insert | [{ACTION_UPDATE}] Update | [{ACTION_REMOVE}] Remove | [{ACTION_EXIT}] Exit");
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