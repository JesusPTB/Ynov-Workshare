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
        ConnectCommand = ReactiveCommand.Create(ConnectToChatHub);
        JoinGroupCommand = ReactiveCommand.Create<string>(JoinGroup);
    }

    public ICommand SendMessageCommand { get; }
    public ICommand ConnectCommand { get; }
    public ICommand JoinGroupCommand { get; }
    
    public string UserInput { get; set; } = "Anonymous";
    public string MessageInput { get; set; } = string.Empty;

    public ObservableCollection<Message> ListMessages { get; }
    
    /// <summary>
    /// Connect client to the SignalR ChatHub
    /// </summary>
    private async void ConnectToChatHub()
    {
        _connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            ListMessages.Add(new Message { User = user, Content = message });
        });

        try
        {
            await _connection.StartAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private async void SendMessage()
    {
        await _connection.InvokeAsync("SendMessageToAll", UserInput, MessageInput);
    }
    
    public async void SendMessageToGroup(string groupName)
    {
        await _connection.InvokeAsync("SendMessageToGroup", groupName, UserInput, MessageInput);
    }

    /// <summary>
    /// Join a group
    /// </summary>
    /// <param name="groupName"></param>
    private async void JoinGroup(string groupName)
    {
        // await _connection.InvokeAsync("JoinGroup", groupName);
        await _connection.InvokeAsync("JoinGroup", "test");
    }
}