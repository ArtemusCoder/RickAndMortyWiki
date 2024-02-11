using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Reactive;
using System.Text.Json;
using ReactiveUI;
using RickAndMortyWiki.Models;
using RickAndMortyWiki.Utils;

namespace RickAndMortyWiki.ViewModels;

public class LocationsViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();
    public ObservableCollection<LocationModel> Locations { get; }
    
    public ReactiveCommand<Unit, Unit> PrevCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NextCommand { get; set; }

    private string? _nextPage;
    private string? _prevPage;

    public new event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    [Obsolete("Obsolete")]
    public LocationsViewModel(IScreen screen)
    {
        HostScreen = screen;
        Locations = new ObservableCollection<LocationModel>();
        LoadPage("https://rickandmortyapi.com/api/location?page=1");
        PrevCommand = ReactiveCommand.Create(Prev);
        NextCommand = ReactiveCommand.Create(Next);
    }

    [Obsolete("Obsolete")]
    public void Next()
    {
        if (_nextPage != null)
            LoadPage(_nextPage);
        OnPropertyChanged(nameof(Locations));
    }

    [Obsolete("Obsolete")]
    public void Prev()
    {
        if (_prevPage != null)
            LoadPage(_prevPage);
        OnPropertyChanged(nameof(Locations));
    }
    
    [Obsolete("Obsolete")]
    public void LoadPage(string url)
    {
        Locations.Clear();
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.GetStringAsync(url).Result;
            var apiResponse = JsonSerializer.Deserialize<LocationResponseModel>(response);
            _nextPage = apiResponse.info.next;
            _prevPage = apiResponse.info.prev;
            if (apiResponse != null)
            {
                foreach (var location in apiResponse.results)
                {
                    Locations.Add(
                        new LocationModel(
                            location.id,
                            location.name,
                            location.type,
                            location.dimension
                        )
                    );
                }
            }
        }
    }
}