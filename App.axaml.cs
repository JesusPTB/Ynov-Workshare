using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Ynov_Workshare.Models;
using Ynov_Workshare.ViewModels;
using Ynov_Workshare.Views;

namespace Ynov_Workshare;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
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
}