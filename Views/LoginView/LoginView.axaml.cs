using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Ynov_Workshare.Views.LoginWindow;

public partial class LoginView : UserControl
{
    public LoginView()
    {
        InitializeComponent();
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        // Charger et afficher la vue Home
        var mainWindow = (MainWindow)VisualRoot;
        mainWindow.Content = new HomeView();
    }
}