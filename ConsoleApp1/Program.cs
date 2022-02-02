// See https://aka.ms/new-console-template for more information
using Pubg.Net;
using Pubg.Net.Extensions;

string apiKey = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJqdGkiOiI2YjM5YjVkMC03MjcxLTAxMzktNjAyNS0xZmE5ZGJlNTJlMWYiLCJpc3MiOiJnYW1lbG9ja2VyIiwiaWF0IjoxNjE2OTkwNTEyLCJwdWIiOiJibHVlaG9sZSIsInRpdGxlIjoicHViZyIsImFwcCI6Ii1kNmEwNTlmZi0wMzBjLTRmNGEtYWRlMi0yZGRiMGMzODNkYjQifQ.w4CE_d7XkrMv6ddWNJivrQRyC-DEY49qd5vcjY0ohWY";
PubgApiConfiguration.Configure(opt => opt.ApiKey = apiKey);

var playerService = new PubgPlayerService();
var matchService = new PubgMatchService();

var request = new GetPubgPlayersRequest
{
    ApiKey = apiKey,
    PlayerNames = new string[] { "Not Schaefer" }//, "BuddyVDoodle", "PtrPumpkinEater" }
};

IEnumerable<PubgPlayer>? players = await playerService.GetPlayersAsync(PubgPlatform.Xbox, request);
if (players.Any())
{
    foreach (var player in players)
    {
        Console.WriteLine($"{"Rank",5}{"Kills",6}{"Death Type",11}{"Map",21}{"Date/Time",20}");
        List<PubgMatch> matches = new();
        foreach (var matchId in player.MatchIds)
        {
            PubgMatch? match = await matchService.GetMatchAsync(PubgPlatform.Xbox, matchId, apiKey);
            matches.Add(match);
        }
        matches.Sort();
        foreach (var match in matches)
        {
            if (match != null)
            {
                PubgParticipant? participant = match.Rosters.FirstOrDefault(r => r.Participants.Any(p => p.Stats.PlayerId == player.Id))?.Participants.FirstOrDefault(p => p.Stats.PlayerId == player.Id);
                Console.WriteLine($"{participant?.Stats.WinPlace,5}{participant?.Stats.Kills,6}{participant?.Stats.DeathType,11}{match.MapName.GetDescription(),21}{DateTime.Parse(match.CreatedAt).ToString("yyyy/MM/dd HH:mm:ss"),20}");
            }
            else
            {
                Console.WriteLine($"Match not found: {match?.Id}");
            }
        }
    }
}