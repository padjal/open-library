using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OpenLibrary.Presentation.Core;
using OpenLibrary.Presentation.Interfaces;
using OpenLibrary.Presentation.ViewModels;

namespace OpenLibrary.Presentation.Services
{
    public class NavigationService : ObservableObject, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
           ViewModelBase viewModel = _viewModelFactory.Invoke(typeof(TViewModel));

           CurrentViewModel = viewModel;
        }
    }
}
