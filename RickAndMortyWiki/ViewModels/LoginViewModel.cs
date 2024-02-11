using System;
using System.IO;
using System.Reactive;
using ReactiveUI;
using RickAndMortyWiki.Models;
using System.Text.Json;
using Avalonia.Platform;
using RickAndMortyWiki.Utils;

namespace RickAndMortyWiki.ViewModels;

public class LoginViewModel : ReactiveObject, IScreen, IRoutableViewModel
{
    public IScreen HostScreen { get; }
    public string UrlPathSegment { get; } = Guid.NewGuid().ToString().Substring(0, 5);
    public RoutingState Router { get; } = new RoutingState();
    public ReactiveCommand<Unit, Unit> RegisterCommand { get; }
    public ReactiveCommand<Unit, Unit> LoginCommand { get; }
    public SettingsModel? Settings { get; set; }

    private string username;
    private string password;

    private readonly DatabaseService databaseService;

    public string Username
    {
        get => username;
        set => this.RaiseAndSetIfChanged(ref username, value);
    }

    public string Password
    {
        get => password;
        set => this.RaiseAndSetIfChanged(ref password, value);
    }


    [Obsolete("Obsolete")]
    public LoginViewModel(IScreen screen)
    {
        string workingDirectory = Environment.CurrentDirectory;
        string databasePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/sqlite.db";
        databaseService = new DatabaseService(databasePath);
        Settings = SettingsHelper.LoadSettings();
        HostScreen = screen;
        LoginCommand = ReactiveCommand.Create(Login);
        RegisterCommand = ReactiveCommand.Create(Register);
        if (Settings.LoggedIn)
        {
            Router.Navigate.Execute(new NavigationViewModel(this));
        }
        // if (Settings.LoggedIn) ReactiveCommand.Create<string>(PerformAction);
    }

    [Obsolete("Obsolete")]
    private void Register()
    {
        var newUser = new User
        {
            username = Username,
            password = Password
        };
        var check = databaseService.InsertUser(newUser);
        if (check == 1)
        {
            var user = databaseService.GetUser(Username, password);
            string workingDirectory = Environment.CurrentDirectory;
            string dataFilePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/Data/user.json";
            string text = File.ReadAllText(dataFilePath);
            var settings = JsonSerializer.Deserialize<SettingsModel>(text);
            if (settings != null) settings.UserId = user.id;
            if (settings != null) settings.Username = Username;
            if (settings != null) settings.Password = Password;
            if (settings != null) settings.LoggedIn = true;
            var jsonString = JsonSerializer.Serialize(settings);
            File.WriteAllText(dataFilePath, jsonString);
            Router.Navigate.Execute(new NavigationViewModel(this));
        }
        else
        {
            Console.WriteLine("Wrong");
        }
    }

    [Obsolete("Obsolete")]
    private void Login()
    {
        var user = databaseService.GetUser(Username, password);
        if (user != null)
        {
            string workingDirectory = Environment.CurrentDirectory;
            string dataFilePath = Directory.GetParent(workingDirectory)?.Parent?.Parent?.FullName + "/Data/user.json";
            string text = File.ReadAllText(dataFilePath);
            var settings = JsonSerializer.Deserialize<SettingsModel>(text);
            if (settings != null) settings.UserId = user.id;
            if (settings != null) settings.Username = Username;
            if (settings != null) settings.Password = Password;
            if (settings != null) settings.LoggedIn = true;
            var jsonString = JsonSerializer.Serialize(settings);
            File.WriteAllText(dataFilePath, jsonString);
            Router.Navigate.Execute(new NavigationViewModel(this));
        }
        else
        {
        }
    }
}