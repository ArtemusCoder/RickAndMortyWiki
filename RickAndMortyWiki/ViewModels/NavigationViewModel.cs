using System;
using System.Reactive;

namespace RickAndMortyWiki.ViewModels;
using ReactiveUI;

public class NavigationViewModel: ReactiveObject, IRoutableViewModel, IScreen 
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, IRoutableViewModel> CharactersPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> LocationsPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> EpisodesPage { get; }
    public ReactiveCommand<Unit, IRoutableViewModel> UserPage { get; }
    [Obsolete("Obsolete")]
    public NavigationViewModel(IScreen screen)
    {
        HostScreen = screen;
        Router.Navigate.Execute(new CharactersViewModel(this));
        CharactersPage = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new CharactersViewModel(this)));
        LocationsPage = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new LocationsViewModel(this)));
        EpisodesPage = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new EpisodesViewModel(this)));
        UserPage = ReactiveCommand.CreateFromObservable(() => Router.Navigate.Execute(new UserViewModel(this)));
    }

}