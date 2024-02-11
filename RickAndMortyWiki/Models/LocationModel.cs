using System;
using System.Collections.Generic;

namespace RickAndMortyWiki.Models;

public class LocationModel
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string dimension { get; set; }
    public string residents { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }

    public LocationModel(int Id, string Name, string Type, string Dimension)
    {
        id = Id;
        name = Name;
        type = Type;
        dimension = Dimension;
    }
}