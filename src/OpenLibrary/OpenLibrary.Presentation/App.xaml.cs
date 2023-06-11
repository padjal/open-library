using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenLibrary.Presentation.Interfaces;
using OpenLibrary.Presentation.Services;
using OpenLibrary.Presentation.ViewModels;
using OpenLibrary.Presentation.Views;

namespace OpenLibrary.Presentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    //Services
                    services.AddSingleton<INavigationService, NavigationService>();
                    services.AddSingleton<Func<Type, ViewModelBase>>(serviceProvider =>
                        viewModelType => (ViewModelBase)serviceProvider.GetRequiredService(viewModelType));

                    //ViewModels
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<BookInfoViewModel>();
                    services.AddSingleton<SearchViewModel>(); 

                    //Views
                    services.AddSingleton<MainView>(serviceProvider => new MainView()
                    {
                        DataContext = serviceProvider.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();

            //Set startup screen
            AppHost.Services.GetRequiredService<INavigationService>().NavigateTo<SearchViewModel>();

            var startupForm = AppHost.Services.GetRequiredService<MainView>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();

            base.OnExit(e);
        }
    }
}
