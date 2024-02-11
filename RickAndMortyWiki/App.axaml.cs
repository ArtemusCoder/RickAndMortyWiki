using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using ReactiveUI;
using RickAndMortyWiki.ViewModels;
using RickAndMortyWiki.Views;
using Splat;

namespace RickAndMortyWiki;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindowView
            {
                DataContext = new LoginViewModel(Locator.Current.GetService<IScreen>()),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}