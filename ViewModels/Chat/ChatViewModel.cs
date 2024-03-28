using System.Windows.Input;
using ReactiveUI;

namespace Ynov_Workshare.ViewModels.Chat;

public class ChatViewModel : ViewModelBase
{
    public ChatViewModel()
    {
        SendMessageCommand = ReactiveCommand.Create(() => { });
    }

    public ICommand SendMessageCommand { get; }
}