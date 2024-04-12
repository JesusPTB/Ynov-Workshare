using System.Net.Http;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Ynov_Workshare.Views.LoginWindow;

public partial class LoginView : UserControl
{
    private TextBox _username;
    private TextBox _password;
    
    public LoginView()
    {
        InitializeComponent();
        _username = this.FindControl<TextBox>("Username");
        _password = this.FindControl<TextBox>("Password");
    }

    private void LoginButton_Click(object sender, RoutedEventArgs e)
    {
        //TODO: appeler fonction connexion
    }
}