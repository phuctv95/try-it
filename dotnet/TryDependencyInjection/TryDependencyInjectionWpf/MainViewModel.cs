using System;
using System.Collections.ObjectModel;
using AutoMapper;
using DataAccess;
using TryDependencyInjectionWpf.Core;

namespace TryDependencyInjectionWpf
{
    public class MainViewModel : BaseNotifyPropertyChanged
    {
        public ObservableCollection<BookModel> Books { get; set; } = new ObservableCollection<BookModel>();
        public string BookTitleToInsert { get; set; } = string.Empty;

        public BookModel? SelectedBook
        {
            get => Get<BookModel?>();
            set
            {
                SetAndRaiseChangedNotify(value);
                SelectedBookTitle = value?.Title ?? string.Empty;
            }
        }

        public CommandHandler InsertBookCommand { get; }
        public CommandHandler UpdateBookCommand { get; }
        public CommandHandler RemoveBookCommand { get; }

        public string SelectedBookTitle
        {
            get => Get<string>();
            set => SetAndRaiseChangedNotify(value);
        }

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public MainViewModel(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            InsertBookCommand = new CommandHandler(InsertBook);
            UpdateBookCommand = new CommandHandler(UpdateBook);
            RemoveBookCommand = new CommandHandler(RemoveBook);

            var books = _bookRepository.GetAllBooks();
            foreach (var book in books)
            {
                Books.Add(mapper.Map<BookModel>(book));
            }
        }

        private void RemoveBook()
        {
            if (SelectedBook == null)
            {
                return;
            }
            _bookRepository.Remove(_mapper.Map<Book>(SelectedBook));
            Books.Remove(SelectedBook);
        }

        private void UpdateBook()
        {
            if (SelectedBook == null)
            {
                return;
            }
            SelectedBook.Title = SelectedBookTitle;
            _bookRepository.Update(_mapper.Map<Book>(SelectedBook));
        }

        private void InsertBook()
        {
            var book = new Book
            {
                Title = BookTitleToInsert,
                Available = new Random().Next(0, 2) == 0,
                Price = new Random().Next(10, 2000),
            };
            _bookRepository.Insert(book);
            Books.Add(_mapper.Map<BookModel>(book));
        }
    }
}
