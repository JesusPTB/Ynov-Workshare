using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;
using Ynov_Workshare.Models;

namespace Ynov_Workshare.ViewModels;

public class MainWindowViewModel : PageViewModelBase
{
    private readonly HubConnection _connection;
    
    public MainWindowViewModel(IEnumerable<Message>? messages)
    {
        // Set current page to first on start up
        _CurrentPage = Pages[0];

        // Create Observables which will activate to deactivate our commands based on CurrentPage state
        var canNavNext = this.WhenAnyValue(x => x.CurrentPage.CanNavigateNext);
        var canNavPrev = this.WhenAnyValue(x => x.CurrentPage.CanNavigatePrevious);

        NavigateNextCommand = ReactiveCommand.Create(NavigateNext, canNavNext);
        NavigatePreviousCommand = ReactiveCommand.Create(NavigatePrevious, canNavPrev);
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:8080/ChatHub")
            .Build();

        ListMessages = new ObservableCollection<Message>(messages);
        SendMessageCommand = ReactiveCommand.Create(SendMessage);
        ConnectCommand = ReactiveCommand.Create(Connect);
    }
    private readonly PageViewModelBase[] Pages =
    {
        new LoginViewViewModel(),
        new MainWindowViewModel(null)
    };
    
    // The default is the first page
    private PageViewModelBase _CurrentPage;

    /// <summary>
    /// Gets the current page. The property is read-only
    /// </summary>
    public PageViewModelBase CurrentPage
    {
        get { return _CurrentPage; }
        private set { this.RaiseAndSetIfChanged(ref _CurrentPage, value); }
    }

    /// <summary>
    /// Gets a command that navigates to the next page
    /// </summary>
    public ICommand NavigateNextCommand { get; }
    
    private void NavigateNext()
    {
        // get the current index and add 1
        var index = Pages.ToList().IndexOf(CurrentPage) + 1;

        //  /!\ Be aware that we have no check if the index is valid. You may want to add it on your own. /!\
        CurrentPage = Pages[index];
    }
    
    /// <summary>
    /// Gets a command that navigates to the previous page
    /// </summary>
    public ICommand NavigatePreviousCommand { get; }

    private void NavigatePrevious()
    {
        // get the current index and subtract 1
        var index = Pages.ToList().IndexOf(CurrentPage) - 1;

        //  /!\ Be aware that we have no check if the index is valid. You may want to add it on your own. /!\
        CurrentPage = Pages[index];
    }
    public ICommand SendMessageCommand { get; }
    public ICommand ConnectCommand { get; }
    
    public string UserInput { get; set; } = "Anonymous";
    public string MessageInput { get; set; } = string.Empty;

    public ObservableCollection<Message> ListMessages { get; }
    
    private async void Connect()
    {
        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            var newMessage = $"{user}: {message}";
            ListMessages.Add(new Message { User = user, Content = message });
            Console.WriteLine(newMessage);
        });

        try
        {
            await _connection.StartAsync();
            Console.WriteLine("Connection started");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async void SendMessage()
    {
        Console.WriteLine("Sending message");
        await _connection.InvokeAsync("SendMessageToAll", UserInput, MessageInput);
        Console.WriteLine("Message sent");
    }
    
    public override bool CanNavigateNext
    {
        get => true;
        protected set => throw new NotSupportedException();
    }

   
    public override bool CanNavigatePrevious
    {
        get => true;
        protected set => throw new NotSupportedException();
    }
    

}