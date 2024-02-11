using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive;
using System.Text.Json;
using RickAndMortyWiki.Models;
using ReactiveUI;
using RickAndMortyWiki.Utils;

namespace RickAndMortyWiki.ViewModels;

public class CharactersViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();
    
    public ReactiveCommand<Unit, Unit> PrevCommand { get; set; }
    public ReactiveCommand<Unit, Unit> NextCommand { get; set; }
    
    
    private string? _nextPage;
    private string? _prevPage;
    public ObservableCollection<CharacterModel> Characters { get; set; }
    
    public new event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }


    [Obsolete("Obsolete")]
    public CharactersViewModel(IScreen screen)
    {
        HostScreen = screen;
        Characters = new ObservableCollection<CharacterModel>();
        LoadPage("https://rickandmortyapi.com/api/character?page=1");
        PrevCommand = ReactiveCommand.Create(Prev);
        NextCommand = ReactiveCommand.Create(Next);
    }

    [Obsolete("Obsolete")]
    private void Next()
    {
        if (_nextPage != null)
            LoadPage(_nextPage);
        OnPropertyChanged(nameof(Characters));
    }
    
    [Obsolete("Obsolete")]
    public void Prev()
    {
        if (_prevPage != null)
            LoadPage(_prevPage);
        OnPropertyChanged(nameof(Characters));
    }

    [Obsolete("Obsolete")]
    public void LoadPage(string url)
    {
        Characters.Clear();
        using (var httpClient = new HttpClient())
        {
            var response = httpClient.GetStringAsync(url).Result;
            var apiResponse = JsonSerializer.Deserialize<CharacterResponseModel>(response);
            _nextPage = apiResponse.info.next;
            _prevPage = apiResponse.info.prev;
            if (apiResponse != null)
            {
                foreach (var character in apiResponse.results)
                {
                    Characters.Add(
                        new CharacterModel(
                            character.id,
                            character.name,
                            character.status,
                            character.species,
                            character.type,
                            character.gender
                        )
                    );
                }
            }
        }
    }
}
