using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.AspNetCore.SignalR.Client;
using ReactiveUI;
using Ynov_Workshare.Models;

namespace Ynov_Workshare.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly HubConnection _connection;

    public MainWindowViewModel(IEnumerable<Message> messages)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("http://localhost:8080/ChatHub")
            .Build();

        ListMessages = new ObservableCollection<Message>(messages);
        SendMessageCommand = ReactiveCommand.Create(SendMessage);
        ConnectCommand = ReactiveCommand.Create(Connect);
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
}