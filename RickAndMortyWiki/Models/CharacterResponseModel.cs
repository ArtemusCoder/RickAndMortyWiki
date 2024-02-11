using System;
using System.Collections.Generic;
using RickAndMortyWiki.Utils;

namespace RickAndMortyWiki.Models;

public class Info
{
    public int count { get; set; }
    public int pages { get; set; }
    public string? next { get; set; }
    public string? prev { get; set; }
}

public class LocationCharacter
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Origin
{
    public string name { get; set; }
    public string url { get; set; }
}

public class Result
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
    public DateTime created { get; set; }
}

public class CharacterResponseModel
{
    public Info info { get; set; }
    public List<Result> results { get; set; }
}