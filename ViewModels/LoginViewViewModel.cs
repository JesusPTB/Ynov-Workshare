using System;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Ynov_Workshare.Models;

namespace Ynov_Workshare.ViewModels;

public class LoginViewViewModel : ViewModelBase
{
    private string _username;
    private string _password;
    private string _statusMessage;
    private readonly UserService _userService;
    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public LoginViewViewModel(UserService  userService)
    {
        _userService = userService;
        LoginCommand = ReactiveCommand.CreateFromTask(LoginAsync);
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
    

    public async Task LoginAsync()
    {
        var loginForm = new LoginForm(Username, Password);
        // Appel API au backend pour authentifier l'utilisateur
        var obj = await _userService.Login(loginForm);
        Console.WriteLine(obj);
    }
}