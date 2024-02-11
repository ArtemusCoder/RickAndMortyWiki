using System;
using System.Collections.Generic;

namespace RickAndMortyWiki.Models;

public class Episode
{
    public int id { get; set; }
    public string name { get; set; }
    public string air_date { get; set; }
    public string episode { get; set; }
    public List<string> characters { get; set; }
    public string url { get; set; }
    public DateTime created { get; set; }
}

public class EpisodeResponseModel
{
    public Info info { get; set; }
    public List<Episode> results { get; set; }
}