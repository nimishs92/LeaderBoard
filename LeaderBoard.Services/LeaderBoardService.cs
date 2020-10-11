using LeaderBoard.Entity;
using LeaderBoard.Entity.DbSettings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaderBoard.Services
{
    /// <summary>
    /// Service to get Leader Board
    /// </summary>
    public class LeaderBoardService
    {
        private readonly IMongoCollection<PlayerStat> _playerStats;

        public LeaderBoardService(IPlayerStatsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _playerStats = database.GetCollection<PlayerStat>(settings.CollectionName);
        }

        public IEnumerable<LeaderBoardEntry> GetLeaderBoard(string match, int duration)
        {
            //Get the maximum score of all the players
            var leaders = from playerStat in _playerStats.AsQueryable<PlayerStat>()
                          where playerStat.TimeStamp >= DateTime.Now.AddHours(-duration) & playerStat.Match.ToLower() == match.ToLower()
                          group playerStat by playerStat.UserName into g
                          orderby g.Max(g => g.Score) descending
                          select new LeaderBoardEntry
                          {
                              UserName = g.Key,
                              Score = g.Max(g => g.Score),
                              Kills = g.Max(g => g.Kills)
                          };

            //Assign Rank based on the scores.
            List<LeaderBoardEntry> result = new List<LeaderBoardEntry>();
            foreach (var leaderBoardEntry in leaders)
            {
                leaderBoardEntry.Rank = leaders.Count<LeaderBoardEntry>(entry => entry.Score > leaderBoardEntry.Score) + 1;
                result.Add(leaderBoardEntry);
            }

            return result;
        }

    }
}
