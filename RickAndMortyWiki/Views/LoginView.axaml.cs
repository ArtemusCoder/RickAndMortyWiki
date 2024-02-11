using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using RickAndMortyWiki.ViewModels;
using ReactiveUI;


namespace RickAndMortyWiki.Views;

public partial class LoginView : ReactiveUserControl<MainWindowViewModel>
{
    public LoginView()
    {

        this.WhenActivated(disposables => { });
        AvaloniaXamlLoader.Load(this);
    }
}