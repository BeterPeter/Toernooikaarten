using TournamentCardMaker;

var matchInfo = await new MatchInfoScraper("40880086-51bf-4646-ac76-0efa38c30885")
    .GetMatchesAsync();

// var json = JsonSerializer.Serialize(matchInfo, new JsonSerializerOptions
// {
//     WriteIndented = true
// });
// Console.WriteLine(json);
CardMaker.CreateCards(matchInfo, "output.xlsx");