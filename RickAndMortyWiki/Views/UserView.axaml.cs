using System;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RickAndMortyWiki.ViewModels;
using ReactiveUI;
using RickAndMortyWiki.ViewModels;


namespace RickAndMortyWiki.Views;

public partial class UserView : ReactiveUserControl<UserViewModel>
{
    public UserView()
    {

        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}