using System;
using System.ComponentModel.DataAnnotations;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using ReactiveUI;
using Ynov_Workshare.Models;
using Ynov_Workshare.Views;

namespace Ynov_Workshare.ViewModels;

public class LoginViewViewModel : PageViewModelBase
{
    private string _username;
    private string _password;
    private string _statusMessage;
    private readonly UserService _userService;
    private bool _CanNavigateNext;
    public ReactiveCommand<Unit, Unit> LoginCommand { get; }

    public LoginViewViewModel(UserService userService)
    {
        _userService = userService;
        LoginCommand = ReactiveCommand.CreateFromTask(LoginAsync);
    }
    
    public LoginViewViewModel(){}

    [Required(ErrorMessage = "Le champs nom d'utilisateur est requis")]
    [EmailAddress(ErrorMessage = "Le format n'est pas correcte")]
    public string Username
    {
        get => _username;
        set => this.RaiseAndSetIfChanged(ref _username, value);
    }

    [Required(ErrorMessage = "Le champs mot de passe est requis")]
    
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
// Mettre à jour le DataContext de MainWindow pour passer à l'écran principal
        ((App)Application.Current).ChangeMainWindowDataContext(new MainView());
        Console.WriteLine(obj);
    }

    // This is our first page, so we can navigate to the next page in any case
    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
        //protected set { this.RaiseAndSetIfChanged(ref _CanNavigateNext, value); }
    }

    // You cannot go back from this page
    public override bool CanNavigatePrevious
    {
        get => false;
        protected set => throw new NotSupportedException();
    }
    
    // Update CanNavigateNext. Allow next page if E-Mail and Password are valid
    private void UpdateCanNavigateNext()
    {
        CanNavigateNext =
            !string.IsNullOrEmpty(Username)
            && Username.Contains("@")
            && !string.IsNullOrEmpty(Password);
    }
}