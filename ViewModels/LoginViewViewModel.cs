using System.Threading.Tasks;
using ReactiveUI;
using Ynov_Workshare.Models;

namespace Ynov_Workshare.ViewModels;

public class LoginViewViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private string _statusMessage;
    private readonly ApiService _apiService;

    public LoginViewViewModel(ApiService apiService)
    {
        _apiService = apiService;
    }

    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }
    
    public string Password
    {
        get => _password;
        set => this.RaiseAndSetIfChanged(ref _password, value);
    }

    public async Task<LoginForm> LoginAsync()
    {
        var loginForm = new LoginForm(Username, Password);
        // Appel API au backend pour authentifier l'utilisateur
        return await _apiService.Post<LoginForm>("api/Users/Login", loginForm);
    }
}