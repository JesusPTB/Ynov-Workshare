using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using FluentAvalonia.UI.Controls;

namespace Ynov_Workshare.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    private void NavigationView_OnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        var navigationView = sender as NavigationView;
        Console.WriteLine("Current item: " + navigationView?.Content);
        var selectedItem = e.SelectedItem as NavigationViewItem;
        Console.WriteLine("Selected item: " + selectedItem?.Content);
        // navigationView.Content = new ChatView();
        switch (selectedItem)
        {
            case {Content: "Accueil"}:
                navigationView.Content = new HomeView();
                break;
            case {Content: "Groupes"}:
                navigationView.Content = new ChatView();
                break;
        }
    }
}