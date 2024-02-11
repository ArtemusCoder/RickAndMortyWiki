using System;
using RickAndMortyWiki.Views;
using RickAndMortyWiki.ViewModels;

namespace RickAndMortyWiki.Locators;

using ReactiveUI;

public class AppViewLocator : IViewLocator
{
    public IViewFor ResolveView<T>(T? viewModel, string? contract = null) => viewModel switch
    {
        LoginViewModel context => new LoginView() {DataContext = context}, 
        NavigationViewModel context => new NavigationView() {DataContext = context}, 
        CharactersViewModel context => new CharactersView() {DataContext = context}, 
        LocationsViewModel context => new LocationsView() { DataContext = context },
        UserViewModel context => new UserView() { DataContext = context },
        EpisodesViewModel context => new EpisodesView() { DataContext = context }
    };
}