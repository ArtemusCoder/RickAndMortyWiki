using System.Collections.Generic;

namespace RickAndMortyWiki.Models;


public class SettingsModel
{
    public int UserId { get; set; }
    
    public string? Username { get; set; }
    public string? Password { get; set; }
    public bool LoggedIn { get; set; }
}