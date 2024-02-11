using SQLite.Net.Attributes;

namespace RickAndMortyWiki.Models;

public class Favourite
{
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public int user_id { get; set; }
    public int character_id { get; set; }
}