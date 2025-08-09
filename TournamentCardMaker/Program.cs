using Microsoft.Extensions.Configuration;
using TournamentCardMaker;

internal class Program
{
    private static IConfigurationRoot? _config;

    private static async Task Main(string[] args)
    {
        _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var date = GetSettingWithDefault("Settings:Date", DateTime.Now.AddDays(1).ToString("yyyyMMdd"));
        var tournamentId = GetSettingWithDefault("Settings:Tournament", "40880086-51bf-4646-ac76-0efa38c30885");

        MatchInfo[] matchInfo;
        try
        {
            matchInfo = await new MatchInfoScraper(tournamentId, date).GetMatchesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while scraping match-info for {tournamentId} with date {date}: {ex.Message}");
            Console.WriteLine("Please check your settings in appsettings.json");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }

        var fileName = string.Empty;
        try
        {
            fileName = GetSettingWithDefault("Settings:OutputFileName", "matches");
            CardMaker.CreateCards(matchInfo, fileName);
        }
        catch (IOException)
        {
            Console.WriteLine($"It looks like the file {fileName}.xlsx is already open. Please close it and try again.");
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occured while creating cards: {ex.Message}");
            Console.WriteLine("Please check your settings in appsettings.json");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            return;
        }
    }

    private static string GetSettingWithDefault(string key, string defaultValue)
    {
        var settingValue = _config?[key];
        if (string.IsNullOrWhiteSpace(settingValue))
            return defaultValue;
        return settingValue;
    }
}