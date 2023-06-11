using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OpenLibrary.Core.Models;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;

namespace OpenLibrary.Presentation.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private Book _selectedBook;
        private string _searchAuthor;
        private string _searchTitle;

        public SearchViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            NavigateBookInfoCommand = new RelayCommand(o => NavigationService.NavigateTo<BookInfoViewModel>(), o => true);

            AfterSearchTitleUpdated = new RelayCommand(o => { SearchAuthor = string.Empty; }, o => true);
            AfterSearchAuthorUpdated = new RelayCommand(o => { SearchTitle = string.Empty; }, o => true);
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

        public ObservableCollection<Book> Books { get; set; } = new ObservableCollection<Book> 
        {
            //TODO: Remove. Used only for development
            new Book
            {
                Title = "Oliver Twist", 
                Author = "Charles Dikens",
                PageCount = 678,
                Isbn = "SDFKSDJFO90808DFSF",
                PublishedTime = DateTime.Now,
            },
            new Book
            {
                Title = "Oliver Twist 2",
                Author = "Charles Dikens",
                PageCount = 678,
                Isbn = "SDFKSDJFO90808DFSF",
                PublishedTime = DateTime.Now,
            }
        };

        public Book SelectedBook
        {
            get => _selectedBook;
            set
            {
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
    }
}
