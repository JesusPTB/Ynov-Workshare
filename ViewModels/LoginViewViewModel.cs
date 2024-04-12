using System;
using System.Threading.Tasks;
using ReactiveUI;
using Ynov_Workshare.Api;
using Ynov_Workshare.Utils;

namespace Ynov_Workshare.ViewModels;

public class LoginViewViewModel : ReactiveObject
{
    private string _username;
    private string _password;
    private string _statusMessage;
    private Auth _auth = new Auth();

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

    public string StatusMessage
    {
        get => _statusMessage;
        set => this.RaiseAndSetIfChanged(ref _statusMessage, value);
    }

    public async Task LoginAsync()
    {
        // Faites l'appel API à votre backend pour authentifier l'utilisateur
        var response = _auth.LoginAsync(_username, _password);
        
        if (response.Result.IsSuccessStatusCode)
        {
            StatusMessage = "Connexion réussie !";
            Console.WriteLine(StatusMessage); //TODO: à virer
        }
        else
        {
            StatusMessage = "Échec de la connexion : " + await response.Result.Content.ReadAsStringAsync();
            Console.WriteLine(StatusMessage); //TODO: à virer
        }
    }
}