using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;
using Splat;
using Ynov_Workshare.Models;
using Ynov_Workshare.Utils;
using Ynov_Workshare.ViewModels;
using Ynov_Workshare.Views;

namespace Ynov_Workshare;

public class App : Application
{
    private UserService _userService;
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        // Créer un conteneur DI
        var services = new ServiceCollection();

        // Enregistrer ApiService en tant que service
        services.AddScoped<ApiService>();
        services.AddScoped<UserService>();
        services.AddScoped<LocalStorage>();

        // Construire le ServiceProvider à partir des services enregistrés
        var serviceProvider = services.BuildServiceProvider();

        // Résoudre ApiService à partir du ServiceProvider
         _userService = serviceProvider.GetService<UserService>();
        RegisterViews();
        //RegisterServices(_userService);
    }
    public void ChangeMainWindowDataContext(object newContext)
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop.MainWindow.DataContext is IDisposable disposable)
            {
                disposable.Dispose();
            }
        
            desktop.MainWindow.DataContext = newContext;
        }
    }


    public override void OnFrameworkInitializationCompleted()
    {


        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow
            {
                DataContext = new LoginViewViewModel(_userService) // TODO: Retrieve messages from db
            };

        base.OnFrameworkInitializationCompleted();
    }

    private void RegisterViews()
    {
        Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainWindowViewModel>));
        Locator.CurrentMutable.Register(() => new LoginView(), typeof(IViewFor<LoginViewViewModel>));
    }
    
    private void RegisterServices(UserService userService)
    {
        var messages = new ObservableCollection<Message>();
        Locator.CurrentMutable.RegisterConstant(() => new LoginViewViewModel(userService), typeof(LoginViewViewModel));
        Locator.CurrentMutable.RegisterConstant(new MainWindowViewModel(messages), typeof(MainWindowViewModel));
    }
}