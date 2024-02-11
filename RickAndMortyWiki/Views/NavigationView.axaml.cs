using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RickAndMortyWiki.ViewModels;
using ReactiveUI;


namespace RickAndMortyWiki.Views;

public partial class NavigationView : ReactiveUserControl<NavigationViewModel>
{
    public NavigationView()
    {

        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}