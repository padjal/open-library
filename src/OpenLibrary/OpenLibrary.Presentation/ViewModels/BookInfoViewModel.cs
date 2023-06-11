using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;
using OpenLibrary.Presentation.Services;
using OpenLibrary.Presentation.Views;

namespace OpenLibrary.Presentation.ViewModels
{
    public class BookInfoViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public BookInfoViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
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

        public ICommand NavigateSearchCommand =>
            new RelayCommand(o => NavigationService.NavigateTo<SearchViewModel>(), o => true);
    }
}
