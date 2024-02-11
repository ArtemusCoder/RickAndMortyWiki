using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using DynamicData;
using RickAndMortyWiki.Models;

namespace RickAndMortyWiki.Utils;

public class SettingsHelper
{
    public static SettingsModel? LoadSettings()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string dataFilePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/Data/user.json";
        string text = File.ReadAllText(dataFilePath);
        var settings = JsonSerializer.Deserialize<SettingsModel>(text);
        return settings;
    }

    public static void AddCharacterToFavorite(int fav_id, int user_id)
    {
        string workingDirectory = Environment.CurrentDirectory;
        string databasePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/sqlite.db";
        DatabaseService db = new DatabaseService(databasePath);
        Favourite fav = new Favourite
        {
            character_id = fav_id,
            user_id = user_id
        };
        db.InsertFav(fav);
    }
    public static void RemoveFromFavourite(int id, int userId)
    {
        string workingDirectory = Environment.CurrentDirectory;
        string databasePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/sqlite.db";
        DatabaseService db = new DatabaseService(databasePath);
        db.DeleteFavourite(id, userId);
    }
    public static void SingOut()
    {
        string workingDirectory = Environment.CurrentDirectory;
        string dataFilePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/Data/user.json";
        string text = File.ReadAllText(dataFilePath);
        var settings = JsonSerializer.Deserialize<SettingsModel>(text);
        if (settings.LoggedIn)
        {
            settings.LoggedIn = false;
        }
        var jsonString = JsonSerializer.Serialize(settings);
        File.WriteAllText(dataFilePath, jsonString);
    }
}