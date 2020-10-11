using LeaderBoard.Entity;
using LeaderBoard.Entity.DbSettings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeaderBoard.Services
{
    /// <summary>
    /// Service to connect to MongoDB 
    /// </summary>
    public class PlayerStatsService
    {
        private readonly IMongoCollection<PlayerStat> _playerStats;

        public PlayerStatsService(IPlayerStatsDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _playerStats = database.GetCollection<PlayerStat>(settings.CollectionName);
        }

        /// <summary>
        /// To get all the records as queryable
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlayerStat> Get() =>
            _playerStats.AsQueryable<PlayerStat>();

        public IList<PlayerStat> GetPlayerStats(string id) =>
            _playerStats.Find<PlayerStat>(playerStat => playerStat.UserName.ToLower() == id.ToLower()).ToList<PlayerStat>();

        public IList<PlayerStat> GetPlayerStats(string id, int duration) =>
            _playerStats.Find<PlayerStat>(playerStat => playerStat.UserName.ToLower() == id.ToLower() & playerStat.TimeStamp >= DateTime.Now.AddHours(-duration)).ToList<PlayerStat>();

        public PlayerStat Create(PlayerStat playerStat)
        {
            _playerStats.InsertOne(playerStat);
            return playerStat;
        }

        public void Remove(PlayerStat PlayerStatIn) =>
            _playerStats.DeleteOne(PlayerStat => PlayerStat.Id == PlayerStatIn.Id);
    }
}
