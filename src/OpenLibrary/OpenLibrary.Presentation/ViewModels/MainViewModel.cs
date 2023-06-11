using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;

namespace OpenLibrary.Presentation.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            NavigateBookInfoCommand = new RelayCommand(o => NavigationService.NavigateTo<BookInfoViewModel>(), o => true);
            NavigateSearchCommand = new RelayCommand(o => NavigationService.NavigateTo<SearchViewModel>(), o => true);

            //NavigationService.NavigateTo<SearchViewModel>();
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

        public RelayCommand NavigateSearchCommand {get; set; }

        public RelayCommand NavigateBookInfoCommand { get; set; }
    }
}
