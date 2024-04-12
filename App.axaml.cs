using System.Collections.Generic;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using Splat;
using Ynov_Workshare.Models;
using Ynov_Workshare.ViewModels;
using Ynov_Workshare.Views;

namespace Ynov_Workshare;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
        RegisterViews();
        RegisterServices();
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(new List<Message>()) // TODO: Retrieve messages from db
            };

        base.OnFrameworkInitializationCompleted();
    }

    private void RegisterViews()
    {
        Locator.CurrentMutable.Register(() => new MainWindow(), typeof(IViewFor<MainWindowViewModel>));
        Locator.CurrentMutable.Register(() => new LoginView(), typeof(IViewFor<LoginViewViewModel>));
    }
    
    private void RegisterServices()
    {
        var apiService = new ApiService();
        var messages = new ObservableCollection<Message>();
        Locator.CurrentMutable.RegisterConstant(() => new LoginViewViewModel(apiService), typeof(LoginViewViewModel));
        Locator.CurrentMutable.RegisterConstant(new MainWindowViewModel(messages), typeof(MainWindowViewModel));
    }
}