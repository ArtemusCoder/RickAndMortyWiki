using System;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RickAndMortyWiki.ViewModels;
using ReactiveUI;
using RickAndMortyWiki.ViewModels;


namespace RickAndMortyWiki.Views;

public partial class EpisodesView : ReactiveUserControl<EpisodesViewModel>
{
    public EpisodesView()
    {

        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}