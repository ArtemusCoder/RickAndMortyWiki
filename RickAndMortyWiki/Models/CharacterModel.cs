using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using RickAndMortyWiki.Utils;

namespace RickAndMortyWiki.Models;

public class CharacterModel
{
    public int id { get; set; }
    public string name { get; set; }
    public string status { get; set; }
    public string species { get; set; }
    public string type { get; set; }
    public string gender { get; set; }
    public Origin origin { get; set; }
    public Location location { get; set; }
    public string image { get; set; }
    public List<string> episode { get; set; }
    public string url { get; set; }
    public string created { get; set; }
    
    public CharacterModel(int Id, string Name, string Status, string Species, string Type, string Gender)
    {
        id = Id;
        name = Name;
        status = Status;
        species = Species;
        if (Type == "")
        {
            type = "Неизвестно";
        }
        else
        {
            type = Type;
        }
        gender = Gender;
    }
    
    public void ModifyFavourite(int favId)
    {
        
        string workingDirectory = Environment.CurrentDirectory;
        string dataFilePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/Data/user.json";
        string text = File.ReadAllText(dataFilePath);
        var settings = JsonSerializer.Deserialize<SettingsModel>(text);
        var userId = settings.UserId;
        string databasePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/sqlite.db";
        DatabaseService db = new DatabaseService(databasePath);
        if (db.CheckFavourite(favId, (int)userId))
        {
            SettingsHelper.RemoveFromFavourite(favId, (int)userId);
        }
        else
        {
            SettingsHelper.AddCharacterToFavorite(favId, (int)userId);
        }
    }
}