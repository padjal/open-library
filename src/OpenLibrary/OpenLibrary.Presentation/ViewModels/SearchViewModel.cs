using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OpenLibrary.Core.Exceptions;
using OpenLibrary.Core.Interfaces;
using OpenLibrary.Core.Models;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;

namespace OpenLibrary.Presentation.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private readonly IBookService _bookService;
        private readonly BookInfoViewModel _bookInfoViewModel;
        private INavigationService _navigationService;
        private Book _selectedBook;
        private string _searchAuthor;
        private string _searchTitle;
        private ObservableCollection<Book> _books;
        private string _resultLabel;

        public SearchViewModel(INavigationService navigationService, IBookService bookService, BookInfoViewModel bookInfoViewModel)
        {
            _bookInfoViewModel = bookInfoViewModel;
            NavigationService = navigationService;
            _bookService = bookService;
            NavigateBookInfoCommand = new RelayCommand(o => NavigationService.NavigateTo<BookInfoViewModel>(), o => true);

            AfterSearchTitleUpdated = new RelayCommand(o => { SearchAuthor = string.Empty; }, o => true);
            AfterSearchAuthorUpdated = new RelayCommand(o => { SearchTitle = string.Empty; }, o => true);

            Books = new ObservableCollection<Book>();
        }

        public INavigationService NavigationService
        {
            get => _navigationService;
            set
            {
                _navigationService = value;
                OnPropertyChanged();
            }
        }

        public ICommand NavigateBookInfoCommand { get; set; }

        public ICommand AfterSearchTitleUpdated { get; set; }

        public ICommand AfterSearchAuthorUpdated { get; set; }

        public ICommand SearchCommand => new RelayCommand(o => Search(), o => true);

        public ObservableCollection<Book> Books 
        {
            get => _books;
            set => SetField(ref _books, value);
        }

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
                SetField(ref _selectedBook, value);

                _bookInfoViewModel.Book = SelectedBook;

                NavigationService.NavigateTo<BookInfoViewModel>();
            }
        }

        public string SearchAuthor
        {
            get => _searchAuthor;
            set => SetField(ref _searchAuthor, value);
        }

        public string SearchTitle
        {
            get => _searchTitle;
            set => SetField(ref _searchTitle, value);
        }

        public string ResultLabel
        {
            get => _resultLabel;
            set => SetField(ref _resultLabel, value);
        }

        private async Task Search()
        {
            if (!string.IsNullOrEmpty(SearchAuthor))
            {
                //Search by author

                var timer = new Stopwatch();
                timer.Start();

                try
                {
                    var result = await _bookService.GetBooksByAuthorAsync(SearchAuthor);

                    Books.Clear();

                    Books = new ObservableCollection<Book>(result);
                }catch (HostException hostException) 
                {
                    MessageBox.Show("Service is not reachable at the moment. Please check your connection and try again.\n" +
                        hostException.Message,
                        "Service not reachable");
                }
                

                timer.Stop();
                ResultLabel = $"{Books.Count} results found in {timer.Elapsed.TotalMilliseconds} ms";
                

            }
            else if (!string.IsNullOrEmpty(SearchTitle))
            {
                //Search by title

                var timer = new Stopwatch();
                timer.Start();

                try
                {
                    var result = await _bookService.GetBooksByTitleAsync(SearchTitle);

                    Books.Clear();

                    Books = new ObservableCollection<Book>(result);
                }catch (HostException hostException)
                {
                    MessageBox.Show("Service is not reachable at the moment. Please check your connection and try again.\n" +
                        hostException.Message,
                        "Service not reachable");
                }
                

                timer.Stop();
                ResultLabel = $"{Books.Count} results found in {timer.Elapsed.TotalMilliseconds} ms";
                //Disable busyIndicator
            }
            else
            {
                MessageBox.Show("No search criteria found.\n" +
                    "Please add either a book title or author and try again.",
                    "Search error");
            }
        }
    }
}
