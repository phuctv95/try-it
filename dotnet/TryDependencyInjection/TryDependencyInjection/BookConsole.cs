using AutoMapper;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TryDependencyInjection
{
    public class BookConsole
    {
        private const int ActionInsert = 1;
        private const int ActionUpdate = 2;
        private const int ActionRemove = 3;
        private const int ActionExit = 4;

        private IList<BookRepresentation> _books = new List<BookRepresentation>();
        private readonly IBookRepository _bookRepository;
        private readonly IMyConsole _console;
        private readonly IMapper _mapper;

        public BookConsole(IBookRepository bookRepository, IMyConsole console, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _console = console;
            _mapper = mapper;
        }

        public void Run()
        {
            while (true)
            {
                var books = _bookRepository.GetAllBooks();
                _books = books.Select(x => _mapper.Map<BookRepresentation>(x)).ToList();
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
            _bookRepository.Remove(_mapper.Map<Book>(_books[index]));
        }

        private void UpdateBook()
        {
            _console.Write("Id of book to update: ");
            var index = int.Parse(_console.ReadLine());
            _console.Write("New title: ");
            _books[index].Title = _console.ReadLine();
            _bookRepository.Update(_mapper.Map<Book>(_books[index]));
        }

        private void InsertNewBook()
        {
            _console.Write("Book title: ");
            _bookRepository.Insert(new Book
            {
                Title = _console.ReadLine(),
                Available = new Random().Next(0, 2) == 0,
                Price = new Random().Next(10, 2000),
            });
        }

        private int GetUserAction()
        {
            _console.WriteLine($"Actions: [{ActionInsert}] Insert | [{ActionUpdate}] Update | [{ActionRemove}] Remove | [{ActionExit}] Exit");
            _console.Write("Your choice: ");
            return int.Parse(_console.ReadLine());
        }

        private void PrintListBooks(IList<BookRepresentation> books)
        {
            var id = 0;
            foreach (var book in books)
            {
                _console.WriteLine($"{id++} | {book.Title} | {book.Available} | {book.Price} | {book.ExpensiveLevel}");
            }
        }
    }
}