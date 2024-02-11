using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using RickAndMortyWiki.Models;
using SQLite;
using SQLite.Net;
using SQLite.Net.Platform.Generic;

namespace RickAndMortyWiki.Utils;

public class DatabaseService
{
    private readonly SQLiteConnection _connection;

    public DatabaseService(string databasePath)
    {
        _connection = new SQLiteConnection(new SQLitePlatformGeneric(), databasePath); 
    }

    public int InsertUser(User user)
    {
        return _connection.Insert(user);
    }

    public User? GetUser(string username, string password)
    {
        var user = _connection.Table<User>().FirstOrDefault(u => u.username == username && u.password == password);
        return user;
    }

    public List<Favourite> GetFavouriteByUser(int id)
    {
        List<Favourite> favourites = _connection.Query<Favourite>("SELECT * FROM Favourite WHERE user_id = ?", id);
        return favourites;
    }

    public int InsertFav(Favourite fav)
    {
        return _connection.Insert(fav);
    }

    public bool CheckFavourite(int fav_id, int user_id)
    {
        var res = _connection.Table<Favourite>().FirstOrDefault(f => f.user_id == user_id && f.character_id == fav_id);
        return (res != null);
    }

    public bool DeleteFavourite(int fav_id, int user_id)
    {
        var row =_connection.Table<Favourite>().FirstOrDefault(f => f.user_id == user_id && f.character_id == fav_id);
        if (row != null)
        {
            int rowsAffected = _connection.Delete<Favourite>(row.id);
            return true;
        }
        return false;
    }
}