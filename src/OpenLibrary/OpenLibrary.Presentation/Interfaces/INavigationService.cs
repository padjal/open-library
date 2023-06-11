using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenLibrary.Presentation.ViewModels;

namespace OpenLibrary.Presentation.Interfaces
{
    public interface INavigationService
    {
        public ViewModelBase CurrentViewModel { get; }

        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;
    }
}
