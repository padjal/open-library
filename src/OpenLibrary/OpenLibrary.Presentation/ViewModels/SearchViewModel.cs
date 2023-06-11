using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;

namespace OpenLibrary.Presentation.ViewModels
{
    public class SearchViewModel : ViewModelBase
    {
        private INavigationService _navigationService;

        public SearchViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;

            NavigateBookInfoCommand = new RelayCommand(o => NavigationService.NavigateTo<BookInfoViewModel>(), o => true);
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
    }
}
