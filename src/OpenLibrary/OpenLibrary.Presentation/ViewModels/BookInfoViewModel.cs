using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using OpenLibrary.Core.Models;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;
using OpenLibrary.Presentation.Services;
using OpenLibrary.Presentation.Views;

namespace OpenLibrary.Presentation.ViewModels
{
    public class BookInfoViewModel : ViewModelBase
    {
        private INavigationService _navigationService;
        private Book _book;

        public BookInfoViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }

        public INavigationService NavigationService
        {
            get => _navigationService;
            set => SetField(ref _navigationService, value);
        }

        public Book Book 
        {
            get => _book;
            set => SetField(ref _book, value);
        }

        public ICommand NavigateSearchCommand =>
            new RelayCommand(o => NavigationService.NavigateTo<SearchViewModel>(), o => true);

        public ICommand ImageLoadedCommand => new RelayCommand(o => AfterImageLoaded(), o => true);

        private async Task AfterImageLoaded()
        {

        }
    }
}
