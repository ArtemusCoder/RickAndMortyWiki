using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Reactive;
using System.Text.Json;
using ReactiveUI;
using RickAndMortyWiki.Models;
using RickAndMortyWiki.Utils;

namespace RickAndMortyWiki.ViewModels;

public class EpisodesViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();
    public ObservableCollection<EpisodeModel> Episodes { get; }

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
    public EpisodesViewModel(IScreen screen)
    {
        HostScreen = screen;
        Episodes = new ObservableCollection<EpisodeModel>();
        LoadPage("https://rickandmortyapi.com/api/episode?page=1");
        PrevCommand = ReactiveCommand.Create(Prev);
        NextCommand = ReactiveCommand.Create(Next);
    }

    [Obsolete("Obsolete")]
    public void Next()
    {
        if (_nextPage != null)
            LoadPage(_nextPage);
        OnPropertyChanged(nameof(Episodes));
    }

    [Obsolete("Obsolete")]
    public void Prev()
    {
        if (_prevPage != null)
            LoadPage(_prevPage);
        OnPropertyChanged(nameof(Episodes));
    }

    [Obsolete("Obsolete")]
    public void LoadPage(string url)
    {
        Episodes.Clear();
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.GetStringAsync(url).Result;
            var apiResponse = JsonSerializer.Deserialize<EpisodeResponseModel>(response);
            _nextPage = apiResponse.info.next;
            _prevPage = apiResponse.info.prev;
            if (apiResponse != null)
            {
                foreach (var episode in apiResponse.results)
                {
                    Episodes.Add(
                        new EpisodeModel(
                            episode.id,
                            episode.name,
                            episode.air_date,
                            episode.episode
                        )
                    );
                }
            }
        }
    }
}