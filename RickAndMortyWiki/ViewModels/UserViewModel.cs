using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Text.Json;
using Microsoft.VisualBasic;
using ReactiveUI;
using RickAndMortyWiki.Models;
using RickAndMortyWiki.Utils;
using Splat;

namespace RickAndMortyWiki.ViewModels;

public class UserViewModel : ReactiveObject, IRoutableViewModel, IScreen
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();

    public ReactiveCommand<Unit, Unit> SignOutCommand { get; set; }

    public ObservableCollection<CharacterModel> FavoriteCharacters { get; }

    public string? Username { get; set; }

    [Obsolete("Obsolete")]
    public UserViewModel(IScreen screen)
    {
        FavoriteCharacters = new ObservableCollection<CharacterModel>();
        HostScreen = screen;
        SignOutCommand = ReactiveCommand.Create(SingOut);
        var settings = SettingsHelper.LoadSettings();
        Username = settings?.Username;
        var userId = settings.UserId;
        FavoriteCharacters = new ObservableCollection<CharacterModel>();
        string workingDirectory = Environment.CurrentDirectory;
        string databasePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/sqlite.db";
        DatabaseService db = new DatabaseService(databasePath);
        List<string> char_list = new List<string>();
        foreach (var character in db.GetFavouriteByUser(settings.UserId))
        {
            char_list.Add(character.character_id.ToString());
        }

        string url = "https://rickandmortyapi.com/api/character/" + string.Join(",", char_list);
        if (char_list.Count != 0)
        {
            using (var httpClient = new HttpClient())
            {
                var response = httpClient.GetStringAsync(url).Result;
                if (char_list.Count == 1)
                {
                    var apiResponse = JsonSerializer.Deserialize<CharacterModel>(response);
                    FavoriteCharacters.Add(
                        new CharacterModel(
                            apiResponse.id,
                            apiResponse.name,
                            apiResponse.status,
                            apiResponse.species,
                            apiResponse.type,
                            apiResponse.gender
                        ));
                }
                else
                {
                    var apiResponse = JsonSerializer.Deserialize<List<CharacterModel>>(response);
                    foreach (var character in apiResponse)
                    {
                        FavoriteCharacters.Add(
                            new CharacterModel(
                                character.id,
                                character.name,
                                character.status,
                                character.species,
                                character.type,
                                character.gender
                            ));
                    }
                }
            }
        }
    }

    [Obsolete("Obsolete")]
    public void SingOut()
    {
        SettingsHelper.SingOut();
        Router.Navigate.Execute(new LoginViewModel(this));
    }
}