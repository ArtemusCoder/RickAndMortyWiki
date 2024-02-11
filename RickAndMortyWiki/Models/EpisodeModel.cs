using System;
using System.Collections.Generic;

namespace RickAndMortyWiki.Models;

public class EpisodeModel
{
    public int id { get; set; }
    public string name { get; set; }
    public string air_date { get; set; }
    public string episode { get; set; }
    public string characters { get; set; }
    public DateTime created { get; set; }

    public EpisodeModel(int Id, string Name, string Air_date, string Episode)
    {
        id = Id;
        name = Name;
        air_date = Air_date;
        episode = Episode;
    }
}