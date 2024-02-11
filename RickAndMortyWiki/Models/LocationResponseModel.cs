using System;
using System.Collections.Generic;

namespace RickAndMortyWiki.Models;

public class Location
{
    public int id { get; set; }
    public string name { get; set; }
    public string type { get; set; }
    public string dimension { get; set; }
    public List<string> residents { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }
}

public class LocationResponseModel
{
    public Info info { get; set; }
    public List<Location> results { get; set; }
}





