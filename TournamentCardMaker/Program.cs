using Microsoft.Extensions.Configuration;
using TournamentCardMaker;

Console.WriteLine(Directory.GetCurrentDirectory());

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

var date = string.IsNullOrWhiteSpace(config["Settings:Date"])
    ? DateTime.Now.AddDays(1).ToString("yyyyMMdd")
    : config["Settings:Date"];
var tournamentId = string.IsNullOrWhiteSpace(config["Settings:Tournament"])
    ? "40880086-51bf-4646-ac76-0efa38c30885"
    : config["Settings:Tournament"];
var matchInfo = await new MatchInfoScraper(tournamentId, date).GetMatchesAsync();

// var json = JsonSerializer.Serialize(matchInfo, new JsonSerializerOptions
// {
//     WriteIndented = true
// });
// Console.WriteLine(json);
var fileName = string.IsNullOrWhiteSpace(config["Settings:OutputFileName"])
    ? "matches"
    : config["Settings:OutputFileName"];
CardMaker.CreateCards(matchInfo, fileName);